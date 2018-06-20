﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocDragAndDrop.Elements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RedChip : ContentView
    {
        double x;
        double y;
        private double _xInitiale;
        private double _yInitiale;

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
                       
                        ((AbsoluteLayout)parent).Children.Add(new RedChip
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