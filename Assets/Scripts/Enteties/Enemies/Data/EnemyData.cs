using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public AttackEnemyType enemyType { get; private set; }
    [SerializeField] [Min(0)] public float health;

    [FormerlySerializedAs("spellData")]
    [SerializeField] private BaseSpellData m_defaultSpell;

    [field: SerializeField] [Range(0f, 100f)] public float speed { get; private set; }
    [field: SerializeField] [Min(0f)] public float attackTime { get; private set; }
    [field: SerializeField] [Min(0f)] public float attackRange { get; private set; }
    [field: SerializeField] private SpellEnemyData[] m_spells;

    public BaseSpellData defaultSpell => m_defaultSpell;
    public IReadOnlyList<SpellEnemyData> spells => m_spells;

    [Serializable]
    public struct SpellEnemyData
    {
        [SerializeField] public BaseSpellData spell;
        [SerializeField] public int count;
    }
}
