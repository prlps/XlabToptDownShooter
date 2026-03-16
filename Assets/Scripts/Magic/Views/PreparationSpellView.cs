using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

namespace Magic.Views
{
 public sealed class PreparationSpellView : MonoBehaviour
 {
 [Header("References")]
 [SerializeField] private Magic.Systems.MagicSystem m_magicSystem;
 [SerializeField] private MagicConfig m_config;

 [SerializeField] private RectTransform m_elementsContainer;
 [SerializeField] private Image[] m_icons;

 [Header("Animation")]
 [SerializeField] private float m_shakeIntensity =20f;
 [SerializeField] private float m_shakeDuration =1f;

 private Tween m_shakeTween;

 private void OnEnable()
 {
 if (m_magicSystem == null) return;

 m_magicSystem.ElementChanged += UpdateIcons;
 m_magicSystem.SpellCancelled += ShakeContainer;
 }

 private void OnDisable()
 {
 if (m_magicSystem == null) return;

 m_magicSystem.ElementChanged -= UpdateIcons;
 m_magicSystem.SpellCancelled -= ShakeContainer;
 }

 private void UpdateIcons(IReadOnlyList<ElementType> elements)
 {
 foreach (var icon in m_icons)
 {
 icon.sprite = null;
 icon.enabled = false;
 }

 if (elements == null || elements.Count ==0) return;

 for (var i =0; i < elements.Count && i < m_icons.Length; i++)
 {
 var elementInfo = GetElementInfo(elements[i]);
 if (elementInfo == null) continue;

 m_icons[i].enabled = true;
 m_icons[i].sprite = elementInfo.icon;
 }
 }

 private void ShakeContainer()
 {
 m_shakeTween?.Kill();
 var localRotation = m_elementsContainer.localRotation;

 m_shakeTween = m_elementsContainer
 .DOShakeRotation(m_shakeDuration, m_shakeIntensity)
 .SetEase(Ease.OutQuad)
 .OnComplete(() => m_elementsContainer.localRotation = localRotation);
 }

 private ElementData.Item GetElementInfo(ElementType type)
 {
 return m_config == null || m_config.ElementData == null
 ? null
 : m_config.ElementData.Items.FirstOrDefault(i => i.type == type);
 }
 }
}
