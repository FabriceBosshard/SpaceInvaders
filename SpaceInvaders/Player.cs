using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
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


        private DispatcherTimer t;

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

            t = new DispatcherTimer {Interval = new TimeSpan(10000)};
            t.Tick += Shoot;
            t.Start();
        }

        public void Shoot(object sender, EventArgs e)
        {
            position.Y -= bulletSpeed;
            Canvas.SetTop(laser, position.Y);
            Canvas.SetLeft(laser, position.X + 50);

            UIObjects.CheckCollisionBetweenLaserPlayer(canvas);

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
            position.Y = 642;

        }

        public void Die()
        {

        }

        public void DecrementLives()
        {
            lives--;
            if (lives == 0)
            {
                Die();
            }
        }
    }
}
