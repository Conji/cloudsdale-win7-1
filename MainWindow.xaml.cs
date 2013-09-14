﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Models;

namespace CloudsdaleWin7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public static MainWindow Instance;
        
        public int MaxCharacters
        {
            get { return (int) GetValue(MaxCharactersProperty); }
            set { SetValue(MaxCharactersProperty, value); }
        }

        public MainWindow()
        {
            Instance = this;
            ClientVersion.Validate();
            InitializeComponent();
            MainFrame.Navigate(new Login());
            App.Connection.MainFrame = MainFrame;
        }

        public static DependencyProperty MaxCharactersProperty =
            DependencyProperty.Register("MaxCharacters", typeof (int), typeof (MainWindow),
                                        new FrameworkPropertyMetadata(200));

        private void SaveSettings(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Settings.Save();
        }

    }
}