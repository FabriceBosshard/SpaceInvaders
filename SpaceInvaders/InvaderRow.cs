using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SpaceInvaders
{
    class InvaderRow
    {
        private double left = 60;
        private double top;

        private Point OldPosition;
        bool isBorder = false;
        private bool hasTurned;

        private Point StartingPoint = new Point(40, 0);
        private List<UIElement> invaders = new List<UIElement>();
        private int InvaderWidth = 40;
        private int InvaderHeight = 40;
        private int invadercount;
        private Canvas canvas;
        DispatcherTimer t = new DispatcherTimer();

        private int lives = 1;
        private int hitPoints = 10;

        public InvaderRow()
        {

        }

        public void Die(UIElement inv)
        {
            canvas.Children.Remove(inv);

        }

        public void DecrementLives(UIElement inv)
        {
            lives--;
            if (lives == 0)
            {
                Die(inv);
            }
        }

        public InvaderRow(Canvas canvas)
        {
            this.canvas = canvas;
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 8; i++)
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
            CheckCollision(invaders);

        }
        
        public bool CheckCollisionWithBorderRight(UIElement inv)
        {
            if (Canvas.GetLeft(inv) > 730)
            {
                return true;
            }
            return false;
        }

        public bool CheckCollisionWithBorderLeft(UIElement inv)
        {
            if (Canvas.GetLeft(inv) < 40)
            {
                return true;
            }
            return false;
        }

        bool direction = false;

        public void moveInvader(List<UIElement> invaders)
        {
            foreach (UIElement inv in invaders)
            {
                OldPosition.X = Canvas.GetLeft(inv);
                OldPosition.Y = Canvas.GetTop(inv);

                if (direction)
                {
                    Canvas.SetLeft(inv, OldPosition.X - 35);

                }
                else
                {
                    Canvas.SetLeft(inv, OldPosition.X + 35);

                }
            }
            hasTurned = false;
            isBorder = false;
        }

        public void CheckCollision(List<UIElement> invaders)
        {
            foreach (UIElement inv in invaders)
            {

                if (CheckCollisionWithBorderLeft(inv))
                {
                    isBorder = true;
                    direction = false;
                }
                else if (CheckCollisionWithBorderRight(inv))
                {
                    isBorder = true;
                    direction = true;
                }
            }
            if (isBorder && !hasTurned)
            {
                oneDown(invaders);

            }
            else
            {
                moveInvader(invaders);
            }
        }

        public void oneDown(List<UIElement> invaders)
        {
            foreach (var inv in invaders)
            {
                OldPosition.X = Canvas.GetLeft(inv);
                OldPosition.Y = Canvas.GetTop(inv);

                Canvas.SetTop(inv, OldPosition.Y + 50);
                Canvas.SetLeft(inv, OldPosition.X);
            }
            hasTurned = true;
        }
    }
}
