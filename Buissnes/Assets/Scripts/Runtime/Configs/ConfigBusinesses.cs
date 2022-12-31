using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using NaughtyAttributes;
using Runtime.Save;
using Runtime.Save.ConcreteModel.Business;
using UnityEngine;

namespace Runtime.Configs
{
    [CreateAssetMenu(fileName = "Business", menuName = "Config/Business", order = 0)]
    public class ConfigBusinesses : ConfigBase
    {
        [SerializeField] private List<ConcreteBusinessDataModel> _concreteDataModels;
        protected override string DirectoryName => GameSaver.BussinesSaver.DirectoryName;
        protected override string FileName => GameSaver.BussinesSaver.DirectoryName;

        [Button]
        [UsedImplicitly]
        protected override void GenerateConfig()
        {
            var separator = Path.DirectorySeparatorChar;
            for (var i = 0; i < _concreteDataModels.Count; i++)
            {
                var filePath =
                    $"{(Application.dataPath)}{separator}Resources{separator}{DirectoryName}{separator}{FileName}{i}";
                GameSaver.BussinesSaver.Save(_concreteDataModels[i], i, filePath);
            }
            base.GenerateConfig();
        }
    }
}