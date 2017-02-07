using System;
using System.Windows.Threading;
using Prism.Commands;

namespace SpaceInvaders
{
    public class MainWindowViewModel : NotifyPropertyChangedViewModel
    {
        private const int Speed = 5;

        private bool _isPaused;
        private double _left;
        public int previousDirection;
        public int direction;
        

        private double _top;

        public MainWindowViewModel()
        {
            IsPaused = false;
            ClickCommand = new DelegateCommand(Pause);
            NotifyHandler = NotifyHandler.InstanceCreation();
            DispatcherTimer moveTimer = new DispatcherTimer();
            moveTimer.Tick += MoveTimerTick;
            moveTimer.Interval = new TimeSpan(10000);
            moveTimer.Start();
        }

        private void MoveTimerTick(object sender, EventArgs e)
        {
            if (!IsPaused)
            {
                switch (direction)
                {
                    case(int)DirectionPlayer.Left:
                        if (Left - Speed > 0)
                        {
                            Left -= Speed;
                        }                      
                        break;
                    case (int)DirectionPlayer.Right:
                        var MaxLeft = PlayboardWidth - PlayerWidth;
                        if (Left + Speed < MaxLeft)
                        {
                            Left += Speed;
                        }
                        break;
                }
            }
        }

        public NotifyHandler NotifyHandler { get; set; }

        public bool IsPaused
        {
            get { return _isPaused; }
            set
            {
                if (_isPaused != value)
                {
                    _isPaused = value;
                    OnPropertyChanged();
                }
            }
        }

        public DelegateCommand ClickCommand { get; }

        public int XOffset { get; } = 200;
        public int PlayboardWidth { get; } = 794;

        public double Top
        {
            get { return _top; }
            set
            {
                _top = value;
                OnPropertyChanged();
            }
        }

        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                OnPropertyChanged();
            }
        }

        public int PlayerWidth { get; } = 100;

        public object PlayerViewModel
        {
            get { throw new NotImplementedException(); }
        }

        private void Pause()
        {
            IsPaused = !IsPaused;
        }

        //public void Move(DirectionPlayer direction)
        //{
        //    if (direction == DirectionPlayer.Left)
        //    {
        //        if (Left - Speed > 0)
        //            Left -= Speed;
        //        else
        //            Left = 0;
        //    }
        //    else if (direction == DirectionPlayer.Right)
        //    {
        //        var MaxLeft = PlayboardWidth - PlayerWidth;
        //        if (Left + Speed < MaxLeft)
        //            Left += Speed;
        //        else
        //            Left = MaxLeft;
        //    }
        //}
    }
}