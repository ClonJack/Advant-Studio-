using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Runtime.Adressable_
{
    public class AddressableModel
    {
        public async UniTask<List<T>> LoadAddressableFolder<T>(string key)
        {
            var locations =
                Addressables.LoadResourceLocationsAsync(key);
            await locations;
            var load = new List<T>();
            foreach (var location in locations.Result)
            {
                var handle =
                    Addressables.LoadAssetAsync<T>(location);
                await handle;

                load.Add(handle.Result);
            }

            return load;
        }
    }
}