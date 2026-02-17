using System;
using Magic.Systems;
using UnityEngine;

namespace Enteties.Enemies.Systems
{
    public class AttackEnemySystem : MonoBehaviour

    {
        private Transform m_target;
        private BaseSpellData m_spell;
        private float m_cooldownTimer;
        private SpellCaster m_spellCaster;
        
        private float m_attackTime;
        private bool m_isInitialized;
        
        private int m_maxCount;
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
        
        
        m_maxCount = spells[^1].count
        private void Initialize(BaseSpellData spell, float attackTime, Transform target)
        {

            if (m_isInitialized)
            {
                return;
            }

            m_spells = spells.OrderBy(spell => spell.count);
            
            m_spell = spell;
            m_target = target;
            m_attackTime = attackTime;
            m_spell = new SpellCaster(transform);
            
            m_isInitialized = true;
        }

        public bool TryAttack()
        {
            if (!m_isInitialized || !m_target)
            {
                return false;
            }
            
            m_spellCaster.Cast(m_spell, m_target.position);
            m_cooldownTimer = m_attackTime;

            if (m_cooldownTimer > 0)
            {
                return false;
            }

            m_count++;
            var spell = m_spells.FirstOrDefault(spell => spell.count == m_count);

            if (spell.spell is null)
            {
                m_spellCaster.Cast(m_spells[0].spell, m_target.position);
            }
            else
            {
                m_spellCaster.Cast(spell.spell, m_target.position);
            }

            if (m_count == m_maxCount)
            {
                m_count = 0;
            }
            
            return true;
            
            
        }
    }
}