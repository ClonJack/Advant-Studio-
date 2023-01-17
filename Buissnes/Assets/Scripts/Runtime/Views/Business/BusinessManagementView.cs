using Runtime.Save;
using Runtime.Views.Business.ConcreteBusiness.Presenter;
using Runtime.Views.Business.ConcreteBusiness.Views;
using Runtime.Views.Business.Player.Presentor;
using Runtime.Views.Business.Player.View;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using UniTaskVoid = Cysharp.Threading.Tasks.UniTaskVoid;

namespace Runtime.Views.Business
{
    public class BusinessManagementView : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private BusinessView _prefabBusiness;
        [FormerlySerializedAs("playerInfo")] [SerializeField] private PlayerView playerView;

        [Inject] private LoaderAndSaverService _loaderAndSaverService;

        private ConcretePlayerPresenter _concretePlayerPresenter;

        public void Repaint()
        {
            RepaintContainer().Forget();
            RepaintPlayerInfo().Forget();
        }

        private async UniTaskVoid RepaintContainer()
        {
            var data = await _loaderAndSaverService.Companies.GetData();
            foreach (var businessDataModel in data)
            {
                var newBussines = Instantiate(_prefabBusiness, _content);
                var newPresenterBusiness = new ConcreteBusinessPresenters(businessDataModel, newBussines,
                    _concretePlayerPresenter);
                newPresenterBusiness.StartHandler();
            }
        }

        private async UniTaskVoid RepaintPlayerInfo()
        {
            var data = await _loaderAndSaverService.PlayerModel.GetData();
            _concretePlayerPresenter =
                new ConcretePlayerPresenter(data[0], playerView);
            _concretePlayerPresenter.StartHandler();
        }
    }
}