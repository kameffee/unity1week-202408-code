using LitMotion;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Unity1week202408
{
    public class LightController : MonoBehaviour
    {
        [SerializeField]
        private Light2D _light;

        public void Flash()
        {
            var intensity = _light.intensity;
            LMotion.Create(_light.intensity, 0f, 0.05f)
                .WithLoops(4, LoopType.Yoyo)
                .WithDelay(0.2f)
                .WithEase(Ease.Linear)
                .WithOnComplete(() => _light.intensity = intensity)
                .Bind(f => _light.intensity = f);
        }
    }
}