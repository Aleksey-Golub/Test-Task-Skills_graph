using System.Collections.Generic;
using CodeBase.UI.Windows.Presenters;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Services.UIController
{
    public class UIController : IUIController
    {
        private readonly Dictionary<WindowId, ILayoutPresenter> _availablePresenters = new();
        private readonly List<WindowId> _shownPopupPresenters = new();

        public void AddPresenter(ILayoutPresenter targetPresenter)
        {
            _availablePresenters.Add(targetPresenter.WindowId, targetPresenter);
        }

        public void RemovePresenter(ILayoutPresenter targetPresenter)
        {
            _availablePresenters.Remove(targetPresenter.WindowId);
        }

        public async UniTask Open(WindowId windowId)
        {
            if (!TryGetPresenterByType(windowId, out var targetPresenter))
            {
                Debug.LogError($"[UIController.Open] Couldn't find layout presenter for {windowId}. Make sure it was registered.");
                return;
            }

            _shownPopupPresenters.Add(windowId);
            await targetPresenter.ActivateAsync();
        }

        public bool IsOpened(WindowId windowId)
        {
            return _shownPopupPresenters.Contains(windowId);
        }

        public async UniTask Close(WindowId windowId)
        {
            if (!TryGetPresenterByType(windowId, out var targetPresenter))
            {
                Debug.LogError($"[UIController.Close] Couldn't find layout presenter for {windowId}. Make sure it was registered.");
                return;
            }
            
            _shownPopupPresenters.Remove(windowId);
            await targetPresenter.DeactivateAsync();
        }
        
        private bool TryGetPresenterByType(WindowId windowId, out ILayoutPresenter presenter)
        {
            return _availablePresenters.TryGetValue(windowId, out presenter);
        }
    }

    public enum WindowId
    {
        None = 0,
        MainScreen = 1,
        SkillsTreeScreen = 2,
    }
}