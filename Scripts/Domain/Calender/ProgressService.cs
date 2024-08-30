using R3;
using UnityEngine;

namespace Unity1week202408.Calender
{
    public class ProgressService
    {
        public ReadOnlyReactiveProperty<int> Current => _current;

        private readonly ReactiveProperty<int> _current = new();

        private const int Max = 7; 

        public void SetFirst()
        {
            _current.Value = 0;
        }

        public void Next()
        {
            Debug.Log($"Next Progress: {_current.Value + 1}");
            _current.Value++;
        }

        public void Reset()
        {
            Debug.Log("Reset Progress");
            _current.Value = 0;
        }

        public bool IsFinal()
        {
            return _current.Value == Max;
        }
    }
}