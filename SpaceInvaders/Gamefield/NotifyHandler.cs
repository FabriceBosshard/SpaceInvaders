namespace SpaceInvaders
{
    public class NotifyHandler : NotifyPropertyChangedViewModel
    {
        private static int _score;
        private static int _wave = 1;
        private static int _lives = 3;
        private static int _bullets = 30;
        private static int _bombs = 3;
        private static volatile NotifyHandler _scoreHandlerObject;
        private static readonly object lockingObject = new object();

        private NotifyHandler()
        {
        }
        public int Lives
        {
            get { return _lives; }
            set
            {
                if (_lives != value)
                {
                    _lives = value;
                    OnPropertyChanged(nameof(Lives));
                }
            }
        }
        public int Bombs
        {
            get { return _bombs; }
            set
            {
                if (_bombs != value)
                {
                    _bombs = value;
                    OnPropertyChanged(nameof(Bombs));
                }
            }
        }
        public int Bullets
        {
            get { return _bullets; }
            set
            {
                if (_bullets != value)
                {
                    _bullets = value;
                    OnPropertyChanged(nameof(Bullets));
                }
            }
        }
        public int Waves
        {
            get { return _wave; }
            set
            {
                if (_wave != value)
                {
                    _wave = value;
                    OnPropertyChanged(nameof(Waves));
                }
            }
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
        private int _goldAmount;
        private int _supernovaCount;
        private int _vBombCount;
        private int _apokalypsecount;
        private int _WaveHighScore;
        private int _HighestScore;
        private int _LivesExceed;

        public int LivesExceed
        {
            get { return _LivesExceed; }
            set
            {
                if (_LivesExceed != value)
                {
                    _LivesExceed = value;
                    OnPropertyChanged(nameof(LivesExceed));
                }
            }
        }

        public int WaveHighScore
        {
            get { return _WaveHighScore; }
            set
            {
                if (_WaveHighScore != value)
                {
                    _WaveHighScore = value;
                    OnPropertyChanged(nameof(WaveHighScore));
                }
            }
        }
        public int HighestScore
        {
            get { return _HighestScore; }
            set
            {
                if (_HighestScore != value)
                {
                    _HighestScore = value;
                    OnPropertyChanged(nameof(HighestScore));
                }
            }
        }
        public int GoldAmount
        {
            get { return _goldAmount; }
            set
            {
                if (_goldAmount != value)
                {
                    _goldAmount = value;
                    OnPropertyChanged(nameof(GoldAmount));
                }
            }
        }
        public int Apokalypsecount
        {
            get { return _apokalypsecount; }
            set
            {
                if (_apokalypsecount != value)
                {
                    _apokalypsecount = value;
                    OnPropertyChanged(nameof(Apokalypsecount));
                }
            }
        }
        public int VBombCount
        {
            get { return _vBombCount; }
            set
            {
                if (_vBombCount != value)
                {
                    _vBombCount = value;
                    OnPropertyChanged(nameof(VBombCount));
                }
            }
        }
        public int SupernovaCount
        {
            get { return _supernovaCount; }
            set
            {
                if (_supernovaCount != value)
                {
                    _supernovaCount = value;
                    OnPropertyChanged(nameof(SupernovaCount));
                }
            }
        }



        public static NotifyHandler InstanceCreation()
        {
            if (_scoreHandlerObject == null)
                lock (lockingObject)
                {
                    if (_scoreHandlerObject == null)
                        _scoreHandlerObject = new NotifyHandler();
                }
            return _scoreHandlerObject;
        }
    }
}