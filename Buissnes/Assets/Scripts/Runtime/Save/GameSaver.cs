using System.IO;
using Runtime.Save.Bussines;
using UnityEditor;
using UnityEngine;

namespace Runtime.Save
{
    public static class GameSaver
    {
        public static readonly BussinesSaver BussinesSaver;
        public static readonly PlayerSaver PlayerSaver;

        static GameSaver()
        {
            BussinesSaver = new BussinesSaver();
            PlayerSaver = new PlayerSaver();
        }

#if UNITY_EDITOR
        [MenuItem("Tools/Saver/Reset All")]
#endif
        public static void ResetAll()
        {
            var directory =
                $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}Saves";

            var dirInfo = new DirectoryInfo(directory);
            if (dirInfo.Exists) dirInfo.Delete(true);

            Debug.Log("All Progress deleted!");
        }

#if UNITY_EDITOR
        [MenuItem("Tools/Saver/Reset Bussines Saver")]
#endif
        public static void ResetCandies()
        {
            BussinesSaver.Reset();
            Debug.Log("All Bussines Saver Data has been deleted!");
        }

#if UNITY_EDITOR
        [MenuItem("Tools/Saver/Reset Player Saver")]
#endif
        public static void ResetPlayer()
        {
            PlayerSaver.Reset();
        }

    }
}