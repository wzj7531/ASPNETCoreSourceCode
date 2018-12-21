using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Microsoft.Extensions.FileSystemGlobbing.Util;

namespace Microsoft.Extensions.FileSystemGlobbing.Internal
{

    public class MatcherContext
    {
        private readonly DirectoryInfoBase _root;
        private readonly List<IPatternContext> _includePatternContexts;
        private readonly List<IPatternContext> _excludePatternContexts;
        private readonly List<FilePatternMatch> _files;

        private readonly HashSet<string> _declaredLiteralFolderSegmentInString;
        private readonly HashSet<LiteralPathSegment> _declaredLiteralFolderSegments = new HashSet<LiteralPathSegment>();
        private readonly HashSet<LiteralPathSegment> _declaredLiteralFileSegments = new HashSet<LiteralPathSegment>();

        private bool _declaredParentPathSegment;
        private bool _declaredWildcardPathSegment;

        private readonly StringComparison _comparisonType;

        public MatcherContext(
            IEnumerable<IPattern> includePatterns,
            IEnumerable<IPattern> excludePatterns,
            DirectoryInfoBase directoryInfo,
            StringComparison comparison)
        {
            _root = directoryInfo;
            _files = new List<FilePatternMatch>();
            _comparisonType = comparison;

            _includePatternContexts = includePatterns.Select(pattern => pattern.CreatePatternContextForInclude()).ToList();
            _excludePatternContexts = excludePatterns.Select(pattern => pattern.CreatePatternContextForExclude()).ToList();

            _declaredLiteralFolderSegmentInString = new HashSet<string>(StringComparisonHelper.GetStringComparer(comparison));
        }



        private void Match(DirectoryInfoBase directory, string parentRelativePath)
        {
            PushDirectory(directory);
            Declare();

            var entities = new List<FileSystemInfoBase>();
            

        }

        private void Declare()
        {
            _declaredLiteralFileSegments.Clear();
            _declaredLiteralFolderSegmentInString.Clear();
            _declaredParentPathSegment = false;
            _declaredWildcardPathSegment = false;

            foreach(var include in _includePatternContexts)
            {
                include.Declare(DeclareInclude);
            }
        }

        private void DeclareInclude(IPathSegment patternSegment, bool isLastSegment)
        {
            var literalSegment = patternSegment as LiteralPathSegment;
            if(literalSegment != null)
            {
                if(isLastSegment)
                {
                    _declaredLiteralFileSegments.Add(literalSegment);
                }
                else
                {
                    _declaredLiteralFolderSegments.Add(literalSegment);
                    _declaredLiteralFolderSegmentInString.Add(literalSegment.Value);
                }
            }
            else if(patternSegment is ParentPathSegment)
            {
                _declaredParentPathSegment = true;
            }
            else if(patternSegment is WildcardPathSegment)
            {
                _declaredWildcardPathSegment = true;
            }
        }

        private void PushDirectory(DirectoryInfoBase directory)
        {
            foreach (var context in _includePatternContexts)
            {
                context.PushDirectory(directory);
            }

            foreach(var context in _excludePatternContexts)
            {
                context.PushDirectory(directory);
            }
        }

        private void PopDirectory()
        {
            foreach(var context in _excludePatternContexts)
            {
                context.PopDirectory();
            }

            foreach(var context in _includePatternContexts)
            {
                context.PopDirectory();
            }
        }
    } 
}