﻿using System;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using CloudsdaleWin7.Views;
using CloudsdaleWin7.Views.LoadingViews;
using CloudsdaleWin7.lib.CloudsdaleLib;

namespace CloudsdaleWin7 {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login
    {
        public static Login Instance;
        public static bool LoggingOut = false;

        public Login()
        {
           
            Instance = this;
            InitializeComponent();
            EmailBox.Text = UserSettings.Default.PreviousEmail;
            PasswordBox.Password = UserSettings.Default.PreviousPassword;
        }

        public void Logout()
        {
            LoggingOut = true;
            EmailBox.Text = UserSettings.Default.PreviousEmail;
        }

        private void ClearText(object sender, MouseButtonEventArgs e)
        {
            if (EmailBox.Text == "Email")
            {
                EmailBox.Text = "";
                PasswordBox.Password = "";
            }
        }

        private async void LoginAttempt(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (String.IsNullOrEmpty(EmailBox.Text) || String.IsNullOrEmpty(PasswordBox.Password))
            {
                ShowMessage("You can't have empty fields, silly filly. Try again.");
                return;
            }
            ErrorMessage.Text = "";
            LoginUi.Visibility = Visibility.Hidden;
            LoggingInUi.Visibility = Visibility.Visible;
           try
           {
               await App.Connection.SessionController.Login(EmailBox.Text, PasswordBox.Password);
               UserSettings.Default.PreviousEmail = EmailBox.Text;
               UserSettings.Default.Save();
           }catch (Exception ex)
           {
               ShowMessage(ex.Message);
               LoginUi.Visibility = Visibility.Visible;
               LoggingInUi.Visibility = Visibility.Hidden;
           }
        }

        public void ShowMessage(string message)
        {
            #region Show Message

            var board = new Storyboard();
            var animation = new DoubleAnimation(0.0, 100.0, new Duration(new TimeSpan(200000000)));
            board.Children.Add(animation);
            Storyboard.SetTargetName(animation, ErrorMessage.Name);
            Storyboard.SetTargetProperty(animation, new PropertyPath(OpacityProperty));
            ErrorMessage.Text = message;
            board.Begin(this);

            #endregion
            #region Hide Message

            //var leavingBoard = new Storyboard();
            //var leavingAnimation = new DoubleAnimation(100.0, 0.0, new Duration(new TimeSpan(1000000000)));
            //leavingBoard.Children.Add(leavingAnimation);
            //Storyboard.SetTargetName(leavingAnimation, ErrorMessage.Name);
            //Storyboard.SetTargetProperty(leavingAnimation, new PropertyPath(OpacityProperty));
            //leavingBoard.Begin(this);

            #endregion

        }
    }
}
