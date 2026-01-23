using System;
using Magic.Buffs;
using UnityEngine;

namespace Magic.Views
{
    public class BuffElementsContainerView : MonoBehaviour
    {
        [SerializeField] private BuffElementView m_debuffView;
        [SerializeField] private BuffElementView m_buffView;
        [SerializeField] private BuffContainer m_buffContainer;

        private void OnEnable()
        {
            foreach (var buff in m_buffContainer.Buffs)
            {
                CreateElement(buff);    
            }
            
            m_buffContainer.BuffAdded += CreateElement()
        }

        private void OnDisable()
        {
            foreach (var buff in m_buffContainer.Buffs)
            {
                CreateElement(buff);    
            }
            
            m_buffContainer.BuffAdded -= CreateElement()
        }

        private void AddElement(IBuff buff)
        {
            var element =  Instantiate(m_buffView, transform);
            element.Initialize(buff);
        }

        private void RemoveElement(IBuff buff)
        {
            var element = m_elements[buff];
            Destroy(element);
            
            m_elements.Remove(buff);
        }
    }
}