using UnityEngine;

namespace Tool
{
    internal class DontDestroyOnLoadObject : MonoBehaviour
    {
        private void Awake()
        {
            if (enabled)
                DontDestroyOnLoad(gameObject);
        }
    }
}
