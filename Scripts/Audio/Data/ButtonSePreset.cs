using UnityEngine;

namespace Unity1week.Audio
{
    [CreateAssetMenu(menuName = "UI/ButtonSePreset", fileName = "ButtonSePreset_")]
    public class ButtonSePreset : ScriptableObject
    {
        [SerializeField]
        private AudioClip _hoverClip;

        [SerializeField]
        private AudioClip _clickClip;

        public AudioClip HoverClip => _hoverClip;
        public AudioClip ClickClip => _clickClip;
    }
}