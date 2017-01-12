using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SpaceInvaders
{
    class Player
    {
        private int lives = 3;
        private int bulletSpeed = 10;
        private Canvas canvas = null;
        private Point currentPositionLaser;
        public bool isShooting = false;
        public Image player;
        private Ellipse laser = Laser.CreateLaser();
        private Point position;
        readonly NotifyHandler _notifyHandler = NotifyHandler.InstanceCreation();

        private DispatcherTimer t;
        private DispatcherTimer LiveChecker;

        public Player()
        {
            LiveChecker = new DispatcherTimer { Interval = new TimeSpan(0,0,0,0,1) };
            LiveChecker.Tick += UpdateLives;
            LiveChecker.Start();            
        }

        private void UpdateLives(object sender, EventArgs e)
        {
            if (UIObjects.PlayerHasBeenHit)
            {
                _notifyHandler.Lives--;
                UIObjects.PlayerHasBeenHit = false;
                if (_notifyHandler.Lives == 0)
                {
                    Die();
                }

            }
        }

        private void Die()
        {
            UIObjects.GameOver = true;
            GameOver GO = new GameOver();
            GO.Show();            
        }

        public void ConfigureShoot(Canvas canvas, Image player)
        {
            isShooting = true;
            this.canvas = canvas;
            this.player = player;

            position.X = Canvas.GetLeft(player);
            position.Y = Canvas.GetTop(player);
           
            Canvas.SetTop(laser, position.Y);
            Canvas.SetLeft(laser, position.X + 50);
            canvas.Children.Add(laser);
            UIObjects.LaserList.Add(laser);
            _notifyHandler.Bullets--;

            if (_notifyHandler.Bullets == 0)
            {
                Die();
            }

            t = new DispatcherTimer {Interval = new TimeSpan(10000)};
            t.Tick += Shoot;
            t.Start();
        }
        
        public void Shoot(object sender, EventArgs e)
        {            
            position.Y -= bulletSpeed;
            Canvas.SetTop(laser, position.Y);
            Canvas.SetLeft(laser, position.X + 50);

            UIObjects.CheckCollisionBetweenLaserInvader(canvas);

            if (position.Y < 0 || UIObjects.hasBeenHit)
            {
                DeleteBullet();
            }
            
        }

        public void DeleteBullet()
        {
            canvas.Children.Remove(laser);
            UIObjects.LaserList.RemoveAt(0);
            isShooting = false;
            UIObjects.hasBeenHit = false;
            t.Stop();          
        }
    }
}
