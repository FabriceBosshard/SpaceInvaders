using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SpaceInvaders
{
    public class InvaderRow
    {
        private const double Left = 60;
        private const int _invaderWidth = 40;
        private readonly int _invaderHeight = 40;

        private readonly NotifyHandler _notifyHandler = NotifyHandler.InstanceCreation();
        private readonly TimeSpan _speed = new TimeSpan(0, 0, 0, 1, 0);
        private readonly Point _startingPoint = new Point(50, 0);
        private readonly Canvas _canvas;
        private bool _direction;
        private bool _hasTurned;
        private readonly int _hitPoints = 10;
        private int _invadercount;
        private int _invaderCountRows = 5;
        private int _invadersRows = 2;
        private bool _isBorder;
        private Point _oldPosition;
        private DispatcherTimer _t = new DispatcherTimer();
        private double _top;
        private int bulletSpeed = 5;
        private DispatcherTimer invT;
        private Ellipse laser;
        private Point InvaderShootPos;
        private bool isShooting;

        public InvaderRow(Canvas canvas)
        {
            _canvas = canvas;
            for (var j = 0; j < _invadersRows; j++)
            {
                for (var i = 0; i < _invaderCountRows; i++)
                {
                    UIElement Body = new Image
                    {
                        Width = _invaderWidth,
                        Height = _invaderHeight,
                        Source = new BitmapImage(new Uri("invader2.gif", UriKind.Relative))
                    };

                    if (_invadercount == 0)
                    {
                        Canvas.SetTop(Body, _startingPoint.Y);
                        Canvas.SetLeft(Body, _startingPoint.X);
                    }
                    else
                    {
                        Canvas.SetTop(Body, _startingPoint.Y + _top);
                        Canvas.SetLeft(Body, _startingPoint.X + i*Left);
                    }
                    canvas.Children.Add(Body);
                    UIObjects.InvaderList.Add(Body);
                    _invadercount++;
                }
                _top += 50;
            }
            _t.Interval = _speed;
            _t.Tick += InvaderRow_Tick;
            _t.Start();
        }

        public void CreateNewWave()
        {
            
            for (var y = 0; y < _invadersRows; y++)
            {
                for (var z = 0; z < _invaderCountRows; z++)
                {
                    UIElement Body = new Image
                    {
                        Width = _invaderWidth,
                        Height = _invaderHeight,
                        Source = new BitmapImage(new Uri("invader2.gif", UriKind.Relative))
                    };

                    if (_invadercount == 0)
                    {
                        Canvas.SetTop(Body, _startingPoint.Y);
                        Canvas.SetLeft(Body, _startingPoint.X);
                        _canvas.Children.Add(Body);
                    }
                    else
                    {
                        Canvas.SetTop(Body, _startingPoint.Y + _top);
                        Canvas.SetLeft(Body, _startingPoint.X + z*Left);
                        _canvas.Children.Add(Body);
                    }
                    UIObjects.InvaderList.Add(Body);
                    _invadercount++;
                }
                _top += 50;
            }
            _direction = false;
            _t = new DispatcherTimer();
            _t.Interval = _speed;
            _t.Tick += InvaderRow_Tick;
            _t.Start();
        }

        private void InvaderRow_Tick(object sender, EventArgs e)
        {
            if (!UIObjects.GameOver)
            {
                CheckCollision(UIObjects.InvaderList);
                CheckPositionInvaders(UIObjects.InvaderList);
                UIObjects.checkInvaderCount();
                UpdatePoints();
                CheckOnNewWave();
                InvaderShoot(UIObjects.InvaderList);
             }

        }

        private void CheckOnNewWave()
        {
            if (UIObjects.newWave)
            {
                UIObjects.InvaderList.Clear();
                _t.Stop();
                _top = 0;
                if (_notifyHandler.Waves == 19)
                {
                    _invadersRows = 5;
                    _invaderCountRows = 5;
                    _notifyHandler.Bullets = 100;
                    _notifyHandler.Lives = 3;
                }
                else if (_notifyHandler.Waves == 9)
                {
                    _invadersRows = 3;
                    _invaderCountRows = 5;
                    _notifyHandler.Bullets = 75;
                    _notifyHandler.Lives = 3;
                }
                else
                {
                    _invaderCountRows += 1;                   
                    _notifyHandler.Bullets = 50;
                }
                _notifyHandler.Waves += 1;
                UIObjects.newWave = false;
                CreateNewWave();
            }
        }

        private void InvaderShoot(List<UIElement> invaders)
        {
            if (isShooting == false)
            {
                Random random = new Random();
                int rnd = random.Next(0, invaders.Count); 
                UIElement inv = invaders.ElementAt(rnd);

                laser = InvaderBombs.CreateLaser();

                InvaderShootPos.Y = Canvas.GetTop(inv);
                InvaderShootPos.X = Canvas.GetLeft(inv);

                Canvas.SetTop(laser, InvaderShootPos.Y + 5);
                Canvas.SetLeft(laser, InvaderShootPos.X + 20);
                _canvas.Children.Add(laser);
                UIObjects.InvaderLaserList.Add(laser);

                invT = new DispatcherTimer { Interval = new TimeSpan(10000) };
                invT.Tick += Shoot;
                invT.Start();
                isShooting = true;
            }


        }

        private void Shoot(object sender, EventArgs e)
        {
            InvaderShootPos.Y += bulletSpeed;

            
            Canvas.SetLeft(laser, InvaderShootPos.X + 17);
            Canvas.SetTop(laser, InvaderShootPos.Y + 5);

            UIObjects.CheckCollisionBetweenInvLaserPlayer(_canvas);

            if (InvaderShootPos.Y > 642 || UIObjects.PlayerHasBeenHit)
            {
                DeleteBullet();
            }
        }

        private void DeleteBullet()
        {
            _canvas.Children.Remove(laser);
            UIObjects.InvaderLaserList.RemoveAt(0);
            invT.Stop();
            isShooting = false;
        }

        private void CheckPositionInvaders(List<UIElement> invaders)
        {
            foreach (var inv in invaders)
            {
                var invaderPosition = new Point
                {
                    X = Canvas.GetLeft(inv),
                    Y = Canvas.GetTop(inv)
                };
                if (invaderPosition.Y > 600)
                    Environment.Exit(0);
            }
        }

        private void UpdatePoints()
        {
            if (UIObjects.updatePoints)
            {
                _notifyHandler.Score += _hitPoints;
                UIObjects.updatePoints = false;
            }
        }

        public bool CheckCollisionWithBorderRight(UIElement inv)
        {
            if (Canvas.GetLeft(inv) > 730)
                return true;
            return false;
        }

        public bool CheckCollisionWithBorderLeft(UIElement inv)
        {
            if (Canvas.GetLeft(inv) < 40)
                return true;
            return false;
        }

        public void MoveInvader(List<UIElement> invaders)
        {
            foreach (var inv in invaders)
            {
                _oldPosition.X = Canvas.GetLeft(inv);
                _oldPosition.Y = Canvas.GetTop(inv);

                if (_direction)
                    Canvas.SetLeft(inv, _oldPosition.X - 35);
                else
                    Canvas.SetLeft(inv, _oldPosition.X + 35);
            }
            _hasTurned = false;
            _isBorder = false;
        }

        public void CheckCollision(List<UIElement> invaders)
        {
            foreach (var inv in invaders)
                if (CheckCollisionWithBorderLeft(inv))
                {
                    _isBorder = true;
                    _direction = false;
                }
                else if (CheckCollisionWithBorderRight(inv))
                {
                    _isBorder = true;
                    _direction = true;
                }
            if (_isBorder && !_hasTurned)
                OneDown(invaders);
            else
                MoveInvader(invaders);
        }

        public void OneDown(List<UIElement> invaders)
        {
            foreach (var inv in invaders)
            {
                _oldPosition.X = Canvas.GetLeft(inv);
                _oldPosition.Y = Canvas.GetTop(inv);

                Canvas.SetTop(inv, _oldPosition.Y + 50);
                Canvas.SetLeft(inv, _oldPosition.X);
            }
            _hasTurned = true;
        }
    }
}