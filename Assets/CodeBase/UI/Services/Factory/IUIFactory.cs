using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory
    {
        Transform UIRoot { get; }
        void CreateUIRoot();
    }
}