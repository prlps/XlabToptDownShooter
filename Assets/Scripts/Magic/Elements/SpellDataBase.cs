using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellDataBase", menuName = "XLab/Magic/Spell/SpellDataBase")]
public class SpellDataBase : ScriptableObject
{
    [SerializeField] private BaseSpellData[] m_spells;

    public IReadOnlyList<BaseSpellData> Spells => m_spells;
}
