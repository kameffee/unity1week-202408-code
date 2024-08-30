using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Unity1week.Extensions;
using VContainer.Unity;

namespace Unity1week202408.Menu
{
    public class InGameMenuPresenter : Presenter, IInitializable
    {
        private readonly InGameUIView _uiView;
        private readonly InGameMenuView _menuView;
        private readonly CreateInGameMenuViewModelUseCase _createInGameMenuViewModelUseCase;

        public InGameMenuPresenter(
            InGameUIView uiView,
            InGameMenuView menuView, 
            CreateInGameMenuViewModelUseCase createInGameMenuViewModelUseCase)
        {
            _uiView = uiView;
            _menuView = menuView;
            _createInGameMenuViewModelUseCase = createInGameMenuViewModelUseCase;
        }

        public void Initialize()
        {
            _uiView.OnClickMenuButtonAsObservable()
                .SubscribeAwait(async (_, token) => await ShowAsync(token))
                .AddTo(this);
        }

        private async UniTask ShowAsync(CancellationToken cancellationToken)
        {
            var viewModel = _createInGameMenuViewModelUseCase.Create();
            _menuView.ApplyViewModel(viewModel);

            await _menuView.ShowAsync(cancellationToken);
            await _menuView.OnClickCloseButtonAsObservable().FirstAsync(cancellationToken);
            await _menuView.HideAsync(cancellationToken);
        }
    }
}