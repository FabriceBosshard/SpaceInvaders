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
        public static List<UIElement> BombList = new List<UIElement>();
        public static List<UIElement> PlayerList = new List<UIElement>();
        public static List<UIElement> InvaderLaserList = new List<UIElement>();
        public static List<UIElement> InvaderHitFromBomb = new List<UIElement>();
        public static bool hasBeenHit;
        public static bool updatePoints;
        public static bool newWave;
        public static bool bombHit;
        public static bool PlayerHasBeenHit { get; set; }

        public static bool GameOver;

        public static void CheckCollisionBetweenLaserInvader(Canvas canvas)
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
                
                if ((Math.Abs(Laserposition.X - (Invaderposition.X + 20))) < 20 && (Math.Abs(Laserposition.Y - (Invaderposition.Y - 20)) < 20))
                {
                    InvaderList.Remove(inv);
                    canvas.Children.Remove(inv);
                    hasBeenHit = true;
                    InvaderHitFromBomb.Add(inv);
                }
            }            
        }
        public static void CheckCollisionBetweenBombInvader(Canvas canvas)
        {
            for (int i = 0; i < InvaderList.Count; i++)
            {
                UIElement inv = InvaderList.ElementAt(i);
                Point Invaderposition = new Point()
                {
                    X = Canvas.GetLeft(inv),
                    Y = Canvas.GetTop(inv)
                };
                Point BombPosition = new Point()
                {
                    X = Canvas.GetLeft(BombList.ElementAt(0)),
                    Y = Canvas.GetTop(BombList.ElementAt(0))
                };

                if ((Math.Abs(BombPosition.X - (Invaderposition.X + 20))) < 60 && (Math.Abs(BombPosition.Y - (Invaderposition.Y - 20)) < 60))
                {
                    InvaderList.Remove(inv);
                    canvas.Children.Remove(inv);                    
                    InvaderHitFromBomb.Add(inv);
                }
            }       
        }

        public static void CheckCollisionBetweenInvLaserPlayer(Canvas canvas)
        {

            for (int i = 0; i < PlayerList.Count; i++)
            {
                UIElement pla = PlayerList.ElementAt(i);
                Point PlayerPosition = new Point()
                {
                    X = Canvas.GetLeft(pla),
                    Y = Canvas.GetTop(pla)
                };
                Point Laserposition = new Point()
                {
                    X = Canvas.GetLeft(InvaderLaserList.ElementAt(0)),
                    Y = Canvas.GetTop(InvaderLaserList.ElementAt(0))
                };
                if ((Math.Abs(Laserposition.X - (PlayerPosition.X + 50))) < 50 && (Math.Abs(Laserposition.Y -  PlayerPosition.Y) < 15))
                {
                    PlayerHasBeenHit = true;
                }
            }
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
