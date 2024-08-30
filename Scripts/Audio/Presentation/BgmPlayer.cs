using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Unity1week.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class BgmPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;

        private float _volume;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        public void Play(AudioClip audioClip, bool isLoop)
        {
            SetVolume(_volume);
            _audioSource.clip = audioClip;
            _audioSource.loop = isLoop;
            _audioSource.Play();
        }

        public async UniTask StopAsync(float fadeoutTime = 0)
        {
            if (fadeoutTime > 0f)
            {
                await LMotion.Create(_audioSource.volume, 0, fadeoutTime)
                    .BindToVolume(_audioSource);
            }

            _audioSource.Stop();
        }

        public void SetVolume(float volume)
        {
            _volume = volume;
            _audioSource.volume = volume;
        }
    }
}