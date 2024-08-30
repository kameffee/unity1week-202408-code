using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202408.Menu
{
    public class InGameUIView : MonoBehaviour
    {
        [SerializeField]
        private Button _menuButton;

        public Observable<Unit> OnClickMenuButtonAsObservable() => _menuButton.OnClickAsObservable();
    }
}