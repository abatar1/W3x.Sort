namespace W3x.Sort.Domain
{
    public class ApplicationArguments
    {
        public string Key { get; set; }
        public Mode Mode { get; set; }
        public string[] Files { get; set; }
    }

    public enum Mode
    {
        All,
        ByName,
        Ignore
    }
}
