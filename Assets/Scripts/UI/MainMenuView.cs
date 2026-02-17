using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action PlayClicked;
        public event Action ExitClicked;

        [SerializeField] private Button m_playButton;
        [SerializeField] private Button m_exitButton;

        private void OnEnable()
        {
            if (m_playButton != null)
            {
                m_playButton.onClick.AddListener(OnPlayClick);
            }

            if (m_exitButton != null)
            {
                m_exitButton.onClick.AddListener(OnExitClick);
            }
        }

        private void OnDisable()
        {
            if (m_playButton != null)
            {
                m_playButton.onClick.RemoveListener(OnPlayClick);
            }

            if (m_exitButton != null)
            {
                m_exitButton.onClick.RemoveListener(OnExitClick);
            }
        }

        private void OnPlayClick() =>
            PlayClicked?.Invoke();

        private void OnExitClick() =>
            ExitClicked?.Invoke();
    }
}
