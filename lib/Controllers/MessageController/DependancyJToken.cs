﻿using System.Windows;
using Newtonsoft.Json.Linq;

namespace Cloudsdale.lib.MessageController
{
    public class DependencyJToken : DependencyObject
    {
        public static DependencyProperty TokenProperty =
            DependencyProperty.Register("Token", typeof(JToken),
            typeof(DependencyJToken), new PropertyMetadata(new JObject()));

        public JToken Token
        {
            get { return (JToken)GetValue(TokenProperty); }
            set { SetValue(TokenProperty, value); }
        }
    }
}