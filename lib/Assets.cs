﻿using System;
using System.IO;
using System.Drawing;

namespace Cloudsdale.lib
{
    public sealed class Assets
    {
        #region Color list
        public static Color PrimaryBlue = Color.FromArgb(99, 168, 208);
        public static Color PrimaryBlue_Dark = Color.FromArgb(63, 133, 179);
        public static Color PrimaryText = Color.FromArgb(77, 77, 77);
        public static Color PrimaryBackground = Color.FromArgb(250, 250, 250);
        public static Color InnerContent = Color.FromArgb(34, 34, 34);
        public static Color InnerBackground = Color.White;
        public static Color Error_Bright = Color.FromArgb(191, 94, 91);
        public static Color Error_Dark = Color.FromArgb(183, 76, 73);
        public static Color Success_Bright = Color.FromArgb(128, 206, 0);
        public static Color Success_Dark = Color.FromArgb(112, 180, 0);

        public static Color FounderTag = Color.FromArgb(255, 183, 230);
        public static Color DevTag = Color.FromArgb(142, 60, 255);
        public static Color AdminTag = Color.FromArgb(99, 151, 63);
        public static Color AssociateTag = Color.FromArgb(110, 110, 167);
        public static Color DonatorTag = Color.FromArgb(220, 206, 70);
        public static Color LegacyTag = Color.FromArgb(160, 160, 160);
        public static Color VerifiedTag = Color.FromArgb(40, 40, 250);

        public static Color OnlineStatus = Color.LightGreen;
        public static Color OfflineStatus = Color.LightGray;
        public static Color AwayStatus = Color.Yellow;
        public static Color BusyStatus = Color.Red;
        #endregion
        #region Folders

        public static string BaseDirectory = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Cloudsdale\";
        public static string InstallDirectory = BaseDirectory + @"cloudsdale-win7\";
        public static string Library = InstallDirectory + @"lib\";
        public static string DataDirectory = BaseDirectory + @"data\";

        #endregion
    }
}