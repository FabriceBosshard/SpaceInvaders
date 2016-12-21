using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpaceInvaders
{
    public class ScoreHandler : INotifyPropertyChanged
    {
        private static int _score;
        private static volatile ScoreHandler _scoreHandlerObject;
        private static object lockingObject = new object();

        private ScoreHandler()
        { }

        public static ScoreHandler InstanceCreation()
        {
            _score = 0;
            if (_scoreHandlerObject == null)
            {
                lock (lockingObject)
                {
                    if (_scoreHandlerObject == null)
                    {
                        _scoreHandlerObject = new ScoreHandler();
                    }
                }
            }
            return _scoreHandlerObject;
        }

        public int Score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged(nameof(Score));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
