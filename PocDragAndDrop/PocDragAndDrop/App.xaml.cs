﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PocDragAndDrop
{
	public partial class App : Application
	{
        public static double ScreenWidth { get; set; }
        public static double ScreenHeight { get; set; } 

		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new PocDragAndDrop.MainPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
