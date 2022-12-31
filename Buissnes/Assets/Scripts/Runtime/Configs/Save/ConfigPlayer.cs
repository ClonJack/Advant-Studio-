using System.IO;
using JetBrains.Annotations;
using NaughtyAttributes;
using Runtime.Save;
using Runtime.Save.ConcreteModel.Player;
using UnityEngine;

namespace Runtime.Configs.Save
{
    [CreateAssetMenu(fileName = "Player", menuName = "Config/Player", order = 0)]
    public class ConfigPlayer : ConfigBase
    {
        [SerializeField] private ConcretePlayerModel _concretePlayer;
        protected override string DirectoryName => GameSaver.PlayerSaver.DirectoryName;
        protected override string FileName => GameSaver.PlayerSaver.DirectoryName;

        [Button]
        [UsedImplicitly]
        protected override void GenerateConfig()
        {
            var separator = Path.DirectorySeparatorChar;
            var filePath =
                $"{(Application.dataPath)}{separator}Resources{separator}{DirectoryName}{separator}{FileName}{0}";
            GameSaver.PlayerSaver.Save(_concretePlayer, 0, filePath);
            base.GenerateConfig();
        }
    }
}