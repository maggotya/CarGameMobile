using UnityEngine;

namespace JoostenProductions {
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
        protected abstract bool DoNotDestroyOnLoad { get; }
        protected static bool isShuttingDown = false;

        private static T instance;
        private static object objectLock;

        private static readonly System.Type instanceType = typeof(T);

        public static T Instance {
            get {
                if(isShuttingDown) {
                    Debug.LogWarning("Tried to access " + instanceType.Name + " while the application is going to quit! This is not allowed.");

                    return null;
                }

                if(objectLock == null) {
                    objectLock = new object();
                }

                lock (objectLock) {
                    if(instance != null) return instance;

                    instance = (T)FindObjectOfType(instanceType);

                    if(instance != null) return instance;

                    instance = (T)new GameObject(instanceType.Name).AddComponent(instanceType);

                    SingletonBehaviour<T> singleton = instance as SingletonBehaviour<T>;
                    if(singleton != null && singleton.DoNotDestroyOnLoad) DontDestroyOnLoad(instance);

                    return instance;
                }
            }
        }

        private void OnApplicationQuit() {
            isShuttingDown = true;
        }

        private void OnDestroy() {
            isShuttingDown = true;
        }
    }
}