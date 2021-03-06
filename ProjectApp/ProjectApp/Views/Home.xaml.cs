﻿using ProjectApp.Database;
using ProjectApp.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace ProjectApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class Home : ContentPage
    {
        ArduinoHandler arduinoHandler = ArduinoHandler.Handler;

        public Home()
        {
            InitializeComponent();

            BindingContext = arduinoHandler.Status;
            arduinoHandler.StatusRefreshedEvent += RefreshGUI;
        }

        /// <summary>
        /// Event handler that gets fired everytime the ArduinoHandler periodically refreshes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefreshGUI(object sender, EventArgs e)
        {

            if (arduinoHandler.IsConnected())
            {
                ButtonUnlock.IsEnabled = arduinoHandler.Status.BoxStatus == "Locked";
                ButtonLock.IsEnabled = arduinoHandler.Status.BoxStatus == "Unlocked";
            }

            else
            {
                ButtonUnlock.IsEnabled = ButtonLock.IsEnabled = false;
            }
        }

        /// <summary>
        /// Event handler for when the unlock button is tapped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUnlock_Clicked(object sender, EventArgs e)
        {
            arduinoHandler.UnlockPackageBox();
        }

        /// <summary>
        /// Event handler for when the lock button is tapped. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLock_Clicked(object sender, EventArgs e)
        {
            arduinoHandler.LockPackageBox();
        }
    }
}