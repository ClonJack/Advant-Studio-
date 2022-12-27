using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

namespace Utilities.Fixes
{
    public class ScrollFix : MonoBehaviour
    {
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private Slider _slider;
        private void Start()
        {
            _scrollbar.OnValueChangedAsObservable().Subscribe(x => { _slider.value = _scrollbar.value; }).AddTo(this);
        }
    }
}