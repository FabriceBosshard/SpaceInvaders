using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SpaceInvaders
{
    public class InvaderRow
    {
        private readonly double left = 60;
        private double top;
        private int incrementForInvader = 2;       
        private Point _oldPosition;
        private bool _isBorder = false;
        private bool _hasTurned;
        private int _invadersRows = 2;
        private int _invaderCountRows = 5;
        private readonly Point _startingPoint = new Point(50, 0);
        private List<UIElement> _invaders = new List<UIElement>();
        private int _invaderWidth = 40;
        private readonly int _invaderHeight = 40;
        private int _invadercount;
        private Canvas _canvas;
        
        private readonly ScoreHandler _s = ScoreHandler.InstanceCreation();
        private DispatcherTimer _t = new DispatcherTimer();
        private readonly TimeSpan _speed = new TimeSpan(0,0,0,0,800);
        private int _hitPoints = 10;
        //private Point InvaderShootPos;
        //private int bulletSpeed = 10;
        //private Ellipse laser;

        

        public void CreateNewWave()
        {            
            for (int y = 0; y < _invadersRows; y++)
            {
                for (int z = 0; z < _invaderCountRows; z++)
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
                        Canvas.SetTop(Body, _startingPoint.Y + top);
                        Canvas.SetLeft(Body, _startingPoint.X + (z * left));
                        _canvas.Children.Add(Body);
                    }
                    _invaders.Add(Body);
                    UIObjects.InvaderList.Add(Body);
                    _invadercount++;
                }
                top += 50;
            }
            direction = false;
            _t = new DispatcherTimer();
            _t.Interval = _speed;
            _t.Tick += InvaderRow_Tick;
            _t.Start();
        }

        public InvaderRow(Canvas canvas)
        {
            this._canvas = canvas;
            for (int j = 0; j < _invadersRows; j++)
            {
                for (int i = 0; i < _invaderCountRows; i++)
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
                        Canvas.SetTop(Body, _startingPoint.Y + top);
                        Canvas.SetLeft(Body, _startingPoint.X + (i * left));                                            
                    }
                    canvas.Children.Add(Body);
                    _invaders.Add(Body);
                    UIObjects.InvaderList.Add(Body);
                    _invadercount++;
                }
                top += 50;
            }
            _t.Interval = _speed;
            _t.Tick += InvaderRow_Tick;
            _t.Start();                     
        }

        private void InvaderRow_Tick(object sender, EventArgs e)
        {
            CheckCollision(_invaders);           
            CheckPositionInvaders(_invaders);
            UIObjects.checkInvaderCount();
            UpdatePoints();
            CheckOnNewWave();

        }

        private void CheckOnNewWave()
        {                                 
            if (UIObjects.newWave)
            {
                _invaders.Clear();
                UIObjects.InvaderList.Clear();
                _t.Stop();
                top = 0;
                if (_s.Waves >= 20)
                {
                    _invadersRows = (int)InvaderRows.TwentyToThirty;
                    _invaderCountRows = 5;
                }
                else if (_s.Waves >= 10)
                {
                    _invadersRows = (int)InvaderRows.tenToTwenty;
                    _invaderCountRows = 5;
                }
                else
                {
                    _invaderCountRows += 1;
                    _s.Waves += 1;
                    UIObjects.newWave = false;
                    CreateNewWave();
                }                                        
            }
        }

        //private void InvaderShoot(List<UIElement> invaders)
        //{
        //    foreach (UIElement inv in invaders)
        //    {
        //        Ellipse laser = Laser.CreateLaser();
        //        Random rnd = new Random();

        //        if (rnd.Next(1,30) < 5)
        //        {
        //            InvaderShootPos.Y = Canvas.GetTop(inv);
        //            InvaderShootPos.X = Canvas.GetLeft(inv);

        //            Canvas.SetTop(laser,InvaderShootPos.Y);
        //            Canvas.SetLeft(laser,InvaderShootPos.X + 20);
        //            _canvas.Children.Add(laser);
        //            UIObjects.InvaderLaserList.Add(laser);

        //            invT = new DispatcherTimer { Interval = new TimeSpan(10000) };
        //            invT.Tick += Shoot;
        //            invT.Start();
        //        }
        //    }
        //}

        //private void Shoot(object sender, EventArgs e)
        //{
            
        //    InvaderShootPos.Y -= bulletSpeed;
        //    Canvas.SetTop(laser, InvaderShootPos.Y);
        //    Canvas.SetLeft(laser, InvaderShootPos.X + 20);

        //    UIObjects.CheckCollisionBetweenInvLaserPlayer(_canvas);

        //    if (InvaderShootPos.Y > 642 || UIObjects.PlayerHasBeenHit)
        //    {
        //        DeleteBullet();
        //    }
        //}

        //private void DeleteBullet()
        //{
        //    _canvas.Children.Remove(laser);
        //    UIObjects.InvaderLaserList.RemoveAt(0);           
        //    UIObjects.PlayerHasBeenHit = false;
        //    invT.Stop();            
        //}

        private void CheckPositionInvaders(List<UIElement> invaders)
        {
            foreach (var inv in invaders)
            {
                Point invaderPosition = new Point()
                {
                    X = Canvas.GetLeft(inv),
                    Y = Canvas.GetTop(inv)
                };
                if (invaderPosition.Y > 580)
                {
                    //Player dies
                    Environment.Exit(0);
                }
            }
        }

        private void UpdatePoints()
        {
            if (UIObjects.updatePoints)
            {
                _s.Score += _hitPoints;
                UIObjects.updatePoints = false;
            }
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
                _oldPosition.X = Canvas.GetLeft(inv);
                _oldPosition.Y = Canvas.GetTop(inv);

                if (direction)
                {
                    Canvas.SetLeft(inv, _oldPosition.X - 35);

                }
                else
                {
                    Canvas.SetLeft(inv, _oldPosition.X + 35);

                }
            }
            _hasTurned = false;
            _isBorder = false;
        }

        public void CheckCollision(List<UIElement> invaders)
        {
            foreach (UIElement inv in invaders)
            {

                if (CheckCollisionWithBorderLeft(inv))
                {
                    _isBorder = true;
                    direction = false;
                }
                else if (CheckCollisionWithBorderRight(inv))
                {
                    _isBorder = true;
                    direction = true;
                }
            }
            if (_isBorder && !_hasTurned)
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
                _oldPosition.X = Canvas.GetLeft(inv);
                _oldPosition.Y = Canvas.GetTop(inv);

                Canvas.SetTop(inv, _oldPosition.Y + 50);
                Canvas.SetLeft(inv, _oldPosition.X);
            }
            _hasTurned = true;
        }
    }
}
