using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Windows.Views
{
    public abstract class LayoutViewBase : MonoBehaviour, ILayoutView
    {
        private bool _isRootSet;

        public virtual async UniTask ShowAsync()
        {
            Show();
            await UniTask.Yield();
        }

        public virtual async UniTask HideAsync()
        {
            Hide();
            await UniTask.Yield();
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void SetRoot(Transform uiRoot)
        {
            if (_isRootSet)
                return;
            
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            var minOffset = rectTransform.offsetMin;
            var maxOffset = rectTransform.offsetMax;
            rectTransform.SetParent(uiRoot);
            rectTransform.offsetMin = minOffset;
            rectTransform.offsetMax = maxOffset;

            _isRootSet = true;
        }
    }
}