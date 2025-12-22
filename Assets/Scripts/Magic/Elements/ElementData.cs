using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "ElementsData", menuName = "XLab/Magic/ElementsData")]
public sealed class ElementData : ScriptableObject
{
    [SerializeField]
    private Item[] m_items;

    public IReadOnlyList<Item> Items => m_items;

    [Serializable]
    public sealed class Item
    {
        [SerializeField] private string m_elementName;
        [SerializeField] private ElementType m_type;
        [SerializeField] private Sprite m_icon;

        public Sprite icon => m_icon;
        public ElementType type => m_type;
        public string elementName => m_elementName;
    }
}
