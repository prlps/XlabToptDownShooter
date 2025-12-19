using UnityEngine;
using System.Collections.Generic;

namespace Magic.Spells.Projectiles
{
 public sealed class FallbackSpellProjectile : MonoBehaviour, ISpellProjectile
 {
 private Vector3 m_direction;
 private float m_speed;
 private float m_targetDistance;
 private float m_traveled;
 private bool m_initialized;
 private IReadOnlyList<IEffect> m_effects;

 public void Initialize(Vector3 targetPosition, float speed, IReadOnlyList<IEffect> effects)
 {
 var target = targetPosition;
 target.y = transform.position.y;
 m_speed = speed;
 m_effects = effects;
 m_direction = (target - transform.position).normalized;
 m_traveled =0f;
 m_targetDistance = Vector3.Distance(transform.position, target);
 if (m_direction != Vector3.zero)
 transform.rotation = Quaternion.LookRotation(m_direction);
 m_initialized = true;
 }

 private void FixedUpdate()
 {
 if (!m_initialized) return;
 var delta = m_speed * Time.fixedDeltaTime;
 transform.position += m_direction * delta;
 m_traveled += delta;
 if (m_traveled >= m_targetDistance)
 Destroy(gameObject);
 }

 private void OnTriggerEnter(Collider other)
 {
 if (!m_initialized) return;
 if (other.TryGetComponent<IEffectable>(out var effectable) && m_effects != null)
 {
 foreach (var effect in m_effects)
 effect?.Apply(effectable);
 }
 Destroy(gameObject);
 }
 }
}
