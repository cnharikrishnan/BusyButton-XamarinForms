using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BusyButtonDemo.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Fields

        private bool isValidLogin;
        private bool isBusy;

        #endregion

        #region Constructor

        public ViewModel()
        {
            this.Username = null;
            this.Password = null;

            this.IsBusy = false;
            this.LoginCommand = new Command(ExecuteLoginCommand);
        }

        #endregion

        #region Property

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsValidLogin
        {
            get { return this.isValidLogin; }
            set
            {
                this.isValidLogin = value;
                OnPropertyChanged("IsValidLogin");
            }
        }

        public bool IsBusy
        {
            get { return this.isBusy; }
            set
            {
                this.isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public Command LoginCommand { get; set; }

        #endregion

        #region CallBack

        private async void ExecuteLoginCommand(object obj)
        {
            IsBusy = true;
            await Task.Delay(2500);
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                IsValidLogin = true;
            }
            IsBusy = false;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
