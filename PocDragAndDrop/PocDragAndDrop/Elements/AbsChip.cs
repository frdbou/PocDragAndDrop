using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PocDragAndDrop.Elements
{
    public class AbsChip: ContentView
    {
        double x;
        double y;
        private double _xInitiale;
        private double _yInitiale;
        private bool _moved;

        #region bindable property

        public Color ChipColor
        {
            get => (Color)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public static readonly BindableProperty SizeProperty = BindableProperty.Create(propertyName: "ChipColor",
                                                                                                    returnType: typeof(Color),
                                                                                                    declaringType: typeof(AbsChip),
                                                                                                    defaultValue: Color.Transparent,
                                                                                                    propertyChanged: AbsChipColorPropertyChanged);

        private static void AbsChipColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AbsChip)bindable;
            if (control.Content != null)
            {
                control.Content.BackgroundColor = control.ChipColor;
            }
        }

        #endregion bindable property



        public AbsChip()
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

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += Move_chip;
            frame.GestureRecognizers.Add(panGesture);
            this.Content = frame;
          
            _moved = false;
        }


       
           public void Move_chip(object sender, PanUpdatedEventArgs e)
        {

            switch (e.StatusType)
            {
                case GestureStatus.Started:

                    View parent = (View)this.Parent;


                    if ((parent is Layout)&& ! _moved)
                    {

                        AbsChip aChip = new AbsChip();
                        aChip._moved = false;
                        AbsoluteLayout.SetLayoutBounds(aChip, new Rectangle(this.X, this.Y, this.Width, this.Height));
                        AbsoluteLayout.SetLayoutFlags(aChip, AbsoluteLayoutFlags.None);
                        aChip.Content.BackgroundColor = ((View)sender).BackgroundColor;
                        ((AbsoluteLayout)parent).Children.Add(aChip);
                       

                    }

                    _xInitiale = this.X;
                    _yInitiale = this.Y;
                    break;


                case GestureStatus.Running:
                    if (Device.RuntimePlatform==Device.Android)
                    {

                        x = x + e.TotalX;
                        y = y + e.TotalY;
                    }
                    else
                    {
                        x = e.TotalX+_xInitiale;
                        y = e.TotalY+_yInitiale;
                    }
                    AbsoluteLayout.SetLayoutBounds(this, new Rectangle(x, y, this.Width, this.Height));
                    AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.None);
                    break;

                case GestureStatus.Completed:

                    x = this.TranslationX;
                    y = this.TranslationY;

                    break;
            }

        }



    }
}
