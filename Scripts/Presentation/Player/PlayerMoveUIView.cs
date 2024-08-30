using System;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202408.Player
{
    public class PlayerMoveUIView : MonoBehaviour
    {
        [SerializeField]
        private Button _leftButton;

        [SerializeField]
        private Button _rightButton;

        private bool _isLeftButtonDown;
        private bool _isRightButtonDown;

        private const double DoubleClickThreshold = 400d;

        private void Awake()
        {
            _leftButton.OnPointerDownAsObservable()
                .Subscribe(_ => _isLeftButtonDown = true)
                .AddTo(this);

            _leftButton.OnPointerUpAsObservable()
                .Merge(_leftButton.OnPointerExitAsObservable())
                .Subscribe(_ => _isLeftButtonDown = false)
                .AddTo(this);

            _rightButton.OnPointerDownAsObservable()
                .Subscribe(_ => _isRightButtonDown = true)
                .AddTo(this);

            _rightButton.OnPointerUpAsObservable()
                .Merge(_rightButton.OnPointerExitAsObservable())
                .Subscribe(_ => _isRightButtonDown = false)
                .AddTo(this);
        }

        public bool IsLeftButtonPushed() => _isLeftButtonDown;
        public bool IsRightButtonPushed() => _isRightButtonDown;

        public Observable<Unit> OnDoubleClickRightButton() => OnDoubleClickAsObservable(_rightButton);
        public Observable<Unit> OnDoubleClickLeftButton() => OnDoubleClickAsObservable(_leftButton);

        private static Observable<Unit> OnDoubleClickAsObservable(Button button)
        {
            return button.OnPointerDownAsObservable()
                .TimeInterval()
                .Select(t => t.Interval.TotalMilliseconds)
                .Chunk(2, 1)
                .Where(list => list[0] > DoubleClickThreshold)
                .Where(list => list[1] <= DoubleClickThreshold)
                .AsUnitObservable();
        }

        public Observable<Unit> OnPointerUpRightButtonAsObservable() => OnPointerUpButtonAsObservable(_rightButton);
        public Observable<Unit> OnPointerUpLeftButtonAsObservable() => OnPointerUpButtonAsObservable(_leftButton);

        private static Observable<Unit> OnPointerUpButtonAsObservable(Button button) => button.OnPointerUpAsObservable()
            .Merge(button.OnPointerExitAsObservable())
            .AsUnitObservable();
    }
}