using System;
using System.Collections.Generic;
using Newtonsoft.Json;
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
        public readonly LoadModeL<ConcreteBusinessDataModel> BussinesLoadModeL =
            new("Companies", GameSaver.BussinesSaver);

        public LoadModeL<ConcretePlayerModel> PlayerLoadModeL =
            new("User", GameSaver.PlayerSaver);

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void Initialize()
        {
            Observable.EveryApplicationPause().Subscribe(x =>
            {
                SaveCompaniesData();
                SavePlayerData();
            }).AddTo(_compositeDisposable);

#if UNITY_EDITOR

            Observable.OnceApplicationQuit().Subscribe(x =>
            {
                Debug.Log("OnceApplicationQuit");
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
            for (var i = 0; i < BussinesLoadModeL.Data.Count; i++)
            {
                GameSaver.BussinesSaver.Save(BussinesLoadModeL.Data[i], i);
            }
        }

        private void SavePlayerData()
        {
            GameSaver.PlayerSaver.Save(PlayerLoadModeL.Data[0]);
        }
    }
}

public class LoadModeL<T>
{
    public List<T> Data;

    private List<T> Load(string nameFolder, Saver<T> saver)
    {
        var list = new List<T>();
        var configOnInit = Resources.LoadAll<TextAsset>(nameFolder);
        for (var i = 0; i < configOnInit.Length; i++)
        {
            if (saver.IsSaveExists(i))
            {
                list.Add(saver.Load(i));
                continue;
            }

            try
            {
                var config = configOnInit[i];
                var deserializedData = JsonConvert.DeserializeObject<T>(config.text);

                list.Add(deserializedData);
                saver.Save(deserializedData, i);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        return list;
    }

    public LoadModeL(string nameFolder, Saver<T> saver)
    {
        Data = Load(nameFolder, saver);
    }
}