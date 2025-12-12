using UnityEngine;

public sealed class ElementData : MonoBehaviour
{
    [SerializeField]
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
