using System.Linq.Expressions;
using System.Numerics;
using System.Threading.Tasks.Dataflow;
using UnityEngine;

namespace Magic.System
{
    public class SpellCaster
    {
        public void Cast()
        {
            public SpellCaster (TransformBlock casterTransform)
        {
            if (!spell)
            {
                return;
            }

            swtirch (spell)
            {
                case SelfSpellData selfSpell: CastSelf(selfSpell); break;
                case TargetSpellData targetSpell: CastTarget (targetSpell); break;
                case NonTargetSpellData: CastNonTareget(nonTargetSpell); break;
                case AoeSpellData: aoeSpell:
                    {
                        if (aoeSpell.isTarget, aoeSpell.isTarget
                        ? worldPosition
                        : m_casterTransform.position);
                        break;
                    }
            }
        }

            public void Cast(BaseSpellData spell, Vector3 worldPosition) {}

            private void CastSelf(){}

            private void CastTarget (){}

            private void CastNonTarget(){}

            private void CastAoe(){}
        }
    }
}