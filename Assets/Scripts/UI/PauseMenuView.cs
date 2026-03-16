using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class PauseMenuView : MonoBehaviour
    {
        public event Action ContinueClicked;
        public event Action MainMenuClicked;

        [SerializeField] private Button m_continue;
        [SerializeField] private Button m_mainMenu;
        [SerializeField] private Button m_settings;

        private void OnEnable()
        {
            if (m_continue != null)
            {
                m_continue.onClick.AddListener(OnContinueClick);
            }

            if (m_mainMenu != null)
            {
                m_mainMenu.onClick.AddListener(OnMainMenuClick);
            }
        }

        private void OnDisable()
        {
            if (m_continue != null)
            {
                m_continue.onClick.RemoveListener(OnContinueClick);
            }

            if (m_mainMenu != null)
            {
                m_mainMenu.onClick.RemoveListener(OnMainMenuClick);
            }
        }

        private void OnContinueClick() =>
            ContinueClicked?.Invoke();

        private void OnMainMenuClick() =>
            MainMenuClicked?.Invoke();
    }
}
