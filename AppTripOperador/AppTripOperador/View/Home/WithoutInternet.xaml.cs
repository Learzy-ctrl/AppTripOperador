﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripOperador.View.Home
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WithoutInternet : ContentView
	{
		public WithoutInternet ()
		{
			InitializeComponent ();
		}
	}
}