namespace Model
{
    public static class LobbyRoomModel
    {
        private static int _startCount;
        public static int StartCount
        {
            get { return _startCount; }
        }

        public static void ResetStartCount()
        {
            _startCount = 10;
        }

        public static void DecreaseStartCount()
        {
            --_startCount;
        }
    }
}