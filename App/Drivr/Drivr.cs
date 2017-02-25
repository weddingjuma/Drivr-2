using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClassLibrary.User;
using Drivr.Annotations;
using Drivr.API;
using Plugin.Settings;

namespace Drivr
{
    public class Drivr : INotifyPropertyChanged
    {
        private string _token;

        public bool IsAuthenticated { get; set; }
        public ApiWrapper ApiWrapper { get; set; }

        private User _currentUser;
        public User CurrentUser 
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); }
        }

        public Drivr()
        {
            LoadStartupSettings();
        }

        private void LoadStartupSettings()
        {
            _token = CrossSettings.Current.GetValueOrDefault<string>("access_token");
            ApiWrapper = new ApiWrapper(_token);

            if (!string.IsNullOrWhiteSpace(_token))
            {
                //var userTask = ApiWrapper.Get<User>("/api/User");
                //await userTask.ContinueWith(t =>
                //{
                //    CurrentUser = t.Result;
                //    IsAuthenticated = !EqualityComparer<User>.Default.Equals(CurrentUser, default(User));
                //});
            }
            else
            {
                IsAuthenticated = false;
            }
        }

        public async Task<string> Authenticate(string username, string password)
        {
            _token = await ApiWrapper.Authenticate(username, password);
            CrossSettings.Current.AddOrUpdateValue("access_token", _token);
            LoadStartupSettings();
            return _token;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}