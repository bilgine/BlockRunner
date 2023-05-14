using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocatorSystem
{
    public class ContainerManager
    {
        private static readonly Dictionary<Container, BaseContainer> Containers = new();

        public static BaseContainer GetContainer(Container name)
        {
            if (Containers.ContainsKey(name))
            {
                return Containers[name];
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Container " + name + " is already created");
#endif  
            }

            return null;
        }
        
        public static bool AddContainer(BaseContainer container)
        {
            if (Containers.ContainsKey(container.Type))
            {
#if UNITY_EDITOR
                Debug.Log("Container " + container.Type + " is already created");
#endif
                return false;
            }
            else
            {
                Containers.Add(container.Type, container);
                return true;
            }
        }
        
    }
}

public enum Container
{
    Game = 0
}