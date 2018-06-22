using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PocDragAndDrop.Elements
{
   public  class Chip: ContentView
    {
        double x;
        double y;
        private double _xInitiale;
        private double _yInitiale;

        #region bindable property

        public Color ChipColor
        {
            get => (Color)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public static readonly BindableProperty SizeProperty = BindableProperty.Create(propertyName: "ChipColor",
                                                                                                    returnType: typeof(Color),
                                                                                                    declaringType: typeof(Chip),
                                                                                                    defaultValue: Color.Transparent,
                                                                                                    propertyChanged: ChipColorPropertyChanged);

        private static void ChipColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (Chip)bindable;
            if (control.Content != null)
            {
                control.Content.BackgroundColor = control.ChipColor;
            }
        }

        #endregion bindable property


        public Chip()
        {
            Frame frame = new Frame
            {
                Padding = new Thickness(0),
                HeightRequest = 60,
                WidthRequest = 60,
                CornerRadius = 30,
                MinimumHeightRequest = 60,
                MinimumWidthRequest = 60,
                BackgroundColor = ChipColor
            };

            _xInitiale = this.TranslationX;
            _yInitiale = this.TranslationY;
            x = _xInitiale;
            y = _yInitiale;
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += Move_chip;
            System.Diagnostics.Debug.WriteLine(panGesture.TouchPoints);
            
            frame.GestureRecognizers.Add(panGesture);
            this.Content = frame;
        }





        public void Move_chip(object sender, PanUpdatedEventArgs e)
        {

            switch (e.StatusType)
            {
                case GestureStatus.Started:

                    View parent = (View)this.Parent;


                    if ((parent is Layout) && (this.TranslationX == _xInitiale) && (this.TranslationY == _yInitiale))
                    {

                        Chip aChip = new Chip();
                        aChip.TranslateTo(0, 0);
                        aChip.Content.BackgroundColor = ((View)sender).BackgroundColor;
                        ((AbsoluteLayout)parent).Children.Add(aChip);

                    }

                    break;


                case GestureStatus.Running:

                    if (Device.RuntimePlatform == Device.Android)
                    {

                        x = x + e.TotalX;
                        y = y + e.TotalY;
                    }
                    else
                    {
                        x = e.TotalX + _xInitiale;
                        y = e.TotalY + _yInitiale;
                    }

                    this.TranslateTo(x, y,42, Easing.Linear);
                    break;

                case GestureStatus.Completed:

                    x = this.TranslationX;
                    y = this.TranslationY;

                    break;
            }

        }

    }
}
