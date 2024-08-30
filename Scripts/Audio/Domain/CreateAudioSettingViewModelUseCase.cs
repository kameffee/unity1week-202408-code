namespace Unity1week.Audio
{
    using ViewModel = AudioSettingView.ViewModel;

    public class CreateAudioSettingViewModelUseCase
    {
        private readonly AudioSettingsService _audioSettingsService;

        public CreateAudioSettingViewModelUseCase(AudioSettingsService audioSettingsService)
        {
            _audioSettingsService = audioSettingsService;
        }

        public ViewModel Create()
        {
            return new ViewModel(
                _audioSettingsService.BgmVolume.Value,
                _audioSettingsService.SeVolume.Value);
        }
    }
}