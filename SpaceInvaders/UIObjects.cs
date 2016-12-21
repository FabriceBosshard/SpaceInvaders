using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SpaceInvaders
{
    class UIObjects
    {
        public static List<UIElement> InvaderList = new List<UIElement>();
        public static List<UIElement> LaserList = new List<UIElement>();
        public static List<UIElement> PlayerList = new List<UIElement>();
        public static List<UIElement> InvaderLaserList = new List<UIElement>();
        public static bool hasBeenHit;
        public static bool updatePoints;
        public static bool PlayerDie;
        public static bool newWave;
        public static bool PlayerHasBeenHit { get; set; }

        public static void CheckCollisionBetweenLaserPlayer(Canvas canvas)
        {
            for (int i = 0; i < InvaderList.Count; i++)
            {
                UIElement inv = InvaderList.ElementAt(i);
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
                if ((Math.Abs(Invaderposition.X - Laserposition.X) < 40) && (Math.Abs(Invaderposition.Y - Laserposition.Y) < 40))
                {
                    InvaderList.Remove(inv);
                    canvas.Children.Remove(inv);
                    hasBeenHit = true;
                    updatePoints = true;
                }
            }            
        }

        public static void CheckCollisionBetweenInvLaserPlayer(Canvas canvas)
        {
            
        }

        public static void checkInvaderCount()
        {
            if (InvaderList.Count == 0)
            {
                //Player wins / new Wave
                newWave = true;
                InvaderList.Clear();
                Thread.Sleep(1000);
            }
        }
    }
}
