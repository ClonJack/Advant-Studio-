using System.IO;
using UnityEditor;
using UnityEngine;

namespace Runtime.Configs
{
    public abstract class ConfigBase : ScriptableObject
    {
        protected abstract string DirectoryName { get; }
        protected abstract string FileName { get; }

        protected virtual void GenerateConfig()
        {
            Debug.Log(
                $"{(Application.dataPath)}{Path.DirectorySeparatorChar}Resources{Path.DirectorySeparatorChar}{DirectoryName}");
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
    }
}