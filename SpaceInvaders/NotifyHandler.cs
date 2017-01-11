namespace SpaceInvaders
{
    public class NotifyHandler : NotifyPropertyChangedViewModel
    {
        private static int _score;
        private static int _wave = 1;
        private static int _lives = 3;
        private static int _bullets = 30;
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
        public static NotifyHandler InstanceCreation()
        {
            _score = 0;
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