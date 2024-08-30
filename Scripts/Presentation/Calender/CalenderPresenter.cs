using R3;
using Unity1week.Extensions;
using VContainer.Unity;

namespace Unity1week202408.Calender
{
    public class CalenderPresenter : Presenter, IInitializable
    {
        private readonly CalenderView _view;
        private readonly ProgressService _progressService;

        public CalenderPresenter(CalenderView view, ProgressService progressService)
        {
            _view = view;
            _progressService = progressService;
        }

        public void Initialize()
        {
            _progressService.Current
                .Subscribe(day => _view.Set(day))
                .AddTo(this);
        }

        public void Set(int index)
        {
            _view.Set(index);
        }
    }
}