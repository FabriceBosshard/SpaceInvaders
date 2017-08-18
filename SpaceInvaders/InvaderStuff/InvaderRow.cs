using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private const double _left = 40;
        private const int _invaderWidth = 40;
        private readonly int _invaderHeight = 40;
        private int hitGold = 20;
        private readonly NotifyHandler _notifyHandler = NotifyHandler.InstanceCreation();
        private readonly MainWindowViewModel _mh = MainWindowViewModel.InstanceCreation();
        private readonly TimeSpan _speed = new TimeSpan(0, 0, 0, 1,0);
        private readonly TimeSpan _speed2 = new TimeSpan(0, 0, 0, 0, 550);
        private readonly Point _startingPoint = new Point(50, 0);
        private readonly Canvas _canvas;
        private bool _direction;
        private bool _hasTurned;
        private readonly int _hitPoints = 10;
        private int _invadercount;
        private int _invaderCountRows = 6;
        private int _invadersRows = 3;
        private bool _isBorder;
        private Point _oldPosition;
        private DispatcherTimer _t = new DispatcherTimer();
        private readonly DispatcherTimer _scoreTimer = new DispatcherTimer();
        private double _top;
        private const int _bulletSpeed = 5;
        private DispatcherTimer _invT;
        private Ellipse _laser;
        private Point _invaderShootPos;
        private bool _isShooting;

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
                        Source = new BitmapImage(new Uri("InvaderStuff/invader.png", UriKind.Relative))
                    };

                    if (_invadercount == 0)
                    {
                        Canvas.SetTop(Body, _startingPoint.Y);
                        Canvas.SetLeft(Body, _startingPoint.X);
                    }
                    else
                    {
                        Canvas.SetTop(Body, _startingPoint.Y + _top);
                        Canvas.SetLeft(Body, _startingPoint.X + i*_left);
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

            _scoreTimer.Interval = new TimeSpan(0,0,0,0,300);
            _scoreTimer.Tick += ScoreTimer_Tick;
            _scoreTimer.Start();
        }

        private void ScoreTimer_Tick(object sender, EventArgs e)
        {
            if (!UIObjects.isShootingBomb)
            {
                UpdatePoints(UIObjects.InvaderHitFromBomb);
            }
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
                        Source = new BitmapImage(new Uri("InvaderStuff/invader.png", UriKind.Relative))
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
                        Canvas.SetLeft(Body, _startingPoint.X + z*_left);
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
            if (!_mh.IsPaused)
            {
                if (!UIObjects.GameOver)
                {
                    CheckCollision(UIObjects.InvaderList);
                    CheckPositionInvaders(UIObjects.InvaderList);
                    UIObjects.checkInvaderCount();
                    CheckOnNewWave();
                    InvaderShoot(UIObjects.InvaderList);
                }
            }

        }

        private void CheckOnNewWave()
        {
            if (UIObjects.newWave)
            {
                UIObjects.InvaderList.Clear();
                _t.Stop();
                _top = 0;
                if (_notifyHandler.Waves >= 29)
                {
                    _notifyHandler.GoldAmount += 2000;
                    _invaderCountRows = 0;
                    _invadersRows = 0;
                    UIObjects.GameOver = true;
                    GameOver GO = new GameOver();
                    GO.Show();
                }
                if (_notifyHandler.Waves >= 24)
                {
                    _notifyHandler.GoldAmount += 1000;
                    _invaderCountRows = 6;
                    _t.Interval = _speed2;
                }
                if (_notifyHandler.Waves == 19)
                {
                    _invadersRows = 7;
                    _invaderCountRows = 6;
                    _notifyHandler.GoldAmount += 800;
                }
                else if (_notifyHandler.Waves == 14)
                {
                    _invadersRows = 6;
                    _invaderCountRows = 6;
                    _notifyHandler.GoldAmount += 600;
                }
                else if (_notifyHandler.Waves == 9)
                {
                    _invadersRows = 5;
                    _invaderCountRows = 6;
                    _notifyHandler.GoldAmount += 400;
                }
                else if (_notifyHandler.Waves == 4)
                {
                    _invadersRows = 4;
                    _invaderCountRows = 6;
                    _notifyHandler.GoldAmount += 300;
                }
                else
                {
                    _invaderCountRows += 1;
                    _notifyHandler.GoldAmount += 200;
                    _notifyHandler.Bullets = 75;
                }
                if (_notifyHandler.Waves % 5 == 0)
                {
                    _notifyHandler.Bombs = 3;
                }
                _notifyHandler.Waves += 1;
                
                UIObjects.newWave = false;
                CreateNewWave();
            }
        }

        private void InvaderShoot(List<UIElement> invaders)
        {
            if (_isShooting == false)
            {
                Random random = new Random();
                int rnd = random.Next(0, invaders.Count); 
                UIElement inv = invaders.ElementAt(rnd);

                _laser = InvaderBombs.CreateLaser();

                _invaderShootPos.Y = Canvas.GetTop(inv);
                _invaderShootPos.X = Canvas.GetLeft(inv);

                Canvas.SetTop(_laser, _invaderShootPos.Y + 5);
                Canvas.SetLeft(_laser, _invaderShootPos.X + (_invaderWidth / 2));
                _canvas.Children.Add(_laser);
                UIObjects.InvaderLaserList.Add(_laser);

                _invT = new DispatcherTimer { Interval = new TimeSpan(10000) };
                _invT.Tick += Shoot;
                _invT.Start();
                _isShooting = true;
            }


        }

        private void Shoot(object sender, EventArgs e)
        {
            if (!_mh.IsPaused)
            {
                _invaderShootPos.Y += _bulletSpeed;

            
                Canvas.SetLeft(_laser, _invaderShootPos.X + (_invaderWidth / 2));
                Canvas.SetTop(_laser, _invaderShootPos.Y + 5);

                UIObjects.CheckCollisionBetweenInvLaserPlayer(_canvas);

                if (_invaderShootPos.Y > 642 || UIObjects.PlayerHasBeenHit)
                {
                    DeleteBullet();
                }
            }
        }

        private void DeleteBullet()
        {
            _canvas.Children.Remove(_laser);
            UIObjects.InvaderLaserList.RemoveAt(0);
            _invT.Stop();
            _isShooting = false;
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
                if (invaderPosition.Y > 400)
                {
                    UIObjects.GameOver = true;
                    GameOver GO = new GameOver();
                    GO.Show();
                }
                    
            }
        }

        private void UpdatePoints(List<UIElement> invadersHit )
        {   
            _notifyHandler.Score += (_hitPoints * invadersHit.Count);            
            _notifyHandler.GoldAmount += (hitGold * invadersHit.Count);
            UIObjects.InvaderHitFromBomb.Clear();

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