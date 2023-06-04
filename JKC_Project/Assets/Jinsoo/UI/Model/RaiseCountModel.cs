using UniRx;

namespace Model
{
    public static class RaiseCountModel
    {
        private static readonly ReactiveProperty<int> _countReactiveProperty = new IntReactiveProperty(0);
        
        public static ReadOnlyReactiveProperty<int> CurrentCount
            => _countReactiveProperty.ToReadOnlyReactiveProperty();

        public static void RaiseCount()
        {
            ++_countReactiveProperty.Value;
        }

        public static void ResetCount()
        {
            _countReactiveProperty.Value = default;
        }
    }
}