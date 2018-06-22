using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace PocDragAndDrop.Elements
{
    public class AbsChip : ContentView, INotifyPropertyChanged
    {
        #region fields

        private double x;
        private double y;
        private double _xInitiale;
        private double _yInitiale;
        private bool _moved;
        private double XMaximum;
        private double YMaximum;

        #endregion fields

        #region Property

        private bool isInDraggableZone;
        public bool IsInDraggableZone
        {
            get { return isInDraggableZone; }
            set
            {
                isInDraggableZone = value;
                OnPropertyChanged("IsInDraggableZone");
            }
        }


        #endregion Property

        #region bindable property

        public Color ChipColor
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(propertyName: "ChipColor",
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

        public double XMinimun
        {
            get => (double)GetValue(XMinimunProperty);
            set => SetValue(XMinimunProperty, value);
        }

        public static readonly BindableProperty XMinimunProperty = BindableProperty.Create(propertyName: "XMinimun",
                                                                                                    returnType: typeof(double),
                                                                                                    declaringType: typeof(AbsChip),
                                                                                                    defaultValue: 0d,
                                                                                                    propertyChanged: XMinimunPropertyChanged);

        private static void XMinimunPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AbsChip)bindable;
            if (control.DraggableZoneWidth != 0)
            {
                control.XMaximum = control.XMinimun + control.DraggableZoneWidth;
            }
        }

        public double YMinimun
        {
            get => (double)GetValue(YMinimunProperty);
            set => SetValue(YMinimunProperty, value);
        }

        public static readonly BindableProperty YMinimunProperty = BindableProperty.Create(propertyName: "YMinimun",
                                                                                                    returnType: typeof(double),
                                                                                                    declaringType: typeof(AbsChip),
                                                                                                    defaultValue: 0d,
                                                                                                    propertyChanged: YMinimunPropertyChanged);

        private static void YMinimunPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AbsChip)bindable;
            if (control.DraggableZoneHeigth != 0)
            {
                control.YMaximum = control.YMinimun + control.DraggableZoneHeigth;
            }
        }

        public double DraggableZoneWidth
        {
            get => (double)GetValue(DraggableZoneWidthProperty);
            set => SetValue(DraggableZoneWidthProperty, value);
        }

        public static readonly BindableProperty DraggableZoneWidthProperty = BindableProperty.Create(propertyName: "DraggableZoneWidth",
                                                                                                    returnType: typeof(double),
                                                                                                    declaringType: typeof(AbsChip),
                                                                                                    defaultValue: 0d,
                                                                                                    propertyChanged: DraggableZoneWidthPropertyChanged);

        private static void DraggableZoneWidthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AbsChip)bindable;

            control.XMaximum = control.XMinimun + control.DraggableZoneWidth;
        }

        public double DraggableZoneHeigth
        {
            get => (double)GetValue(DraggableZoneHeigthProperty);
            set => SetValue(DraggableZoneHeigthProperty, value);
        }

        public static readonly BindableProperty DraggableZoneHeigthProperty = BindableProperty.Create(propertyName: "DraggableZoneHeigth",
                                                                                                    returnType: typeof(double),
                                                                                                    declaringType: typeof(AbsChip),
                                                                                                    defaultValue: 0d,
                                                                                                    propertyChanged: DraggableZoneHeigthPropertyChanged);

        private static void DraggableZoneHeigthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AbsChip)bindable;

            control.YMaximum = control.YMinimun + control.DraggableZoneHeigth;
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

        /// <summary>
        /// Cette méthode copie les propriété de l'objet passé en argument à l'objet courant.
        /// </summary>
        /// <param name="first"></param>
        public void SetProperties(AbsChip first)
        {
            Content.BackgroundColor = first.BackgroundColor;
            this.XMinimun = first.XMinimun;
            this.YMinimun = first.YMinimun;
            this.DraggableZoneHeigth = first.DraggableZoneHeigth;
            this.DraggableZoneWidth = first.DraggableZoneWidth;
        }

        public void Move_chip(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:

                    View parent = (View)this.Parent;

                    if ((parent is Layout) && !_moved)
                    {
                        AbsChip aChip = new AbsChip();
                        aChip.SetProperties(this);
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
                    if (!IsInDraggableZone)
                    {

                        if (Device.RuntimePlatform == Device.Android)
                        {
                            x = Math.Min(Math.Max(0, x + e.TotalX), App.ScreenWidth - this.Width);
                            y = Math.Min(Math.Max(0, y + e.TotalY), App.ScreenHeight - this.Height);
                        }
                        else
                        {
                            x = Math.Min(Math.Max(0, e.TotalX + _xInitiale), App.ScreenWidth - this.Width);
                            y = Math.Min(Math.Max(0, e.TotalY + _yInitiale), App.ScreenHeight - this.Height);
                        }
                    CheckIsInDraggableZone();
                    }
                    else
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            x = Math.Min(Math.Max(XMinimun, x + e.TotalX), XMaximum - this.Width);
                            y = Math.Min(Math.Max(YMinimun, y + e.TotalY), YMaximum - this.Height);
                        }
                        else
                        {
                            x = Math.Min(Math.Max(XMinimun, e.TotalX + _xInitiale), XMaximum - this.Width);
                            y = Math.Min(Math.Max(YMinimun, e.TotalY + _yInitiale), YMaximum - this.Height);
                        }
                    }

                    AbsoluteLayout.SetLayoutBounds(this, new Rectangle(x, y, this.Width, this.Height));
                    AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.None);
                    break;

                case GestureStatus.Completed:

                    x = this.X;
                    y = this.Y;
                    System.Diagnostics.Debug.WriteLine("X={0} Y={1}", x, y);

                    break;
                case GestureStatus.Canceled:
                    System.Diagnostics.Debug.WriteLine("FINIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII");
                    break;
            }
        }

        private void CheckIsInDraggableZone()
        {
            IsInDraggableZone = (x > XMinimun) && (x < XMaximum - this.Width) && (y > YMinimun) && (y < YMaximum - this.Height);

        }
    }
}