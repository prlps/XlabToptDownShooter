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
        
        private void Initialize(BaseSpellData spell, float attackTime, Transform target)
        {

            if (m_isInitialized)
            {
                return;
            }
            
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
            
            return true;
        }
    }
}