using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClassLibrary.User;
using Drivr.Annotations;
using Drivr.API;
using Drivr.Helpers;
using Drivr.Models;
using Plugin.Settings;

namespace Drivr
{
    public class Drivr : INotifyPropertyChanged
    {
        private string _token;

        public bool IsAuthenticated { get; set; }
        public ApiWrapper ApiWrapper { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

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
                var user = AsyncHelpers.RunSync(() => ApiWrapper.Get<User>("api/User"));
                CurrentUser = user.Object;
                IsAuthenticated = !EqualityComparer<User>.Default.Equals(CurrentUser, default(User));
            }
            else
            {
                IsAuthenticated = false;
            }
        }

        public async void Authenticate(string username, string password)
        {
            var task = ApiWrapper.Authenticate(username, password);
            await task.ContinueWith(t =>
            {
                _token = t.Result.Object;
                CrossSettings.Current.AddOrUpdateValue("access_token", _token);
                LoadStartupSettings();
            });
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}