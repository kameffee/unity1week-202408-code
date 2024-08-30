using R3;
using UnityEngine;

namespace Unity1week202408.Manual
{
    public class GameManualPoint : MonoBehaviour
    {
        public ReadOnlyReactiveProperty<bool> OnStayPlayer => _onStayPlayer;

        private readonly ReactiveProperty<bool> _onStayPlayer = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            _onStayPlayer.Value = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            _onStayPlayer.Value = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _onStayPlayer.Value = false;
        }
    }
}