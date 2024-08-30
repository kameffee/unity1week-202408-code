using R3;
using Unity1week202408.Player;
using UnityEngine;

namespace Unity1week202408.Stage
{
    public class NextStagePoint : MonoBehaviour
    {
        private readonly Subject<Unit> _onPlayerEnter = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out var player))
            {
                _onPlayerEnter.OnNext(Unit.Default);
            }
        }

        public Observable<Unit> OnPlayerEnterAsObservable() => _onPlayerEnter;
    }
}