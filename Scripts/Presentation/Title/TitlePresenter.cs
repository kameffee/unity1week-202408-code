using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Unity1week.Extensions;

namespace Unity1week202408.Title
{
    public class TitlePresenter : Presenter
    {
        private readonly TitleView _view;

        public TitlePresenter(TitleView view)
        {
            _view = view;
        }

        public void Show()
        {
            _view.ShowFast();
        }

        public Observable<Unit> OnClickStartButtonAsObservable() => _view.OnClickStartButtonAsObservable();

        public async UniTask ShowAsync(CancellationToken cancellationToken)
        {
            await _view.ShowAsync(cancellationToken);
        }

        public async UniTask HideAsync(CancellationToken cancellationToken)
        {
            await _view.HideAsync(cancellationToken);
        }
    }
}