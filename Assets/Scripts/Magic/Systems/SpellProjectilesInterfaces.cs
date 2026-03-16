using UnityEngine;
using System.Collections.Generic;

namespace Magic.Spells.Projectiles
{
 public interface ISpellProjectile
 {
 void Initialize(Vector3 targetPosition, float speed, IReadOnlyList<IEffect> effects);
 }
}

namespace Magic.Spells.Aoe
{
 public interface ISpellAoe
 {
 void Initialize(Vector3 worldPosition, float radius, IReadOnlyCollection<IEffect> effects);
 }
}
