using Cysharp.Threading.Tasks;
using Runtime.Save.ConcreteModel.Business;
using Runtime.Views.Business.ConcreteBusiness.Models;
using Runtime.Views.Business.ConcreteBusiness.Views;
using Runtime.Views.Business.Player.Presentor;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Views.Business.ConcreteBusiness.Presenter
{
    [System.Serializable]
    public class ConcreteBusinessPresenter
    {
        private ConcreteReactiveBusinessModel _reactiveBusinessModel;

        private readonly ConcreteBusinessDataModel _concreteBusinessDataModel;
        private readonly BusinessView _businessView;
        private readonly ConcretePlayerPresenter _concretePlayerPresenter;

        public ConcreteBusinessPresenter(ConcreteBusinessDataModel concreteBusinessDataModel, BusinessView businessView,
            ConcretePlayerPresenter concretePlayerPresenter)
        {
            _concreteBusinessDataModel = concreteBusinessDataModel;
            _businessView = businessView;

            _concretePlayerPresenter = concretePlayerPresenter;

            _reactiveBusinessModel = new ConcreteReactiveBusinessModel(_concreteBusinessDataModel);
        }

        public void StartHandler()
        {
            GeneralInfo();
            UpgradeLevel();

            UpgradeButton(_reactiveBusinessModel.UpgradeReactiveDataModel1, _businessView.UpgradeBusinessViewRight,
                _concreteBusinessDataModel.UpgradeDataModel1);
            UpgradeButton(_reactiveBusinessModel.UpgradeReactiveDataModel2, _businessView.UpgradeBusinessViewLeft,
                _concreteBusinessDataModel.UpgradeDataModel2);
        }

        private void UpgradeButton(UpgradeReactiveDataModel modelUpgrade, UpgradeBusinessView upgradeView,
            UpgradeDataModel upgradeDataModel)
        {
            /// :D
            modelUpgrade.Name.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(x => { upgradeView.RepaintName(modelUpgrade.Name.Value); })
                .AddTo(_businessView);
            modelUpgrade.IncomeMultiplierPercentages.ObserveEveryValueChanged(x => x.Value).Subscribe(x =>
            {
                upgradeView.RepaintIncomePercentages(modelUpgrade
                    .IncomeMultiplierPercentages.Value);
            }).AddTo(_businessView);
            modelUpgrade.IsPurchased.ObserveEveryValueChanged(x => x.Value).Subscribe(x =>
            {
                if (modelUpgrade.IsPurchased.Value)
                {
                    upgradeDataModel.IsPurchased = modelUpgrade.IsPurchased.Value;
                    _businessView.GeneralView.RepaintIncomeInfo($"{GetIncoming()}");
                    upgradeView.RepaintPurchasedState();
                }
            }).AddTo(_businessView);
            modelUpgrade.Price.ObserveEveryValueChanged(x => x.Value).Subscribe(x =>
            {
                if (!modelUpgrade.IsPurchased.Value)
                {
                    upgradeView.RepaintPrice(modelUpgrade.Price.Value);
                }
                else
                {
                    upgradeView.RepaintPurchasedState();
                }
            }).AddTo(_businessView);

            upgradeView.Icon.OnPointerClickAsObservable()
                .Subscribe(x =>
                {
                    if (!modelUpgrade.IsPurchased.Value)
                    {
                        OnClickUpgrade(modelUpgrade, upgradeView).Forget();
                    }
                }).AddTo(_businessView);
        }

        private async UniTaskVoid OnClickUpgrade(UpgradeReactiveDataModel modelUpgrade, UpgradeBusinessView upgradeView)
        {
            if (_concretePlayerPresenter.ReactivePlayer.Balance.Value >= modelUpgrade.Price.Value)
            {
                await upgradeView.Select();
                _concretePlayerPresenter.ReactivePlayer.Balance.Value -= modelUpgrade.Price.Value;
                modelUpgrade.IsPurchased.Value = true;
                await upgradeView.Deselect();
            }
        }

        private void UpgradeLevel()
        {
            RepaintPriceLevel();
            _businessView.UpgradeLevelView.Icon.OnPointerClickAsObservable()
                .Subscribe((data => OnClickUpgradeLevel(data).Forget()))
                .AddTo(_businessView);
        }

        private async UniTaskVoid OnClickUpgradeLevel(PointerEventData x)
        {
            var priceLevel = (_reactiveBusinessModel.Level.Value + 1) * _concreteBusinessDataModel.BaseCost;
            if (!(_concretePlayerPresenter.ReactivePlayer.Balance.Value >= priceLevel)) return;

            _concretePlayerPresenter.ReactivePlayer.Balance.Value -= priceLevel;
            _reactiveBusinessModel.Level.Value++;

            await _businessView.UpgradeLevelView.Select();
            _reactiveBusinessModel.BaseCost.Value = GetIncoming();
            _businessView.GeneralView.RepaintIncomeInfo($"{GetIncoming()}");
            await _businessView.UpgradeLevelView.Deselect();
        }

        private void RepaintPriceLevel()
        {
            var price = (_reactiveBusinessModel.Level.Value + 1) * _concreteBusinessDataModel.BaseCost;
            _businessView.UpgradeLevelView.RepaintPrice(price);
        }

        private void GeneralInfo()
        {
            _reactiveBusinessModel.Name.ObserveEveryValueChanged(x => x.Value).Subscribe(x =>
                {
                    _businessView.GeneralView.RepaintNameInfo(x);
                })
                .AddTo(_businessView);

            _reactiveBusinessModel.Level.ObserveEveryValueChanged(x => x.Value).Subscribe(x =>
                {
                    _concreteBusinessDataModel.Level = _reactiveBusinessModel.Level.Value;
                    _businessView.GeneralView.RepaintLevelInfo(x.ToString());
                    RepaintPriceLevel();
                })
                .AddTo(_businessView);

            _reactiveBusinessModel.BaseCost.ObserveEveryValueChanged(x => _reactiveBusinessModel.Level.Value != 0)
                .Subscribe(x => { _businessView.GeneralView.RepaintIncomeInfo($"{GetIncoming()}"); })
                .AddTo(_businessView);


            _reactiveBusinessModel.DelayedIncoming
                .ObserveEveryValueChanged(x => _reactiveBusinessModel.Level.Value != 0)
                .Subscribe(x =>
                {
                    var timeElapsed = _concreteBusinessDataModel.Progress;
                    var startValue = _businessView.GeneralView.ProgressBar.minValue;
                    var endValue = _businessView.GeneralView.ProgressBar.maxValue;
                    var duration = _concreteBusinessDataModel.DelayedIncoming;
                    Observable.EveryUpdate().Where(_ => _reactiveBusinessModel.Level.Value != 0).Subscribe(x =>
                    {
                        _businessView.GeneralView.ProgressBar.value =
                            Mathf.Lerp(startValue, endValue, timeElapsed / duration);
                        timeElapsed += Time.deltaTime;

                        if (timeElapsed > duration)
                        {
                            OnFinish();
                            timeElapsed = 0;
                        }

                        _concreteBusinessDataModel.Progress = timeElapsed;
                    }).AddTo(_businessView);
                })
                .AddTo(_businessView);
        }

        private void OnFinish()
        {
            _concretePlayerPresenter.OnUpdateBalance(GetIncoming());
        }

        private float GetIncoming()
        {
            var income1 = _reactiveBusinessModel.UpgradeReactiveDataModel1.IsPurchased.Value
                ? _reactiveBusinessModel.UpgradeReactiveDataModel1.IncomeMultiplierPercentages.Value
                : 0f;
            var multiplier1 =
                (income1 / 100f) *
                _concreteBusinessDataModel.BaseCost;

            var income2 = _reactiveBusinessModel.UpgradeReactiveDataModel2.IsPurchased.Value
                ? _reactiveBusinessModel.UpgradeReactiveDataModel2.IncomeMultiplierPercentages.Value
                : 0f;
            var multiplier2 =
                (income2 / 100f) *
                _concreteBusinessDataModel.BaseCost;

            return
                _reactiveBusinessModel.Level.Value * _concreteBusinessDataModel.BaseCost *
                (1 + multiplier1 + multiplier2);
        }
    }
}