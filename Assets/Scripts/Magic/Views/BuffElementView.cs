using Magic.Buffs;
using UnityEngine;
using UnityEngine.UI;

namespace Magic.Views
{
    public class BuffElementView : MonoBehaviour
    {
        [SerializeField] private Image m_iconImage;
        [SerializeField] private Image m_timerImage;

        private IBuff m_buff;

        public void Initialize(IBuff buff)
        {
            m_buff = buff;
            gameObject.SetActive(true);

            if (m_iconImage != null)
            {
                m_iconImage.sprite = buff.Icon;
            }

            if (m_timerImage != null)
            {
                m_timerImage.fillAmount = 1f;
            }
        }

        public void Deinitialize()
        {
            m_buff = null;
            gameObject.SetActive(false);

            if (m_timerImage != null)
            {
                m_timerImage.fillAmount = 0f;
            }
        }

        private void Update()
        {
            if (m_buff is ITimeBuff timeBuff && m_timerImage != null && timeBuff.Duration > 0f)
            {
                m_timerImage.fillAmount = timeBuff.Timer / timeBuff.Duration;
            }
        }
    }
}
