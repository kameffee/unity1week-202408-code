using Unity1week202408.Player;
using UnityEngine;

namespace Unity1week202408
{
    public class PlayerService
    {
        private readonly PlayerController _playerController;
        
        private Vector3 _initialPosition;

        public PlayerService(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void SetMovable(bool isMovable)
        {
            _playerController.SetMovable(isMovable);
        }

        public void SetCurrentPositionToInitialPosition()
        {
            _initialPosition = _playerController.WorldPosition;
        }

        public void ApplyInitialPosition()
        {
            _playerController.Teleport(_initialPosition);
        }
    }
}