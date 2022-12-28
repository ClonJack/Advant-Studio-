using System.Collections.Generic;
using JetBrains.Annotations;
using NaughtyAttributes;
using Runtime.Save;
using Runtime.Save.ConcreteModel.Business;
using UnityEngine;

namespace Runtime.Configs
{
    [CreateAssetMenu(fileName = "Business", menuName = "Config/Business", order = 0)]
    public class ConfigBusinesses : ScriptableObject
    {
        [SerializeField] private List<ConcreteBusinessDataModel> _concreteDataModels;

        [Button]
        [UsedImplicitly]
        private void GenerateConfig()
        {
            for (var i = 0; i < _concreteDataModels.Count; i++)
            {
                GameSaver.BussinesSaver.Save(_concreteDataModels[i], i);
                Debug.Log(GameSaver.BussinesSaver.GetFilepath(i));
            }
        }
    }
}