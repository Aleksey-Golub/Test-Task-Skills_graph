using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public void UnLoad(string name, Action onUnLoaded = null) =>
            _coroutineRunner.StartCoroutine(UnLoadScene(name, onUnLoaded));

        private IEnumerator UnLoadScene(string nextScene, Action onUnLoaded = null)
        {
            AsyncOperation waitUnLoadScene = SceneManager.UnloadSceneAsync(nextScene);

            while (!waitUnLoadScene.isDone)
                yield return null;

            onUnLoaded?.Invoke();
        }

        public void Load(string name, Action onLoaded = null, LoadSceneMode loadSceneMode = LoadSceneMode.Single) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded, loadSceneMode));

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null,
            LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene, loadSceneMode);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}