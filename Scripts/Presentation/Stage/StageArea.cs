using R3;
using Unity1week202408.Player;
using UnityEngine;

namespace Unity1week202408.Stage
{
    public enum Direction
    {
        Left,
        Right,
    }

    public class StageArea : MonoBehaviour
    {
        public ReadOnlyReactiveProperty<bool> StayPlayer => _onStay;

        [SerializeField]
        private Collider2D _collider2D;

        private readonly ReactiveProperty<bool> _onStay = new(false);
        private readonly Subject<Direction> _onExit = new();

        public Observable<Unit> OnEnterPlayerAsObservable() => _onStay.Where(x => x).AsUnitObservable();

        public Observable<Direction> OnExitPlayerAsObservable() => _onExit; 

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out _))
            {
                var inside = Inside(other);
                _onStay.Value = inside;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out _))
            {
                // 右から出たか
                if (IsRightExit(other))
                {
                    Debug.Log("Right Exit");
                    _onStay.Value = false;
                    _onExit.OnNext(Direction.Right);
                }
                else if (IsLeftExit(other))
                {
                    Debug.Log("Left Exit");
                    _onStay.Value = false;
                    _onExit.OnNext(Direction.Left);
                }
            }
        }
        
        private bool IsRightExit(Collider2D target)
        {
            return target.bounds.min.x > _collider2D.bounds.max.x;
        }
        
        private bool IsLeftExit(Collider2D target)
        {
            return target.bounds.max.x < _collider2D.bounds.min.x;
        }

        private bool Inside(Collider2D target)
        {
            return target.bounds.min.x > _collider2D.bounds.min.x &&
                   target.bounds.max.x < _collider2D.bounds.max.x;
        }
    }
}