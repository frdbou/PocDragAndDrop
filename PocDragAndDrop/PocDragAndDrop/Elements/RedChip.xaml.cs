using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocDragAndDrop.Elements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RedChip : ContentView
    {
        private double x;
        private double y;
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
                                                                                                    declaringType: typeof(RedChip),
                                                                                                    defaultValue: Color.Transparent,
                                                                                                    propertyChanged: ChipColorPropertyChanged);

        private static void ChipColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RedChip)bindable;
            if (control.Content != null)
            {
                control.Content.BackgroundColor = control.ChipColor;
            }
        }

        #endregion bindable property

        public RedChip()
        {
            InitializeComponent();
            
           _xInitiale = this.TranslationX;
            _yInitiale = this.TranslationY;
            x = _xInitiale;
            y = _yInitiale;
        }

        public void Move_chip(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:

                    View parent = (View)this.Parent;

                    if ((parent is Layout) && (this.TranslationX == _xInitiale) && (this.TranslationY == _yInitiale))
                    {
                        RedChip aRedChip = new RedChip();
                        aRedChip.TranslateTo(0, 0, 1);
                        aRedChip.Content.BackgroundColor = ((View)sender).BackgroundColor;
                        ((AbsoluteLayout)parent).Children.Add(aRedChip);
                    }
                    break;

                case GestureStatus.Running:

                    x = x + e.TotalX;
                    y = y + e.TotalY;

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