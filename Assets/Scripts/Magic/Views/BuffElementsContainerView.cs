using System.Collections.Generic;
using Magic.Buffs;
using Magic.Views;
using UnityEngine;

public class BuffElementsContainerView : MonoBehaviour
{
    [SerializeField] private BuffElementView m_buffView;
    [SerializeField] private BuffElementView m_deBuffView;
    [SerializeField] private BuffContainer m_buffContainer;

    private readonly Dictionary<string, BuffElementView> m_elements = new();

    private void OnEnable()
    {
        if (m_buffContainer == null)
        {
            return;
        }

        foreach (var buff in m_buffContainer.Buffs)
        {
            AddElement(buff);
        }

        m_buffContainer.BuffAdded += AddElement;
        m_buffContainer.BuffRemoved += RemoveElement;
    }

    private void OnDisable()
    {
        if (m_buffContainer == null)
        {
            return;
        }

        m_buffContainer.BuffAdded -= AddElement;
        m_buffContainer.BuffRemoved -= RemoveElement;

        foreach (var buff in m_buffContainer.Buffs)
        {
            RemoveElement(buff);
        }
    }

    private void AddElement(IBuff buff)
    {
        if (buff == null || m_elements.ContainsKey(buff.Id))
        {
            return;
        }

        var prefab = buff.Type == BuffType.Buff ? m_buffView : m_deBuffView;
        if (prefab == null)
        {
            return;
        }

        var element = Instantiate(prefab, transform);
        element.Initialize(buff);

        m_elements.Add(buff.Id, element);
    }

    private void RemoveElement(IBuff buff)
    {
        if (buff == null)
        {
            return;
        }

        if (!m_elements.TryGetValue(buff.Id, out var element))
        {
            return;
        }

        element.Deinitialize();
        Destroy(element.gameObject);
        m_elements.Remove(buff.Id);
    }
}
