using System;

namespace AncertCronometro.DomainAPI
{
    public interface ICronometro
    {
        bool IsRunning { get; }
        public TimeSpan CurrentTime { get; }

        event EventHandler? TimeChanged;

        void Pause();
        void Start();
        void Stop();
    }
}