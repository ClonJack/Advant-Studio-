using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Runtime.Save.Base
{
    public abstract class Saver<T>
    {
        private const int DefaultSlot = 0;
        private const int MaxSavesCount = 10;
        protected abstract string DirectoryName { get; }
        protected abstract string FileName { get; }

        public virtual void Save(T data, int slot = DefaultSlot)
        {
            var filepath = GetFilepath(slot);
            var serializedData = JsonConvert.SerializeObject(data);

            var file = new FileInfo($"{filepath}");
            file.Directory?.Create();

            File.WriteAllText(file.FullName, serializedData);
        }

        public virtual T Load(int slot = DefaultSlot)
        {
            if (!IsSaveExists(slot)) return default;

            var filepath = GetFilepath(slot);
            var data = File.ReadAllText(filepath);

            try
            {
                var deserializedData = JsonConvert.DeserializeObject<T>(data);
                return deserializedData;
            }
            catch (Exception e)
            {
                Debug.LogError(
                    $"Unable to load {GetFullFileName(slot)}. Resetting...");
                Debug.LogException(e);
                Reset();
            }

            return default;
        }

        public bool IsSaveExists(int slot = DefaultSlot)
        {
            var path = $"{GetFilepath(slot)}";
            return File.Exists(path);
        }

        public void Reset()
        {
            for (int i = 0; i < MaxSavesCount; i++)
            {
                string filepath = GetFilepath(i);

                if (!File.Exists(filepath)) continue;

                File.Delete(filepath);
            }
        }

        private string GetFullFileName(int slot)
            => $"{FileName}{slot}";

        public virtual string GetFilepath(int slot)
        {
            char separator = Path.DirectorySeparatorChar;
            return
                $"{Application.persistentDataPath}{separator}Saves{separator}{DirectoryName}{separator}{GetFullFileName(slot)}.json";
        }
    }
}