using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DeadMenuView : MonoBehaviour
    {
        public event Action GoToMenuClicked;

        [SerializeField] private Button m_goToMainMenuButton;

        private void OnEnable()
        {
            if (m_goToMainMenuButton != null)
            {
                m_goToMainMenuButton.onClick.AddListener(OnClicked);
            }
        }

        private void OnDisable()
        {
            if (m_goToMainMenuButton != null)
            {
                m_goToMainMenuButton.onClick.RemoveListener(OnClicked);
            }
        }

        private void OnClicked() =>
            GoToMenuClicked?.Invoke();
    }
}
