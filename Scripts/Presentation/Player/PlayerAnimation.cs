using System;
using Spine.Unity;
using UnityEngine;

namespace Unity1week202408.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField]
        private SkeletonAnimation _skeletonAnimation;

        [SerializeField]
        [SpineAnimation]
        private string _eyeAnimationName = "Eye";

        [SerializeField]
        [SpineAnimation]
        private string _idleAnimationName = "Idle";

        [SerializeField]
        [SpineAnimation]
        private string _walkAnimationName = "Walk";

        [SerializeField]
        [SpineAnimation]
        private string _runAnimationName = "Run";

        private const int EyeTrackIndex = 0;
        private const int BodyTrackIndex = 1;

        private void Awake()
        {
            _skeletonAnimation.AnimationState.SetAnimation(EyeTrackIndex, _eyeAnimationName, true);
            _skeletonAnimation.AnimationState.SetEmptyAnimation(BodyTrackIndex, 0);
        }

        public void Idle()
        {
            if (_skeletonAnimation.AnimationState.GetCurrent(BodyTrackIndex).Animation.Name == _idleAnimationName)
                return;

            _skeletonAnimation.AnimationState.SetAnimation(BodyTrackIndex, _idleAnimationName, true);
        }

        public void Walk()
        {
            if (_skeletonAnimation.AnimationState.GetCurrent(BodyTrackIndex).Animation.Name == _walkAnimationName)
                return;

            _skeletonAnimation.AnimationState.SetAnimation(BodyTrackIndex, _walkAnimationName, true);
        }

        public void Run()
        {
            if (_skeletonAnimation.AnimationState.GetCurrent(BodyTrackIndex).Animation.Name == _runAnimationName)
                return;

            _skeletonAnimation.AnimationState.SetAnimation(BodyTrackIndex, _runAnimationName, true);
        }

        public void SetDirection(Vector2 direction)
        {
            if (direction.x > 0)
            {
                _skeletonAnimation.Skeleton.ScaleX = 1;
            }
            else if (direction.x < 0)
            {
                _skeletonAnimation.Skeleton.ScaleX = -1;
            }
        }
    }
}