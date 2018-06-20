using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PocDragAndDrop.Elements
{
    public class RoundSticker:Frame 
    {
        #region properties
        double x;
        double y;
        private double _xInitiale;
        private double _yInitiale;
        #endregion properties



        #region bindable property
        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public static readonly BindableProperty  SizeProperty = BindableProperty.Create(propertyName: "Size",
                                                                                                    returnType: typeof(double),
                                                                                                    declaringType: typeof(RoundSticker),
                                                                                                    defaultValue: 40d,
                                                                                                    propertyChanged: SizePropertyChanged);


        private static void SizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RoundSticker)bindable;
            control.Load();
        }

        #endregion bindable property



        private void Load()
        {
            MinimumHeightRequest=Size;
            MinimumWidthRequest = Size;
            HeightRequest = Size;
            WidthRequest = Size;
            CornerRadius = (float)Size / 2;
            
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += Move_sticker;
            GestureRecognizers.Add(panGesture);

        }
        private void Load(double size)
        {
            Size = size;
            MinimumHeightRequest = Size;
            HeightRequest = Size;
            WidthRequest = Size;
            MinimumWidthRequest = Size;
            CornerRadius = (float)Size / 2;

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += Move_sticker;
            GestureRecognizers.Add(panGesture);

        }


        public void Move_sticker(object sender, PanUpdatedEventArgs e)
        {

            switch (e.StatusType)
            {
                case GestureStatus.Started:

                    View parent = (View)this.Parent;


                    if ((parent is Layout) && (this.TranslationX == _xInitiale) && (this.TranslationY == _yInitiale))
                    {
                        RoundSticker newOne = new RoundSticker
                        {
                            TranslationX = 0,
                            TranslationY = 0
                        };
                        newOne.Load(Size);
                        newOne.BackgroundColor = this.BackgroundColor;
                       // ((AbsoluteLayout)parent).Children.Add(newOne);
                        //System.Diagnostics.Debug.WriteLine(((AbsoluteLayout)parent).Children.Count);
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


        public RoundSticker()
        {
            _xInitiale = this.TranslationX;
            _yInitiale = this.TranslationY;
            x = _xInitiale;
            y = _yInitiale;
            Load();
        }
    }
}
