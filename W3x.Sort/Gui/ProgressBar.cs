namespace W3x.Sort.Gui
{
    using System;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// An ASCII progress bar
    /// This is copypaste from https://gist.github.com/DanielSWolf/0ab6a96899cc5377bf54
    /// with some changes
    /// </summary>
    public class ProgressBar : IDisposable, IProgress<ProgressBarMessage>
    {
        private const int BlockCount = 10;
        private const string Animation = @"|/—\";

        private readonly TimeSpan _animationInterval = TimeSpan.FromSeconds(1.0 / 8);     
        private readonly Timer _timer;

        private double _currentProgress;
        private string _currentText;
        private string _currentMessage = string.Empty;
        private bool _disposed;
        private int _animationIndex;

        public ProgressBar()
        {
            _timer = new Timer(TimerHandler);

            if (!Console.IsOutputRedirected)
            {
                ResetTimer();
            }
        }

        public void Report(ProgressBarMessage message)
        {
            var value = Math.Max(0, Math.Min(1, message.Value));
            Interlocked.Exchange(ref _currentProgress, value);
            _currentText = message.Text;
        }

        private void TimerHandler(object state)
        {
            lock (_timer)
            {
                if (_disposed) return;

                var progressBlockCount = (int) (_currentProgress * BlockCount);
                var percent = (int)(_currentProgress * 100);
                var text = string.Format("[{0}{1}] {2,3}% {3} {4}",
                    new string('#', progressBlockCount), 
                    new string('-', BlockCount - progressBlockCount),
                    percent,
                    Animation[_animationIndex++ % Animation.Length],
                    _currentText);
                UpdateText(text);

                ResetTimer();
            }
        }

        private void UpdateText(string text)
        {
            var commonPrefixLength = 0;
            var commonLength = Math.Min(_currentMessage.Length, text.Length);
            while (commonPrefixLength < commonLength && text[commonPrefixLength] == _currentMessage[commonPrefixLength])
            {
                commonPrefixLength++;
            }

            var outputBuilder = new StringBuilder();
            outputBuilder.Append('\b', _currentMessage.Length - commonPrefixLength);
            outputBuilder.Append(text.Substring(commonPrefixLength));

            var overlapCount = _currentMessage.Length - text.Length;
            if (overlapCount > 0)
            {
                outputBuilder.Append(' ', overlapCount);
                outputBuilder.Append('\b', overlapCount);
            }
            
            Console.Write(outputBuilder);
            _currentMessage = text;
        }

        private void ResetTimer()
        {
            _timer.Change(_animationInterval, TimeSpan.FromMilliseconds(-1));
        }

        public void Dispose()
        {
            lock (_timer)
            {
                _disposed = true;
                UpdateText(string.Empty);
            }
        }

    }
}
