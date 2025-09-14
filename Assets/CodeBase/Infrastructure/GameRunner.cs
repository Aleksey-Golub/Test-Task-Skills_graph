using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;

        private IObjectResolver _objectResolver;

        [Inject]
        private void Construct(IObjectResolver objectResolver)
        {
            Debug.Log($"[GameRunner] Construct called...");
            _objectResolver = objectResolver;

            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper != null) return;

            _objectResolver.Instantiate(BootstrapperPrefab);
        }
    }
}