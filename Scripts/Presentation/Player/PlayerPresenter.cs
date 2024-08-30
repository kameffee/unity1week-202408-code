using R3;
using Unity1week.Extensions;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202408.Player
{
    public class PlayerPresenter : Presenter, IInitializable, ITickable
    {
        private readonly PlayerController _playerController;
        private readonly PlayerMoveUIView _playerMoveUIView;

        public PlayerPresenter(PlayerController playerController, PlayerMoveUIView playerMoveUIView)
        {
            _playerController = playerController;
            _playerMoveUIView = playerMoveUIView;
        }

        public void Initialize()
        {
            _playerMoveUIView.OnDoubleClickRightButton()
                .Merge(_playerMoveUIView.OnDoubleClickLeftButton())
                .Subscribe(_ => _playerController.SetDash(true))
                .AddTo(this);

            _playerMoveUIView.OnPointerUpRightButtonAsObservable()
                .Merge(_playerMoveUIView.OnPointerUpLeftButtonAsObservable())
                .Subscribe(_ => _playerController.SetDash(false))
                .AddTo(this);
        }

        public void Tick()
        {
            if (_playerMoveUIView.IsLeftButtonPushed() && _playerMoveUIView.IsRightButtonPushed())
            {
                _playerController.Move(Vector2.zero);
                return;
            }

            if (_playerMoveUIView.IsLeftButtonPushed())
            {
                _playerController.Move(Vector2.left);
            }
            else if (_playerMoveUIView.IsRightButtonPushed())
            {
                _playerController.Move(Vector2.right);
            }
            else
            {
                var direction = new Vector2(Input.GetAxis("Horizontal"), 0);
                _playerController.Move(direction);
            }
        }
    }
}