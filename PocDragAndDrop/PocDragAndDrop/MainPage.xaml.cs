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


        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            App.ScreenWidth = width;
            App.ScreenHeight = height;
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
        public async void button4_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page4());
        }
        public async void button5_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page5());
        }
    }
}
