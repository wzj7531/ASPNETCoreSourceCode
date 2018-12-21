namespace Microsoft.Extensions.FileSystemGlobbing.Internal
{
    public struct PatternTestResult
    {
        public static readonly PatternTestResult Failed = new PatternTestResult(isSuccessful:false,stem:null);

        private PatternTestResult(bool isSuccessful,string stem)
        {
            IsSuccessful = isSuccessful;
            Stem = stem;
        }

        public bool IsSuccessful{get;} 

        public string Stem{get;}      

        public static PatternTestResult Success(string stem)
        {
            return new PatternTestResult(isSuccessful:true,stem:stem);
        } 
    }
}