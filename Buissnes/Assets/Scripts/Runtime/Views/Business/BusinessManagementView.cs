using UnityEngine;

namespace Runtime.Views.Business
{
    public class BusinessManagementView : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private BusinessView _prefabBusiness;

        private void SetContent(int count)
        {
            for (var i = 0; i < count; i++)
            {
                Instantiate(_prefabBusiness, _content);
            }
        }
    }
}