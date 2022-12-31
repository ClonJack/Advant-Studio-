using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Views.Business.ConcreteBusiness.Views
{
    [System.Serializable]
    public class UpgradeBusinessView
    {
        [SerializeField] private TextMeshProUGUI _nameInfo;
        [SerializeField] private TextMeshProUGUI _infoStateInfo;
        [SerializeField] private TextMeshProUGUI _incomePercentagesValueInfo;
        [SerializeField] private Image _icon;

        [Header("Animate")] [SerializeField] private float Duration;
        [SerializeField] private Vector3 SelectV3;
        [SerializeField] private Vector3 UnSelectV3;
        public void RepaintName(string name) => _nameInfo.text = name;
        public void RepaintIncomePercentages(float income) => _incomePercentagesValueInfo.text = $"+{income}%";

        public Image Icon => _icon;

        public void RepaintPurchasedState()
        {
            _infoStateInfo.text = "Purchased";
        }

        public void RepaintPrice(float value)
        {
            _infoStateInfo.text = $"Price:{value}$";
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