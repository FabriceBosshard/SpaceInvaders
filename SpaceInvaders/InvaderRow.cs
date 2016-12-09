using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SpaceInvaders
{
    class InvaderRow
    {
        private double left = 60;
        private double top;

        private Point StartingPoint = new Point(40, 0);
        private List<UIElement> invaders = new List<UIElement>();

        private int InvaderWidth = 40;
        private int InvaderHeight = 40;
        private int invadercount;
        DispatcherTimer t = new DispatcherTimer();
        Invaders inv = new Invaders();
        

        public InvaderRow(Canvas canvas)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 6; i++)
                {
                    UIElement Body = new Image
                    {
                        Width = InvaderWidth,
                        Height = InvaderHeight,
                        Source = new BitmapImage(new Uri("invader2.gif", UriKind.Relative))
                    };

                    if (invadercount == 0)
                    {
                        Canvas.SetTop(Body, StartingPoint.Y);
                        Canvas.SetLeft(Body, StartingPoint.X);
                        canvas.Children.Add(Body);                   
                    }
                    else
                    {
                        Canvas.SetTop(Body, StartingPoint.Y + top);
                        Canvas.SetLeft(Body, StartingPoint.X + (i * left));
                        canvas.Children.Add(Body);
                    }
                    invaders.Add(Body);
                    invadercount++;
                }
                top += 50;
            }
            t.Interval = new TimeSpan(0,0,0,0,800);
            t.Tick += InvaderRow_Tick;
            t.Start();
        }

        private void InvaderRow_Tick(object sender, EventArgs e)
        {           
            inv.CheckCollision(invaders);
        }
    }
}
