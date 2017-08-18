using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int bulletSpeed = 15;
        private int VBombSpeed = 4;
        private int SupernovaSpeed = 7;
        private Canvas canvas = null;
        private Point currentPositionLaser;
        public bool isShooting = false;
        public bool isShootingBomb = false;
        public bool isShootingVBomb;
        public bool isShootingSuperNova;
        public bool isShootingApokalypse;
        public int bombs;
        public Image player;
        private Ellipse laser = Laser.CreateLaser();
        private Ellipse SuperLaser = Laser.CreateBomb();
        private UIElement Vbomb = Laser.CreateVBomb();
        private UIElement supernova = Laser.CreateSupernova();
        private UIElement apokalypse = Laser.CreateApokalypse();
        private Point position;
        readonly NotifyHandler _notifyHandler = NotifyHandler.InstanceCreation();
        private readonly MainWindowViewModel _mh = MainWindowViewModel.InstanceCreation();

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

        #region Shoot

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

            t = new DispatcherTimer { Interval = new TimeSpan(10000) };
            t.Tick += Shoot;
            t.Start();
        }

        public void Shoot(object sender, EventArgs e)
        {
            if (!_mh.IsPaused)
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

        }

        public void DeleteBullet()
        {
            canvas.Children.Remove(laser);
            UIObjects.LaserList.RemoveAt(0);
            isShooting = false;
            UIObjects.hasBeenHit = false;
            t.Stop();
        }

        #endregion

        #region SuperLaser
        
        public void ConfigureSuperLaser(Canvas canvas, Image player)
        {
            if (_notifyHandler.Bombs != 0)
            {
                isShootingBomb = true;

                this.canvas = canvas;
                this.player = player;

                position.X = Canvas.GetLeft(player);
                position.Y = Canvas.GetTop(player);

                Canvas.SetTop(SuperLaser, position.Y);
                Canvas.SetLeft(SuperLaser, position.X + 50);
                canvas.Children.Add(SuperLaser);
                UIObjects.BombList.Add(SuperLaser);
                _notifyHandler.Bombs--;

                t = new DispatcherTimer { Interval = new TimeSpan(10000) };
                t.Tick += ShootSuperLaser;
                t.Start();
            }
        }

        private void ShootSuperLaser(object sender, EventArgs e)
        {
            if (!_mh.IsPaused)
            {
                position.Y -= bulletSpeed;
                Canvas.SetTop(SuperLaser, position.Y);
                Canvas.SetLeft(SuperLaser, position.X + 50);

                UIObjects.CheckCollisionBetweenSuperLaserInvader(canvas);

                if (position.Y < 0)
                {
                    DeleteSuperLaser();
                }
            }
        }

        private void DeleteSuperLaser()
        {
            canvas.Children.Remove(SuperLaser);
            UIObjects.BombList.Remove(SuperLaser);
            isShootingBomb = false;
            UIObjects.hasBeenHit = false;
            t.Stop();
        }

        #endregion

        #region VBomb

        public void ConfigureVBomb(Canvas playground, Image player)
        {
            if (_notifyHandler.VBombCount != 0)
            {
                isShootingVBomb = true;
                UIObjects.isShootingBomb = true;

                this.canvas = playground;
                this.player = player;

                position.X = Canvas.GetLeft(player);
                position.Y = Canvas.GetTop(player);

                Canvas.SetTop(Vbomb, position.Y - 100);
                Canvas.SetLeft(Vbomb, position.X - 50);
                canvas.Children.Add(Vbomb);
                UIObjects.BombList.Add(Vbomb);
                _notifyHandler.VBombCount--;

                t = new DispatcherTimer { Interval = new TimeSpan(10000) };
                t.Tick += ShootVBomb;
                t.Start();
            }
        }

        private void ShootVBomb(object sender, EventArgs e)
        {
            if (!_mh.IsPaused)
            {
                position.Y -= VBombSpeed;
                Canvas.SetTop(Vbomb, position.Y - 100);
                Canvas.SetLeft(Vbomb, position.X - 50);

                UIObjects.CheckCollisionBetweenVBombInvader(canvas);

                if (position.Y < 25 || UIObjects.hasBeenHit)
                {
                    DeleteVBomb();
                }
            }
        }

        private void DeleteVBomb()
        {
            canvas.Children.Remove(Vbomb);
            UIObjects.BombList.Remove(Vbomb);
            isShootingVBomb = false;
            UIObjects.hasBeenHit = false;
            UIObjects.isShootingBomb = false;
            t.Stop();
        }

        #endregion

        #region SuperNova

        public void ConfigureSuperNova(Canvas playground, Image image)
        {
            if (_notifyHandler.SupernovaCount != 0)
            {
                isShootingSuperNova = true;

                this.canvas = playground;
                this.player = image;

                position.X = Canvas.GetLeft(player);
                position.Y = Canvas.GetTop(player);

                Canvas.SetTop(supernova, position.Y-100);
                Canvas.SetLeft(supernova, position.X - 50);
                canvas.Children.Add(supernova);
                UIObjects.SpecialsList.Add(supernova);
                _notifyHandler.SupernovaCount--;

                t = new DispatcherTimer { Interval = new TimeSpan(10000) };
                t.Tick += ShootSuperNova;
                t.Start();
            }
        }

        private void ShootSuperNova(object sender, EventArgs e)
        {
            if (!_mh.IsPaused)
            {
                position.Y -= SupernovaSpeed;
                Canvas.SetTop(supernova, position.Y - 100);
                Canvas.SetLeft(supernova, position.X - 50);

                UIObjects.CheckCollisionBetweenSuperNovaInvader(canvas);

                if (position.Y < 65)
                {
                    DeleteSuperNova();
                }
            }
        }

        private void DeleteSuperNova()
        {
            canvas.Children.Remove(supernova);
            UIObjects.SpecialsList.Remove(supernova);
            isShootingSuperNova= false;
            UIObjects.hasBeenHit = false;
            t.Stop();
        }

        #endregion

        #region Apokalypse

        public void ConfigureApokalypse(Canvas playground, Image image)
        {
            if (_notifyHandler.Apokalypsecount != 0)
            {
                isShootingApokalypse = true;

                this.canvas = playground;
                this.player = image;

                position.X = Canvas.GetLeft(player);
                position.Y = Canvas.GetTop(player);

                Canvas.SetTop(apokalypse, position.Y-100);
                Canvas.SetLeft(apokalypse, position.X);
                canvas.Children.Add(apokalypse);
                UIObjects.SpecialsList.Add(apokalypse);
                _notifyHandler.Apokalypsecount--;

                t = new DispatcherTimer { Interval = new TimeSpan(10000) };
                t.Tick += ShootApokalypse;
                t.Start();
            }
        }

        private void ShootApokalypse(object sender, EventArgs e)
        {
            if (!_mh.IsPaused)
            {
                position.Y -= ApokalypseSpeed;
                Canvas.SetTop(apokalypse, position.Y - 100);
                Canvas.SetLeft(apokalypse, position.X);

                UIObjects.CheckCollisionBetweenApokalypseInvader(canvas);

                if (position.Y < 100 || UIObjects.hasBeenHit)
                {
                    DeleteApokalypse();
                }
            }
        }

        public double ApokalypseSpeed = 2;

        private void DeleteApokalypse()
        {
            canvas.Children.Remove(apokalypse);
            UIObjects.SpecialsList.Remove(apokalypse);
            isShootingApokalypse = false;
            UIObjects.hasBeenHit = false;
            t.Stop();
        }

        #endregion




    }
}
