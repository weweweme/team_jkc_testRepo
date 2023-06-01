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
    }
}