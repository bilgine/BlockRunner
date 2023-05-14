using System.Collections.Generic;
using ServiceLocatorSystem;
using Stack;
using Stack.StackPoolSystem;
using UnityEngine;

namespace StackSpawnerSystem
{
    public class StackSpawner: MonoBehaviour,IService
    {
        [SerializeField] private NormalStack startingStack;
        private Pool _stackPool;
        private List<IStackPiece> _stackList = new List<IStackPiece>();
        private IStackListManager _stackListManager;
        public void OnRegisterComplete()
        {
        }

        public void OnContainerStart()
        {
            _stackPool = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<Pool>();
            _stackListManager = ContainerManager.GetContainer(Container.Game).ServiceLocator.Resolve<StackListManager>();
            _stackList =  _stackListManager.GetStackList();
            SpawnStack();
        }
    
        public void SpawnStack()
        {
            DeSpawnStack();
            AddStack();
        }
        
        public void RestartStackSpawn()
        {
            DeSpawnAllStack();
            SpawnStack();
        }

        private void DeSpawnStack()
        {
            if (_stackList.Count <= 10) return;
            _stackPool.DeSpawn((NormalStack)_stackListManager.GetFirstStack());
            _stackListManager.RemoveFirstStack();
        }
        
        private void AddStack()
        {
            var stack = _stackList.Count > 0 ? _stackPool.Spawn(transform, (NormalStack)_stackListManager.GetLastStack())
                : _stackPool.Spawn(transform, startingStack);
            _stackListManager.AddStack(stack);
        }

        public void DeSpawnAllStack()
        {
            
            foreach (var stack in _stackList)
            {
                _stackPool.DeSpawn((NormalStack)stack);
            }
            _stackList.Clear();
        }
    }
}