using JetBrains.Annotations;
using NaughtyAttributes;
using Runtime.Save;
using Runtime.Save.ConcreteModel.Player;
using UnityEngine;

namespace Runtime.Configs
{
    [CreateAssetMenu(fileName = "Player", menuName = "Config/Player", order = 0)]
    public class ConfigPlayer : ScriptableObject
    {
        [SerializeField] private ConcretePlayerModel _concretePlayer;
        
        [Button]
        [UsedImplicitly]
        private void GenerateConfig()
        {
            GameSaver.PlayerSaver.Save(_concretePlayer);
            Debug.Log(GameSaver.PlayerSaver.GetFilepath(0));
        }
    }
}