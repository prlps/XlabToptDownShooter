using UnityEngine;
using UnityEngine.UI;

namespace Enteties
{
    public class HealthVarView : MonoBehaviour
    {
        [SerializeField] private HealthComponent m_healthComponent;
        [SerializeField] private Image m_bar;

        private void OnEnable()
        {
            if (m_healthComponent != null)
            {
                m_healthComponent.valueChanged += SetValue;
            }

            SetValue();
        }

        private void OnDisable()
        {
            if (m_healthComponent != null)
            {
                m_healthComponent.valueChanged -= SetValue;
            }
        }

        private void SetValue()
        {
            if (m_healthComponent == null || m_bar == null || m_healthComponent.maxValue <= 0f)
            {
                return;
            }

            m_bar.fillAmount = m_healthComponent.value / m_healthComponent.maxValue;
        }
    }
}
