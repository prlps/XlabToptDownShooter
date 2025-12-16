using System.Collections.Generic;
using UnityEngine;

public class SpellDataBase : ScriptableObject
{
    [SerializeField] private BaseSpellData[] m_spells;

    public IReadOnlyList<BaseSpellData> Spells => m_spells;
}
