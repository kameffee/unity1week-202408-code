using Unity.Cinemachine;
using UnityEngine;

namespace Unity1week202408
{
    public class PlayerFollowCamera : MonoBehaviour
    {
        [SerializeField]
        private CinemachineCamera _mainCamera;
        
        private Vector3 _defaultDamping;

        private void Awake()
        {
            _defaultDamping = _mainCamera.GetComponent<CinemachinePositionComposer>().Damping;
        }

        public void Lock()
        {
            if (_mainCamera.TryGetComponent<CinemachinePositionComposer>(out var composer))
            {
                composer.Damping = Vector3.zero;
            }
        }

        public void Unlock()
        {
            if (_mainCamera.TryGetComponent<CinemachinePositionComposer>(out var composer))
            {
                composer.Damping = _defaultDamping;
            }
        }
    }
}