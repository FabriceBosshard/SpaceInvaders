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
    public class Score : INotifyPropertyChanged
    {
        private int _score;
        private static volatile Score scoreObject;
        private static object lockingObject = new object();

        private Score()
        { }

        public static Score InstanceCreation()
        {
            if (scoreObject == null)
            {
                lock (lockingObject)
                {
                    if (scoreObject == null)
                    {
                        scoreObject = new Score();
                    }
                }
            }
            return scoreObject;
        }

        public int score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged(nameof(score));
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
