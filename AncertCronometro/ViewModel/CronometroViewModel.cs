using AncertCronometro.DomainAPI;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace AncertCronometro.ViewModel
{
    public class CronometroViewModel : ObservableObject
    {
        private readonly ICronometro _cronometro;

        const string TIME_FORMAT = @"hh\:\:mm\:\:ss";
        const string CONTINUE_TEXT = @"Continue";

        public string CurrentTime
        {
            get => _cronometro.CurrentTime.ToString(TIME_FORMAT);
        }
        public string StartPauseCommandCurrentText
        {
            get
            {
                if (_cronometro.IsRunning)
                    return nameof(_cronometro.Pause);
                else
                    return _cronometro.CurrentTime == TimeSpan.Zero ? nameof(_cronometro.Start) : CONTINUE_TEXT;
            }
        }

        public ICommand StartPauseCommand { get; }
        public ICommand StopCommand { get; }

        public CronometroViewModel(ICronometro cronometro)
        {
            _cronometro = cronometro;
            _cronometro.TimeChanged += _cronometro_TimeChanged;

            StartPauseCommand = new RelayCommand(() => StartPause());
            StopCommand = new RelayCommand(() => _cronometro.Stop());
        }

        private void StartPause()
        {
            if (_cronometro.IsRunning)
                _cronometro.Pause();
            else
                _cronometro.Start();

            OnPropertyChanged(nameof(StartPauseCommandCurrentText));
        }

        private void _cronometro_TimeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(CurrentTime));
            OnPropertyChanged(nameof(StartPauseCommandCurrentText));
        }

        public void Dispose()
        {
            _cronometro.TimeChanged -= _cronometro_TimeChanged;
        }
    }
}
