using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocDragAndDrop
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page2 : ContentPage
	{
        double x, y;

		public Page2 ()
		{
			InitializeComponent ();
		}
        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {

                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    //Content.TranslationX = Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(Content.Width - this.Width));
                    //Content.TranslationY = Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs(Content.Height - this.Height));
                    Content.TranslationX =  e.TotalX;
                    Content.TranslationY =  e.TotalY;
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    x = Content.TranslationX;
                    y = Content.TranslationY;
                    break;
            }
        }
    }
}