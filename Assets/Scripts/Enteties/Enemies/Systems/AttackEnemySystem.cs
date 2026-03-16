using System.Linq;
using System.Collections.Generic;
using Magic.Systems;
using UnityEngine;

namespace Enteties.Enemies.Systems
{
    public sealed class AttackEnemySystem : MonoBehaviour
    {
        private Transform m_target;
        private IReadOnlyList<EnemyData.SpellEnemyData> m_spells;
        private SpellCaster m_spellCaster;

        private float m_attackTime;
        private float m_cooldownTimer;
        private bool m_isInitialized;

        private int m_maxCount;
        private int m_count;
        private BaseSpellData m_defaultSpell;

        public void Initialize(
            BaseSpellData defaultSpell,
            IReadOnlyList<EnemyData.SpellEnemyData> spells,
            Transform target,
            float attackTime)
        {
            if (m_isInitialized)
            {
                return;
            }

            m_defaultSpell = defaultSpell;
            m_target = target;
            m_attackTime = attackTime;
            m_spellCaster = new SpellCaster(transform);

            m_spells = spells != null
                ? spells.OrderBy(s => s.count).ToArray()
                : new EnemyData.SpellEnemyData[0];

            m_maxCount = m_spells.Count > 0
                ? m_spells.Last().count
                : 0;

            m_isInitialized = true;
        }

        private void Update()
        {
            if (!m_isInitialized)
            {
                return;
            }

            if (m_cooldownTimer > 0)
            {
                m_cooldownTimer -= Time.deltaTime;
            }
        }

        public bool TryAttack()
        {
            if (!m_isInitialized || !m_target)
            {
                return false;
            }

            if (m_cooldownTimer > 0)
            {
                return false;
            }

            m_count++;
            BaseSpellData spellToCast = null;

            if (m_spells.Count > 0)
            {
                var spell = m_spells.FirstOrDefault(s => s.count == m_count);
                spellToCast = spell.spell;
            }

            if (spellToCast == null)
            {
                spellToCast = m_defaultSpell;
            }

            m_spellCaster.Cast(spellToCast, m_target.position);

            if (m_maxCount > 0 && m_count == m_maxCount)
            {
                m_count = 0;
            }

            m_cooldownTimer = m_attackTime;
            return true;
        }
    }
}
