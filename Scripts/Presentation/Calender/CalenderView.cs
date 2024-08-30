using UnityEngine;

namespace Unity1week202408.Calender
{
    public class CalenderView : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _days;

        public void Set(int index)
        {
            if (index < 0 || index >= _days.Length)
            {
                Debug.LogError($"index out of range: {index}");
            }

            foreach (var day in _days)
            {
                day.SetActive(false);
            }

            _days[index].SetActive(true);
        }
    }
}