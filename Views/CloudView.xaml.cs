﻿using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cloudsdale_Win7.Views;
using Cloudsdale_Win7.Win7_Lib;
using Cloudsdale_Win7.Win7_Lib;
using Cloudsdale_Win7.MVVM;
using Newtonsoft.Json.Linq;

namespace Cloudsdale_Win7 {
    /// <summary>
    /// Interaction logic for CloudView.xaml
    /// </summary>
    public partial class CloudView {
        public JToken Cloud;
        public static CloudView Instance;

        public CloudView(JToken cloud)
        {
            Instance = this;
            Cloud = cloud;
            InitializeComponent();
            ChatMessages.Items.Clear();
            Title = (string)cloud["name"];
            DataContext = cloud;
            MessageSource.GetSource(Cloud).Messages.CollectionChanged += NewMessage;
            ChatMessages.ItemsSource = MessageSource.GetSource(Cloud).Messages;
            MaxCharContainer.DataContext = MainWindow.Instance;
            Dispatcher.BeginInvoke(new Action(ChatScroll.ScrollToBottom));
            ((DependencyJToken)Resources["Cloud"]).Token = cloud;
        }

        ~CloudView() {
            MessageSource.GetSource(Cloud).Messages.CollectionChanged -= NewMessage;
        }

        private void NewMessage(object sender, EventArgs e) {
            Dispatcher.BeginInvoke(new Action(ChatScroll.ScrollToBottom));
        }

        private void SendBoxEnter(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) return;
            if (string.IsNullOrWhiteSpace(InputBox.Text)) return;
                Send(InputBox.Text, (string)Cloud["id"]);
                InputBox.Text = "";
        }

        internal void Send(string message, string cloudId)
        {
            var dataObject = new JObject();
            dataObject["content"] = message;
            dataObject["client_id"] = FayeConnector.ClientID;
            dataObject["device"] = "desktop";
            var data = Encoding.UTF8.GetBytes(dataObject.ToString());
            var request = WebRequest.CreateHttp(Endpoints.CloudMessages.Replace("[:id]", cloudId));
            request.Accept = "application/json";
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Headers["X-Auth-Token"] = MainWindow.User["user"]["auth_token"].ToString();
            request.BeginGetRequestStream(ar => {
                var reqs = request.EndGetRequestStream(ar);
                reqs.Write(data, 0, data.Length);
                reqs.Close();
                request.BeginGetResponse(a => {
                    try {
                        var response = request.EndGetResponse(a);
                        response.Close();
                    } catch (Exception ex) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex);
                    }
                }, null);
            }, null);
        }

        private void DropUp(object sender, MouseButtonEventArgs e) {
            var drop = (JToken)((FrameworkElement)sender).DataContext;
            MainWindow.Instance.Frame.Navigate(new Browser());
            MainWindow.Instance.CloudList.SelectedIndex = -1;
            Browser.Instance.WebBrowser.Navigate((string)drop["url"]);
            Browser.Instance.WebAddress.Text = ((string) drop["url"]);
            
            Browser.Instance.BrowserPage.Width = MainWindow.Instance.Width;
        }

        private void DirectHome(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance.Frame.Navigate(new Home());
        }

        private void ShowSettings(object sender, MouseButtonEventArgs e)
        {
            var settings = new SettingsWindow();
            settings.Show();
        }

        private void ShowUserInfo(object sender, EventArgs e)
        {
            var block = (TextBlock) sender;
            var j = (JObject) block.DataContext;
            var user = new UserInfo();
            user.ShowUserInfo((JObject) j["author"]);
        }

        private void Quote(object sender, RoutedEventArgs e)
        {
            var obj = (MenuItem) sender;
            var text = obj.DataContext.ToString();
            InputBox.Text = "> " + text.Replace(@"\n", Environment.NewLine);
        }

        private void CheckIfTextIsMultiLine(object sender, TextChangedEventArgs e)
        {
            InputBox.Text.Replace(@"\n", Environment.NewLine);
            if (InputBox.LineCount != 1)
            {
                InputBox.MaxHeight *= 4;
                InputBox.Height *= InputBox.LineCount;
            }
        }
    }
}