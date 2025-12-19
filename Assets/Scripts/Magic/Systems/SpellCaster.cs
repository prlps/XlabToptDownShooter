using System;
using UnityEngine;
using Magic.Spells.Aoe;
using Magic.Spells.Projectiles;
using Object = UnityEngine.Object;

namespace Magic.Systems
{
    public sealed class SpellCaster
    {
        private readonly Transform m_casterTransform;

        public SpellCaster(Transform casterTransform)
        {
            m_casterTransform = casterTransform;
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
                    if (aoeSpell.isTarget)
                        CastAoe(aoeSpell, worldPosition);
                    else
                        CastAoe(aoeSpell, m_casterTransform.position);
                    break;
            }
        }

        private void CastSelf(SelfSpellData selfSpell)
        {
            if (selfSpell.visualEffect)
            {
                Object.Instantiate(selfSpell.visualEffect, m_casterTransform.position, Quaternion.identity);
            }

            if (m_casterTransform.TryGetComponent<IEffectable>(out var effectable))
            {
                foreach (var effect in selfSpell.effects)
                {
                    effect?.Apply(effectable);
                }
            }
        }

        private void CastTarget(TargetSpellData targetSpell, Vector3 worldPosition)
        {
            if (!targetSpell.visualEffect)
            {
                Debug.LogError("Target spell must have visualEffect");
                return;
            }

            var projectileObj = Object.Instantiate(targetSpell.visualEffect, m_casterTransform.position, Quaternion.identity);

            var spellProjectile = projectileObj.GetComponent<ISpellProjectile>() ?? projectileObj.AddComponent<FallbackSpellProjectile>();

            spellProjectile.Initialize(worldPosition, targetSpell.speed, targetSpell.effects);
        }

        private void CastNonTarget(NonTargetSpellData nonTargetSpell) { }

        private void CastAoe(AoeSpellData aoeSpell, Vector3 worldPosition)
        {
            GameObject aoeObj;

            if (aoeSpell.visualEffect)
            {
                aoeObj = Object.Instantiate(aoeSpell.visualEffect, m_casterTransform.position, Quaternion.identity);
            }
            else
            {
                aoeObj = new GameObject("AoeEffect");
            }

            aoeObj.transform.position = worldPosition;

            var spellAoe = aoeObj.GetComponent<ISpellAoe>() ?? aoeObj.AddComponent<SpellAoe>();

            spellAoe.Initialize(worldPosition, aoeSpell.radius, aoeSpell.effects);
        }
    }
}