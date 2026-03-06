using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class PauseMenuView
    {
        [SerializeField] private Button m_continue;
        [SerializeField] private Button m_mainMenu;

        private void OnEnable()
        {
            m_continue.onClick.AddListener(OnContinueClick);
            m_mainMenu.onClick
        }
    }
}