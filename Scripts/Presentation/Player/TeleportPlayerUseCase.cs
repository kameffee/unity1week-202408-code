using System.Threading;
using Cysharp.Threading.Tasks;
using Unity1week202408.Stage;

namespace Unity1week202408.Player
{
    public class TeleportPlayerUseCase
    {
        private readonly PlayerController _playerController;
        private readonly StartPoint _startPoint;
        private readonly PlayerFollowCamera _playerFollowCamera;
        
        public TeleportPlayerUseCase(PlayerController playerController, StartPoint startPoint, PlayerFollowCamera playerFollowCamera)
        {
            _playerController = playerController;
            _startPoint = startPoint;
            _playerFollowCamera = playerFollowCamera;
        }
        
        public async UniTask TeleportToStartPoint(CancellationToken cancellationToken)
        {
            _playerFollowCamera.Lock();
            await UniTask.NextFrame(cancellationToken: cancellationToken);
            _playerController.Teleport(_startPoint.WorldPosition);
            await UniTask.NextFrame(cancellationToken: cancellationToken);
            _playerFollowCamera.Unlock();
        }
    }
}