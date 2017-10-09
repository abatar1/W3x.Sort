namespace W3x.Sort.W3x
{
    public class WRequest
    {
        public WRequest(string filepath, int count, string key = null)
        {
            Filepath = filepath;
            Key = key;
            Count = count;
        }

        public string Filepath { get; }
        public string Key { get; }
        public int Count { get; }
    }
}
