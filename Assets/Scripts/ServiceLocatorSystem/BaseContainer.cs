using UnityEngine;

namespace ServiceLocatorSystem
{
    public abstract class BaseContainer : MonoBehaviour
    {
        public readonly ServiceLocator ServiceLocator = new ServiceLocator();
        
        public abstract Container Type { get; }
        private bool _isInstalled;
        
        public void Awake()
        {
            if (!ContainerManager.AddContainer(this))
            {
                Destroy(gameObject);
                return;
            }
            Install();
            _isInstalled = true;
        }

        public void Start()
        {
            if(_isInstalled)
                ServiceLocator.ContainerStart();
        }

        protected abstract void Install();
    }
}