using System;
using Spine;
using Spine.Unity;
using Unity1week.Audio;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;
using Event = Spine.Event;

namespace Unity1week202408.Player
{
    public class FootstepSePlayer : MonoBehaviour
    {
        [SerializeField]
        private SkeletonAnimation _skeletonAnimation;

        [Inject]
        private readonly AudioPlayer _audioPlayer;

        private void Awake()
        {
            Assert.IsNotNull(_skeletonAnimation, "_skeletonAnimation is null.");

            var lifetimeScope = LifetimeScope.Find<LifetimeScope>();
            lifetimeScope.Container.Inject(this);

            _skeletonAnimation.AnimationState.Event += OnAnimationStateOnEvent;
        }

        private void OnAnimationStateOnEvent(TrackEntry entry, Event ev)
        {
            switch (ev.Data.Name)
            {
                case "footstep1":
                    _audioPlayer.PlaySe("InGame/Footstep1");
                    break;
                case "footstep2":
                    _audioPlayer.PlaySe("InGame/Footstep2");
                    break;
            }
        }
    }
}