using R3;
using Unity1week202408.Player;
using UnityEngine;

namespace Unity1week202408.Stage
{
    public class GoalPoint : MonoBehaviour
    {
        private readonly Subject<Unit> _onEnter = new();

        public Observable<Unit> OnPlayerEnterAsObservable() => _onEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out _))
            {
                _onEnter.OnNext(Unit.Default);
            }
        }
    }
}