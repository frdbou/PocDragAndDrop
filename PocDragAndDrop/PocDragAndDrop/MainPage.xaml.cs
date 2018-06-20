using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PocDragAndDrop
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}



        public async void button1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());
        }

        public async void button2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page2());
        }
        public async void button3_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page3());
        }
    }
}
