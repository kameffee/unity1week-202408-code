using UnityEngine;

namespace Unity1week202408.Stage
{
    public class StageCoverView : MonoBehaviour
    {
        public void Show()
        {
            SetActive(true);
        }

        public void Hide()
        {
            SetActive(false);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}