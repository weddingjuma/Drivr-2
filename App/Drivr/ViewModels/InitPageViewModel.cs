using System.ComponentModel;
using System.Runtime.CompilerServices;
using Drivr.Annotations;

namespace Drivr.ViewModels
{
    public class InitPageViewModel : INotifyPropertyChanged
    {
        private double _progress;

        public double Progress
        {
            get { return _progress; }
            set { _progress = value; OnPropertyChanged(nameof(Progress)); }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        public InitPageViewModel()
        {
            Status = "Initializing";
            Progress = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
