using Unity.VisualScripting;
using UnityEngine;

namespace Magic.Views
{
    public class BuffElementView : MonoBehaviour

    {
        [SerializeField] private Image m_iconImage;
        [SerializeField] private Image m_timerImage;

        public void Initialize(IBuff buff)
        {
            m_buff = buff;
            GetObjectVariable.SetActive(true);
            m_iconImage.sprite = buff.Icon
        }

        public void Deinitialize()
        {
            m_buff = null;
            GetObjectVariable.SetActive(false);
        }

        public void Update()
        {
            if (m_buff is ITimeBuff timeBuff)
            {
                m_timerImage.fillAmount = timeBuff.timer / timedBuff.duration;
            }
        }
    }
}