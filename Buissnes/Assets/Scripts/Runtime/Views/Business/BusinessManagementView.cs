using Runtime.Save;
using Runtime.Views.Business.ConcreteBusiness.Presenter;
using Runtime.Views.Business.ConcreteBusiness.Views;
using Runtime.Views.Business.Player.Presentor;
using Runtime.Views.Business.Player.View;
using UnityEngine;
using Zenject;

namespace Runtime.Views.Business
{
    public class BusinessManagementView : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private BusinessView _prefabBusiness;
        [SerializeField] private PlayerInfo playerInfo;

        [Inject] private LoaderAndSaverService _loaderAndSaverService;

        private ConcretePlayerPresenter _concretePlayerPresenter;

        private void Start()
        {
            RepaintPlayerInfo();
            RepaintContainer();
        }
        private void RepaintContainer()
        {
            foreach (var businessDataModel in _loaderAndSaverService.BussinesLoadModeL.Data)
            {
                var newBussines = Instantiate(_prefabBusiness, _content);
                var newPresenterBusiness = new ConcreteBusinessPresenter(businessDataModel, newBussines,
                    _concretePlayerPresenter);
                newPresenterBusiness.StartHandler();
            }
        }
        private void RepaintPlayerInfo()
        {
            _concretePlayerPresenter =
                new ConcretePlayerPresenter(_loaderAndSaverService.PlayerLoadModeL.Data[0], playerInfo);
            _concretePlayerPresenter.StartHandler();
        }
    }
}