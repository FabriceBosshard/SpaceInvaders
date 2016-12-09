using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Prism.Commands;

namespace SpaceInvaders
{
    public class MainWindowViewModel : NotifyPropertyChangedViewModel
    {
        public MainWindowViewModel()
        {
            IsPaused = false;
            ClickCommand = new DelegateCommand(Pause);
        }

        private void Pause()
        {
            IsPaused = !IsPaused;
        }

        private bool _isPaused;
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

        private double _top;
        private double _left;
        private const int Speed = 30;

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

        public void Move(DirectionPlayer direction)
        {
            if (direction == DirectionPlayer.Left)
            {
                if (Left - Speed > 0)
                {
                    Left -= Speed;
                }
                else
                {
                    Left = 0;
                }
            }
            else if (direction == DirectionPlayer.Right)
            {
                int MaxLeft = PlayboardWidth - PlayerWidth;
                if (Left + Speed < MaxLeft)
                {
                    Left += Speed;
                }
                else
                {
                    Left = MaxLeft;
                }
            }
        }
    }
}
