using SchoolProject.Models;
using SchoolProject.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SchoolProject.ViewModel
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _UserName;
        private string _Password;
        private string _isVisible = "Hidden";
        public ICommand LoginCommand { get; }
        public string Username
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(_isVisible));
            }
        }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private async void Login()
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                User user = await userRepository.GetUser(Username, Password);
                if (user != null && user.UserName != string.Empty && user.Password != string.Empty)
                {

                }
                else
                    _isVisible = "Visibility";
                    

            }
            catch (Exception error)
            {
                MessageBox.Show($"Error in login: {error}");
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
