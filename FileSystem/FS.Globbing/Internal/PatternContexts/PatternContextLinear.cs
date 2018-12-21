using System;
using System.Collections.Generic;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts
{
    public abstract class PatternContextLinear : PatternContext<PatternContextLinear.FrameData>
    {
        public PatternContextLinear(IlinearPattern pattern)
        {
            Pattern = pattern;
        }

        public override PatternTestResult Test(Abstractions.FileInfoBase file)
        {
            if(IsStackEmtpy())
            {
                throw new InvalidOperationException("Can't test file befor entering a directory.");
            }
            
            if(!Frame.IsNotApplication && IsLastSegment() && TestMatchingSegment(file.Name))
            {
                return PatternTestResult.Success(CalculateStem(file));
            }

            return PatternTestResult.Failed;
        }

        public override void PushDirectory(DirectoryInfoBase directory)
        {
            var frame = Frame;

            if(IsStackEmtpy() || Frame.IsNotApplication)
            {

            }
            else if(!TestMatchingSegment(directory.Name))
            {
                frame.IsNotApplication = true;
            }
            else
            {
                var segment = Pattern.Segments[Frame.SegmentIndex];
                if(frame.InStem || segment.CanProduceStem)
                {
                    frame.InStem = true;
                    frame.StemItems.Add(directory.Name);
                }

                frame.SegmentIndex = frame.SegmentIndex + 1;
            }

            PushDataFrame(frame);
        }

        public struct FrameData
        {
            public bool IsNotApplication;
            public int SegmentIndex;
            public bool InStem;
            private IList<string> _stemItems;

            public IList<string> StemItems
            {
                get{return _stemItems ?? (_stemItems = new List<string>());}
            }

            public string Stem
            {
                get {return _stemItems == null ? null:string.Join("/",_stemItems);}
            }
        }

        protected bool IsLastSegment()
        {
            return Frame.SegmentIndex == Pattern.Segments.Count - 1;
        }

        protected bool TestMatchingSegment(string value)
        {
            if(Frame.SegmentIndex >= Pattern.Segments.Count)
            {
                return false;
            }
            return Pattern.Segments[Frame.SegmentIndex].Match(value);
        }

        protected string CalculateStem(FileInfoBase matchedFile)
        {
            
        }

        protected IlinearPattern Pattern { get; }

        
    }
}