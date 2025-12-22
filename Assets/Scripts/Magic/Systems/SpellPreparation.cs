using System;
using System.Collections.Generic;

namespace Magic.Systems
{
    public sealed class SpellPreparation
    {
        public event Action<IReadOnlyList<ElementType>> ElementChanged;
        public event Action OverflowOccurred;

        private readonly MagicConfig m_magicConfig;
        private readonly List<ElementType> m_elements = new();

        public SpellPreparation(MagicConfig magicConfig)
        {
            m_magicConfig = magicConfig ?? throw new ArgumentNullException(nameof(magicConfig));
        }

        public void AddElement(ElementType elementType)
        {
            if (m_elements.Count >= m_magicConfig.MaxElements)
            {
                Clear();
                OverflowOccurred?.Invoke();
                return;
            }

            m_elements.Add(elementType);
            ElementChanged?.Invoke(m_elements);
        }

        public bool TryGetSpell(out BaseSpellData spell)
        {
            spell = null;

            if (m_elements.Count == 0)
            {
                return false;
            }

            foreach (var spellData in m_magicConfig.SpellDataBase.Spells)
            {
                if (IsMatchingCombination(spellData.combination))
                {
                    spell = spellData;
                    return true;
                }
            }

            return false;
        }

        private bool IsMatchingCombination(IReadOnlyList<ElementType> combination)
        {
            if (combination == null || combination.Count != m_elements.Count)
            {
                return false;
            }

            for (var i = 0; i < combination.Count; i++)
            {
                if (combination[i] != m_elements[i])
                {
                    return false;
                }
            }

            return true;
        }

        public void Clear()
        {
            m_elements.Clear();
            ElementChanged?.Invoke(m_elements);
        }
    }
}