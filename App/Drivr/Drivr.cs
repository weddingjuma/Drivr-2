using System.Collections.Generic;
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
        public event PropertyChangedEventHandler PropertyChanged;

        private User _currentUser;
        public User CurrentUser 
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); }
        }

        public async Task<bool> InitAuthentication()
        {
            _token = CrossSettings.Current.GetValueOrDefault<string>("access_token");
            ApiWrapper = new ApiWrapper(_token);

            if (string.IsNullOrWhiteSpace(_token)) return false;
            var task = ApiWrapper.Get<User>("api/User");
            await task.ContinueWith(t =>
            {
                if (string.IsNullOrWhiteSpace(t.Result.Error) || !EqualityComparer<User>.Default.Equals(t.Result.Object, default(User)))  //TODO: set to EqualityComparer to true 
                {
                    //TODO log error
                    return false;
                }
                CurrentUser = t.Result.Object;
                return true;
            });
            return false;
        }

        public async void Authenticate(string username, string password)
        {
            var task = ApiWrapper.Authenticate(username, password);
            await task.ContinueWith(async t =>
            {
                _token = t.Result.Object;
                CrossSettings.Current.AddOrUpdateValue("access_token", _token);
                var auth = InitAuthentication();
                await auth.ContinueWith(r =>
                {
                    IsAuthenticated = r.Result;
                });
            });
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}