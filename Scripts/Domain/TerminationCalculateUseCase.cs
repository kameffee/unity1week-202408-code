using Unity1week202408.Calender;

namespace Unity1week202408
{
    public class TerminationCalculateUseCase
    {
        private readonly ProgressService _progressService;
        private readonly Situation _situation;

        public TerminationCalculateUseCase(
            ProgressService progressService,
            Situation situation)
        {
            _progressService = progressService;
            _situation = situation;
        }

        public bool IsTermination()
        {
            if (_situation.ExistAnomaly)
            {
                return false;
            }
            
            return _progressService.IsFinal();
        }
    }
}