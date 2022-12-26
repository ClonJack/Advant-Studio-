using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

namespace Utilities.Fixes
{
    public class ScrollFix : MonoBehaviour
    {
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private Slider _slider;

        public void UpdateScrollPosition()
        {
            _scrollbar.value = _slider.value;
        }
    }
}