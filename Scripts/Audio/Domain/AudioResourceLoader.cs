using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Unity1week.Audio
{
    public class AudioResourceLoader
    {
        private readonly AudioResource _audioResource;

        public AudioResourceLoader(AudioResource audioResource)
        {
            _audioResource = audioResource;
        }

        public async UniTask<AudioClip> LoadAsync(AudioType type, string id)
        {
            var audioClipData = _audioResource.Get(type, id);
            if (audioClipData.Clip.loadState != AudioDataLoadState.Loaded)
            {
                audioClipData.Clip.LoadAudioData();
            }

            await UniTask.WaitUntil(() => audioClipData.Clip.loadState == AudioDataLoadState.Loaded);
            return audioClipData.Clip;
        }
    }
}