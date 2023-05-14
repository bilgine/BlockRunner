using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ServiceLocatorSystem
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
    
    
        public void Register<T>(T serviceInstance = null) where T : class, new()
        {
            if (!CanRegister<T>()) return;
        
            if (typeof(T).IsSubclassOf(typeof(MonoBehaviour)))
            {
                throw new Exception("Cannot register MonoBehaviour with Register method. Use RegisterMono instead.");
            }

            if (serviceInstance == null) serviceInstance = new T();
        
            _services[typeof(T)] = serviceInstance;
            InvokeRegisterComplete<T>(serviceInstance);
        }
    
        public void RegisterMono<T>(T serviceInstance = null, Transform parent = null) where T : MonoBehaviour
        {
            if (!CanRegister<T>()) return;
        
            if (ReferenceEquals(serviceInstance, null) && ReferenceEquals(serviceInstance = Object.FindObjectOfType<T>(), null))
            {
                GameObject go = new();
                Transform tform = go.transform;
                tform.parent = parent;
                tform.position = Vector3.zero;
                go.name = typeof(T).ToString();
                serviceInstance = go.AddComponent<T>();
            }

            _services[typeof(T)] = serviceInstance;
        }
    
        public void ContainerStart()
        {
            foreach (KeyValuePair<Type, object> pair in _services)
            {
                object obj = pair.Value;
                Type type = pair.Key;
                if(!typeof(IService).IsAssignableFrom(type)) continue;
                IService service = (IService) obj;
                service.OnContainerStart();
            }
        }
    
        private void InvokeRegisterComplete<T>(object instance)
        {
            if(!typeof(IService).IsAssignableFrom(typeof(T))) return;
            IService service = (IService) instance;
            service.OnRegisterComplete();
        }
    
        public T Resolve<T>()
        {
            if (IsServiceAvailable<T>())
            {
                return (T)_services[typeof(T)];
            }
            Debug.Log("Service not found: " + typeof(T));
            throw new ServiceNotFoundException();
        }
    
        private bool CanRegister<T>()
        {
            if (_services.ContainsKey(typeof(T)))
            {
                return false;
            }
            return true;
        }
    
        private bool IsServiceAvailable<T>()
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                Debug.LogError("Service " + typeof(T) +" cannot be found");
                return false;
            }
            return true;
        }
    
        private class ServiceNotFoundException : Exception
        {
        }
    }
}


    
