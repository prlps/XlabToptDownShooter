using System;
using Magic.Spells.Aoe;
using Magic.Spells.Projectiles;
using UnityEngine;
using UnityEngine.Pool;

namespace Magic.Systems
{
    public sealed class SpellCaster
    {
        private readonly Transform m_casterTransform;
        private ObjectPool<GameObject> m_visualEffectPool;

        private readonly bool m_isSingleSpell;

        public SpellCaster(Transform casterTransform, bool isSingleSpell = false)
        {
            m_casterTransform = casterTransform;
            m_isSingleSpell = isSingleSpell;
        }

        public void Cast(BaseSpellData spell, Vector3 worldPosition)
        {
            if (spell == null)
            {
                return;
            }

            switch (spell)
            {
                case SelfSpellData selfSpell:
                    CastSelf(selfSpell);
                    break;
                case TargetSpellData targetSpell:
                    CastTarget(targetSpell, worldPosition);
                    break;
                case NonTargetSpellData nonTargetSpell:
                    CastNonTarget(nonTargetSpell);
                    break;
                case AoeSpellData aoeSpell:
                    CastAoe(aoeSpell, aoeSpell.isTarget ? worldPosition : m_casterTransform.position);
                    break;
            }
        }

        private void CastSelf(SelfSpellData spell)
        {
            if (spell.visualEffect)
            {
                var visualEffect = UnityEngine.Object.Instantiate(spell.visualEffect, m_casterTransform.position, Quaternion.identity);
                SetLayer(visualEffect);
            }

            var effectable = m_casterTransform.GetComponent<IEffectable>();
            spell.effects.ApplyEffect(effectable);
        }

        private void CastTarget(TargetSpellData spell, Vector3 worldPosition)
        {
            if (!spell.visualEffect)
            {
                throw new NullReferenceException("Target spell must have visualEffect");
            }

            var projectile = UnityEngine.Object.Instantiate(spell.visualEffect, m_casterTransform.position, Quaternion.identity);
            SetLayer(projectile);

            var spellProjectile =
                projectile.GetComponent<ISpellProjectile>() ??
                projectile.AddComponent<FallbackSpellProjectile>();

            spellProjectile.Initialize(worldPosition, spell.speed, spell.effects);
        }

        private void CastNonTarget(NonTargetSpellData spell)
        {
        }

        private void CastAoe(AoeSpellData spell, Vector3 worldPosition)
        {
            GameObject aoe;

            if (m_isSingleSpell)
            {
                m_visualEffectPool ??= new ObjectPool<GameObject>(
                    () => Create(),
                    gm => gm.SetActive(true),
                    gm => gm.SetActive(false),
                    UnityEngine.Object.Destroy);

                aoe = m_visualEffectPool.Get();
            }
            else
            {
                aoe = Create();
            }

            SetLayer(aoe);
            aoe.transform.position = worldPosition;

            var spellAoe =
                aoe.GetComponent<ISpellAoe>() ??
                aoe.AddComponent<SpellAoe>();

            spellAoe.Initialize(worldPosition, spell.radius, spell.effects);

            GameObject Create() =>
                UnityEngine.Object.Instantiate(spell.visualEffect, m_casterTransform.position, Quaternion.identity);
        }

        private void SetLayer(GameObject visualEffect) =>
            visualEffect.layer = m_casterTransform.gameObject.layer;
    }
}
