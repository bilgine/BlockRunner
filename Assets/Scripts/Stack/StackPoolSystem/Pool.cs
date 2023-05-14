using System.Collections.Generic;
using UnityEngine;

namespace Stack.StackPoolSystem
{
    public class Pool: MonoBehaviour, IPool
    {
        [SerializeField] NormalStack stackPrefab;
        private Queue<NormalStack> _inactiveItems;

        private void Awake()
        {
            _inactiveItems = new Queue<NormalStack>();
        }

        public NormalStack Spawn(Transform parent, NormalStack previousStack)
        {
            if (_inactiveItems.Count > 0)
            {
                NormalStack obj = _inactiveItems.Dequeue();
                obj.InitializeStackPiece(previousStack);
                Transform tform = obj.transform;
                tform.SetParent(parent);
                return obj;
            }
            else
            {
                NormalStack obj = Instantiate(stackPrefab, parent);
                obj.InitializeStackPiece(previousStack);
                return obj;
            }
        }
     
        public void DeSpawn(NormalStack obj)
        {
            if (obj == null)
                return;

            obj.SetStackDefault();
            _inactiveItems.Enqueue(obj);
        }
    }
}