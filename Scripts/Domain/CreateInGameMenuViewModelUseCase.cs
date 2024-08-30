using Unity1week202408.Anomaly;
using Unity1week202408.Menu;

namespace Unity1week202408
{
    using ViewModel = InGameMenuView.ViewModel;

    public class CreateInGameMenuViewModelUseCase
    {
        private readonly AnomalyRecordRepository _anomalyRecordRepository;
        private readonly AnomalyMasterDataRepository _anomalyMasterDataRepository;
        
        public CreateInGameMenuViewModelUseCase(
            AnomalyRecordRepository anomalyRecordRepository,
            AnomalyMasterDataRepository anomalyMasterDataRepository)
        {
            _anomalyRecordRepository = anomalyRecordRepository;
            _anomalyMasterDataRepository = anomalyMasterDataRepository;
        }
        
        public ViewModel Create()
        {
            return new ViewModel(
                _anomalyRecordRepository.Count,
                _anomalyMasterDataRepository.Count()
            );
        }
    }
}