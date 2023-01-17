using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Newtonsoft.Json;
using Runtime.Adressable_;
using Runtime.Save.Base;
using Runtime.Save.ConcreteModel.Business;
using Runtime.Save.ConcreteModel.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace Runtime.Save
{
    public class LoaderAndSaverService : IInitializable, IDisposable
    {
        public LoadModeL<ConcretePlayerModel> PlayerModel = new(GameSaver.PlayerSaver);
        public LoadModeL<ConcreteBusinessDataModel> Companies = new(GameSaver.BussinesSaver);

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void Initialize()
        {
#if UNITY_EDITOR

            Observable.OnceApplicationQuit().Subscribe(x =>
            {
                Debug.Log("OnceApplicationQuit");
                SaveCompaniesData();
                SavePlayerData();
            }).AddTo(_compositeDisposable);
#else
            Observable.EveryApplicationPause().Subscribe(x =>
            {
                SaveCompaniesData();
                SavePlayerData();
            }).AddTo(_compositeDisposable);
#endif
        }

        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }

        private void SaveCompaniesData()
        {
            var data = Companies.GetData().GetAwaiter().GetResult();
            if (data.Count == 0) return;
            for (var i = 0; i < data.Count; i++)
            {
                GameSaver.BussinesSaver.Save(data[i], i);
            }
        }

        private void SavePlayerData()
        {
            var data = PlayerModel.GetData().GetAwaiter().GetResult();
            if (data.Count == 0) return;
            GameSaver.PlayerSaver.Save(data[0]);
        }
    }
}

public class LoadModeL<T>
{
    private readonly Saver<T> _saver;

    private List<T> _data;

    public async UniTask<List<T>> GetData()
    {
        return _data ??= await LoadJson();
    }

    private async UniTask<List<T>> LoadJson()
    {
        var data = new List<T>();
        var configOnInit =
            await new AddressableModel()
                .LoadAddressableFolder<TextAsset>(_saver.DirectoryName);
        for (var i = 0; i < configOnInit.Count; i++)
        {
            var config = configOnInit[i];
            if (_saver.IsSaveExists(i))
            {
                data.Add(_saver.Load(i));
                continue;
            }

            try
            {
                var deserializedData = JsonConvert.DeserializeObject<T>(config.text);

                data.Add(deserializedData);
                _saver.Save(deserializedData, i);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        return data;
    }


    public LoadModeL(Saver<T> saver)
    {
        _saver = saver;
    }
}