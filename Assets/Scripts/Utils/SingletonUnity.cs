using UnityEngine;

namespace Solitaire.Utils
{
    public class SingletonUnity<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool _quitting = false;
        private static readonly object _lock = new object();

        public static T Instance
        {
            get
            {
                if (_quitting) return null;

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindAnyObjectByType<T>();

                        if (_instance == null)
                        {
                            GameObject obj = new GameObject(typeof(T).Name);
                            _instance = obj.AddComponent<T>();
                            DontDestroyOnLoad(obj);
                        }
                    }

                    return _instance;
                }
            }
        }

        protected virtual void OnDestroy()
        {
            _quitting = true;
        }
    }
}