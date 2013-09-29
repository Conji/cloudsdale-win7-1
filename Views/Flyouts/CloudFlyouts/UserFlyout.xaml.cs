﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7.Views.Flyouts.CloudFlyouts
{
    /// <summary>
    /// Interaction logic for UserFlyout.xaml
    /// </summary>
    public partial class UserFlyout
    {

        private static User Self { get; set; }
        private static Cloud FoundOn { get; set; }

        public UserFlyout(User user, Cloud cloud)
        {
            InitializeComponent();
            Self = user;
            FoundOn = cloud;
            AvatarBounce();
            Username.Text = "@" + user.Username;
            Name.Text = user.Name;
            AviImage.Source = new BitmapImage(user.Avatar.Normal);
            CheckIfSubbed();
        }

        private void AvatarBounce()
        {
            var a = new ThicknessAnimation(new Thickness(10, -800, 0, 810), new Thickness(10, 10, 0, 0),
                                           new Duration(new TimeSpan(0, 0, 1)));
            var bounce = new BounceEase();
            bounce.Bounces = 3;
            bounce.Bounciness = 10;
            a.EasingFunction = bounce;
            Avi.BeginAnimation(MarginProperty, a);
        }

        private void AddOnSkype(object sender, RoutedEventArgs e)
        {
            UIHelpers.MessageOnSkype(Self.SkypeName ?? Self.SkypeName);
        }

        private void UpdateSubscription(object sender, RoutedEventArgs e)
        {
            var subModel = new Subscription(SubscriptionType.User, Self.Id);
            App.Connection.SubscriptionController.AddSubscription(subModel);
        }
        private void CheckIfSubbed()
        {
            if (App.Connection.SubscriptionController.SubscribedUsers.Contains(new Subscription{SubscriptionType = SubscriptionType.User, ModelId = Self.Id}))
            {
                SubscriptionCheck.IsChecked = true;
            }
        }

        private void Unsubscribe(object sender, RoutedEventArgs e)
        {
            App.Connection.SubscriptionController.RemoveSubscription(new Subscription
            {
                ModelId = Self.Id,
                SubscriptionType = SubscriptionType.User
            });
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Main.Instance.ShowFlyoutMenu(this);
        }
    }
}