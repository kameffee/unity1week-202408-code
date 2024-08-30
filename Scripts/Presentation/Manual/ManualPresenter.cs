using R3;
using Unity1week.Extensions;
using VContainer.Unity;

namespace Unity1week202408.Manual
{
    public class ManualPresenter : Presenter, IInitializable
    {
        private readonly ManualView _view;
        private readonly GameManualPoint _gameManualPoint;

        public ManualPresenter(
            ManualView view,
            GameManualPoint gameManualPoint)
        {
            _view = view;
            _gameManualPoint = gameManualPoint;
        }

        public void Initialize()
        {
            _gameManualPoint.OnStayPlayer
                .Where(onStay => onStay)
                .SubscribeAwait((_, token) => _view.ShowAsync(token))
                .AddTo(this);

            _gameManualPoint.OnStayPlayer
                .Where(onStay => !onStay)
                .SubscribeAwait((_, token) => _view.HideAsync(token))
                .AddTo(this);
        }
    }
}