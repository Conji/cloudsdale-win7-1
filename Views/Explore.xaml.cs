﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CloudsdaleWin7.Views.ExploreViews;
using CloudsdaleWin7.lib;

namespace CloudsdaleWin7
{
    /// <summary>
    /// Interaction logic for Explore.xaml
    /// </summary>
    public partial class Explore
    {
        public Explore()
        {
            InitializeComponent();
            LoadPreviousSource();
        }

        private void LoadPreviousSource()
        {
            switch (App.Settings["selected_source"])
            {
                case "popular":
                    ExploreFrame.Navigate(new ExplorePopular());
                    CmdPopular.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
                case "recent":
                    ExploreFrame.Navigate(new ExploreRecent());
                    CmdRecent.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
                case "top":
                    ExploreFrame.Navigate(new ExploreTop());
                    CmdTop.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
                case "roulette":
                    ExploreFrame.Navigate(new ExploreRoulette());
                    CmdRoulette.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
                default:
                    ExploreFrame.Navigate(new ExplorePopular());
                    CmdPopular.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
            }
        }

        private void ChangeExploreSource(object sender, RoutedEventArgs e)
        {
            foreach (Button button in ViewPanel.Children)
            {
                button.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlue);
            }
            var senderName = ((Button) sender).Name;
            switch (senderName)
            {
                case "CmdPopular":
                    App.Settings.ChangeSetting("selected_source", "popular");
                    CmdPopular.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
                case "CmdRecent":
                    App.Settings.ChangeSetting("selected_source", "recent");
                    CmdRecent.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
                case "CmdTop":
                    App.Settings.ChangeSetting("selected_source", "top");
                    CmdTop.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;
                case "CmdRoulette":
                    App.Settings.ChangeSetting("selected_source", "roulette");
                    CmdRoulette.BorderBrush = new SolidColorBrush(CloudsdaleSource.PrimaryBlueDark);
                    break;

            }
        }
    }
}
