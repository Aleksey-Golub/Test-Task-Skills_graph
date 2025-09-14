using System.Collections.Generic;
using CodeBase.UI.Windows.Views;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "Layouts Repository", menuName = "UI/Repository")]
    public sealed class LayoutsRepository : ScriptableObject
    {
        [SerializeField] private List<LayoutViewBase> _views;

        public List<LayoutViewBase> Views => _views;
    }
}
