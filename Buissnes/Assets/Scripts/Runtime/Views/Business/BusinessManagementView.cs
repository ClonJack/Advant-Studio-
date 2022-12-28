using Runtime.Save;
using Runtime.Views.Business.ConcreteBussines.Views;
using UnityEngine;
using Zenject;

namespace Runtime.Views.Business
{
    public class BusinessManagementView : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private BusinessView _prefabBusiness;
        [SerializeField] private PlayerInfo playerInfo;
        [Inject] private LoaderService _loaderService;

        private void Start()
        {
            RepaintContainer();
            RepaintPlayerInfo();
        }

        private void RepaintContainer()
        {
            foreach (var businessDataModel in _loaderService.BussinesLoadModeL.Data)
            {
                var newBussines = Instantiate(_prefabBusiness, _content);
                newBussines.Repaint(businessDataModel);
            }
        }

        private void RepaintPlayerInfo()
        {
            playerInfo.Repaint(_loaderService.PlayerLoadModeL.Data[0]);
        }
    }
}