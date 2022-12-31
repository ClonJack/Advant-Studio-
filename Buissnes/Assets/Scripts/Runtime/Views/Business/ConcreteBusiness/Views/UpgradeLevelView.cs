using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Views.Business.ConcreteBusiness.Views
{
    [System.Serializable]
    public class UpgradeLevelView
    {
        [SerializeField] private TextMeshProUGUI _priceInfo;
        [SerializeField] private Image _icon;

        [Header("Animate")] [SerializeField] private float Duration;
        [SerializeField] private Vector3 SelectV3;
        [SerializeField] private Vector3 UnSelectV3;

        public Image Icon => _icon;
        public void RepaintPrice(float price)
        {
            _priceInfo.text = $"{price}";
        }

        public async UniTask Select()
        {
            await _icon.rectTransform.DOScale(SelectV3, Duration).AsyncWaitForCompletion().AsUniTask()
                .SuppressCancellationThrow();
        }
        public async UniTask Deselect()
        {
            await _icon.rectTransform.DOScale(UnSelectV3, Duration).AsyncWaitForCompletion().AsUniTask()
                .SuppressCancellationThrow();
        }
    }
}