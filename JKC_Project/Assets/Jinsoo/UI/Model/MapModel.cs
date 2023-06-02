namespace Model
{
    public static class MapModel
    {
        private static int _remainingTime;
        public static int RemainingTime
        {
            get { return _remainingTime; }
        }

        public static void DecreaseTime()
        {
            --_remainingTime;
        }

        public static void Initialize()
        {
            _remainingTime = 90;
        }

        private static int _currentScreenSizeIndex;
        public static int CurrentScreenSizeIndex
        {
            get { return _currentScreenSizeIndex; }
        }

        private static int _prevScreenSizeIndex;
        public static int PrevScreenSizeIndex
        {
            get { return _prevScreenSizeIndex; }
        }

        public static void SetCurrentScreenSizeIndex(int index)
        {
            _prevScreenSizeIndex = _currentScreenSizeIndex;
            _currentScreenSizeIndex = index;
        }
    }
}