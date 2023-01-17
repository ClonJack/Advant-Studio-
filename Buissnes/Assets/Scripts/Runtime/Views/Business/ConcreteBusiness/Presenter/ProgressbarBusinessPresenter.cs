using UniRx;
using UnityEngine;

namespace Runtime.Views.Business.ConcreteBusiness.Presenter
{
    public class ProgressbarBusinessPresenter
    {
        private readonly ConcreteBusinessPresenters _concreteBusinessPresenters;

        public ProgressbarBusinessPresenter(ConcreteBusinessPresenters concreteBusinessPresenters)
        {
            _concreteBusinessPresenters = concreteBusinessPresenters;
        }
        
        public void Start()
        {
            RepaintProgressBar();
        }
        private void RepaintProgressBar()
        {
            _concreteBusinessPresenters.ReactiveBusinessModel.DelayedIncoming
                .ObserveEveryValueChanged(x => _concreteBusinessPresenters.ReactiveBusinessModel.Level.Value != 0)
                .Subscribe(x =>
                {
                    var timeElapsed = _concreteBusinessPresenters.СoncreteBusinessDataModel.Progress;
                    var startValue = _concreteBusinessPresenters.BusinessView.GeneralView.ProgressBar.minValue;
                    var endValue = _concreteBusinessPresenters.BusinessView.GeneralView.ProgressBar.maxValue;
                    var duration = _concreteBusinessPresenters.СoncreteBusinessDataModel.DelayedIncoming;
                    Observable.EveryUpdate()
                        .Where(_ => _concreteBusinessPresenters.ReactiveBusinessModel.Level.Value != 0).Subscribe(x =>
                        {
                            _concreteBusinessPresenters.BusinessView.GeneralView.ProgressBar.value =
                                Mathf.Lerp(startValue, endValue, timeElapsed / duration);
                            timeElapsed += Time.deltaTime;

                            if (timeElapsed > duration)
                            {
                                OnFinish();
                                timeElapsed = 0;
                            }

                            _concreteBusinessPresenters.СoncreteBusinessDataModel.Progress = timeElapsed;
                        }).AddTo(_concreteBusinessPresenters.BusinessView);
                })
                .AddTo(_concreteBusinessPresenters.BusinessView);
        }
        private void OnFinish()
        {
            _concreteBusinessPresenters.ConcretePlayerPresenter.OnUpdateBalance(
                _concreteBusinessPresenters.GetIncoming());
        }
    }
}