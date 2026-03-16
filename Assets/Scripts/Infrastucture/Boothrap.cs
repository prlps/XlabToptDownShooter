using Infrastucture;
using UnityEngine;

[DefaultExecutionOrder(-500)]
public class Boothrap : MonoBehaviour
{
    [SerializeField] private Loading m_loading;

    private void Awake()
    {
        if (m_loading == null)
        {
            var loadings = Resources.FindObjectsOfTypeAll<Loading>();
            if (loadings.Length > 0)
            {
                m_loading = loadings[0];
            }
        }

        ServiceLocator.Clear();
        ServiceLocator.Register(m_loading);
    }
}
