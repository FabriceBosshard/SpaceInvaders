using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SpaceInvaders
{
    static class UIObjects
    {
        public static List<UIElement> InvaderList = new List<UIElement>();
        public static List<UIElement> LaserList = new List<UIElement>();
        public static List<UIElement> PlayerList = new List<UIElement>();
        public static bool hasBeenHit;

        public static void CheckCollisionBetweenLaserPlayer(Canvas canvas)
        {
            foreach (UIElement inv in InvaderList)
            {
                Point Invaderposition = new Point()
                {
                    X = Canvas.GetLeft(inv),
                    Y = Canvas.GetTop(inv)
                };
                Point Laserposition = new Point()
                {
                    X = Canvas.GetLeft(LaserList.ElementAt(0)),
                    Y = Canvas.GetTop(LaserList.ElementAt(0))
                };
                //Fromel Stimmt noch nicht genau
                if ((Math.Abs(Invaderposition.X - Laserposition.X) < 10) && (Math.Abs(Invaderposition.Y - Laserposition.Y) < 10))
                {
                    canvas.Children.Remove(inv);
                    hasBeenHit = true;
                }
                
            }
            
        }
    }
}
