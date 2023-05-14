using UnityEngine;

namespace Stack.StackPoolSystem
{
    public interface IPool
    {
        public NormalStack Spawn(Transform parent, NormalStack previousStack);
        public void DeSpawn(NormalStack obj);
    }
}