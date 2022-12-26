using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Runtime.Input
{
    public class MasterInput : IInitializable , IDisposable
    {
        private PlayerInput _playerInput;
        
        public void Initialize()
        {
            _playerInput = new PlayerInput();
            
            _playerInput?.Enable();
            Debug.Log("PlayerInput");
        }

        public void Dispose()
        {
            _playerInput?.Dispose();
        }
    }
}