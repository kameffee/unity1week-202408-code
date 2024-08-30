using R3;

namespace Unity1week.Audio
{
    public class AudioSettingsService
    {
        public ReactiveProperty<AudioVolume> BgmVolume => _bgmVolume;
        public ReactiveProperty<AudioVolume> SeVolume => _seVolume;

        private readonly ReactiveProperty<AudioVolume> _bgmVolume = new(new AudioVolume(0.4f));
        private readonly ReactiveProperty<AudioVolume> _seVolume = new(new AudioVolume(0.4f));

        public AudioSettingsService()
        {
        }

        public void SetBgmVolume(AudioVolume volume) => _bgmVolume.Value = volume;

        public void SetSeVolume(AudioVolume volume) => _seVolume.Value = volume;
    }
}