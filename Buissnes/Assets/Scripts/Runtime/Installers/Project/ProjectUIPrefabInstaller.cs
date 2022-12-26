using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Runtime.Installers.Project
{
    [CreateAssetMenu(menuName = "Installers/UIPrefabInstaller", fileName = "UIPrefabInstaller")]
    public class ProjectUIPrefabInstaller : ScriptableObjectInstaller
    {
        [Header("Base")] [SerializeField] private Canvas _canvas;
        [SerializeField] private EventSystem _eventSystem;

        public override void InstallBindings()
        {
            Container.InstantiatePrefabForComponent<Canvas>(_canvas);
            Container.InstantiatePrefabForComponent<EventSystem>(_eventSystem);
        }
    }
}