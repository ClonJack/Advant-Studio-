using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Views.Business.Data
{
    [CreateAssetMenu(fileName = "DataBase_Business", menuName = "Create DataBaseBusiness", order = 0)]
    public class DataBaseBusiness : ScriptableObject
    {
        [SerializeField] private List<ConcreteDataModel> _concreteDataModels;

        public List<ConcreteDataModel> ConcreteDataModels => _concreteDataModels;
    }
}