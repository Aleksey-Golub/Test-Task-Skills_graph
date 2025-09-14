using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class UIRoot : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
