using System;
using System.Diagnostics;
using System.Timers;
using AncertCronometro.DomainAPI;

namespace AncertCronometro.Domain
{
    public class Cronometro : IDisposable, ICronometro
    {
        private readonly Timer _timerNotifyTimeChanged;
        private readonly Stopwatch _stopwatch;

        public bool IsRunning => _stopwatch.IsRunning;
        public TimeSpan CurrentTime => _stopwatch.Elapsed;

        public event EventHandler? TimeChanged;

        public Cronometro()
        {
            _stopwatch = new Stopwatch();

            _timerNotifyTimeChanged = new Timer(500);
            _timerNotifyTimeChanged.Elapsed += TimerNotifyTimeChanged_Elapsed;
        }

        public void Start()
        {
            _stopwatch.Start();
            _timerNotifyTimeChanged.Start();

        }
        public void Pause()
        {
            _timerNotifyTimeChanged.Stop();
            _stopwatch.Stop();
            OnSecondsChanged();
        }
        public void Stop()
        {
            _timerNotifyTimeChanged.Stop();
            _stopwatch.Reset();
            OnSecondsChanged();
        }

        private void TimerNotifyTimeChanged_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnSecondsChanged();
        }

        private void OnSecondsChanged()
        {
            TimeChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            _timerNotifyTimeChanged.Dispose();
        }
    }
}
