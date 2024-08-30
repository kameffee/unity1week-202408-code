using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace Unity1week202408.Ending
{
    public class EndingPerformPresenter
    {
        private readonly EndingPerformView _endingPerformView;

        public EndingPerformPresenter(EndingPerformView endingPerformView)
        {
            _endingPerformView = endingPerformView;
        }

        public async UniTask PerformAsync(CancellationToken cancellationToken)
        {
            await _endingPerformView.PerformAsync(cancellationToken);
        }
        
        public Observable<Unit> OnClickTitleButtonAsObservable() => _endingPerformView.OnClickTitleButtonAsObservable();

        public void Hide()
        {
            _endingPerformView.Hide();
        }
    }
}