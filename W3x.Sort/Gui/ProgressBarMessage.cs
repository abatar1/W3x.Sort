namespace W3x.Sort.Gui
{
    public class ProgressBarMessage
    {
        public ProgressBarMessage(double value, string text)
        {
            Value = value;
            Text = text;
        }

        public double Value { get; }
        public string Text { get; }
    }
}
