using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PocDragAndDrop.Elements
{
   public  class Chip:Frame
    {
        double x;
        double y;
        private double _xInitiale;
        private double _yInitiale;


        public Chip()
        {
            
           
            _xInitiale = this.TranslationX;
            _yInitiale = this.TranslationY;
            x = _xInitiale;
            y = _yInitiale;
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += Move_chip;
            GestureRecognizers.Add(panGesture);
        }





        public void Move_chip(object sender, PanUpdatedEventArgs e)
        {

            switch (e.StatusType)
            {
                case GestureStatus.Started:

                    View parent = (View)this.Parent;


                    if ((parent is Layout) && (this.TranslationX == _xInitiale) && (this.TranslationY == _yInitiale))
                    {

                        ((AbsoluteLayout)parent).Children.Add(new Chip
                        {
                            TranslationX = 0,
                            TranslationY = 0
                        });

                    }
                    break;


                case GestureStatus.Running:

                    x = Math.Max(0, x + e.TotalX);
                    y = Math.Max(0, y + e.TotalY);

                    this.TranslateTo(x, y, 1, Easing.SinInOut);
                    break;

                case GestureStatus.Completed:

                    x = this.TranslationX;
                    y = this.TranslationY;

                    break;
            }

        }

    }
}
