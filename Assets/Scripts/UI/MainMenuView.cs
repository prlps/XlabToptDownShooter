using System;
using Infrastucture;
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
        [SerializeField] private Button m_settingsButton;
        [SerializeField] private GameObject m_settingsView;

        private Loading m_loading;

        private void OnEnable()
        {
            ResolveViewReferences();

            if (m_playButton != null)
            {
                m_playButton.onClick.AddListener(OnPlayClick);
            }

            if (m_exitButton != null)
            {
                m_exitButton.onClick.AddListener(OnExitClick);
            }

            if (m_settingsButton != null)
            {
                m_settingsButton.onClick.AddListener(OnSettingsClick);
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

            if (m_settingsButton != null)
            {
                m_settingsButton.onClick.RemoveListener(OnSettingsClick);
            }
        }

        private void Start()
        {
            m_loading = ResolveLoading();
            ResolveViewReferences();
            SetSettingsVisible(false);
        }

        private void OnPlayClick()
        {
            m_loading ??= ResolveLoading();
            if (m_loading == null)
            {
                return;
            }

            SetSettingsVisible(false);
            PlayClicked?.Invoke();
            m_loading.LoadScene(GlobalConstants.Scenes.Game);
        }

        private void OnExitClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
            ExitClicked?.Invoke();
            Application.Quit();
        }

        private void OnSettingsClick()
        {
            ResolveViewReferences();
            SetSettingsVisible(m_settingsView == null || !m_settingsView.activeSelf);
        }

        private Loading ResolveLoading()
        {
            try
            {
                return ServiceLocator.Resolved<Loading>();
            }
            catch
            {
                var loadings = Resources.FindObjectsOfTypeAll<Loading>();
                return loadings.Length > 0 ? loadings[0] : null;
            }
        }

        private void ResolveViewReferences()
        {
            if (m_playButton == null || m_exitButton == null || m_settingsButton == null)
            {
                foreach (var button in GetComponentsInChildren<Button>(true))
                {
                    var name = button.name.Trim();
                    if (m_playButton == null && name.IndexOf("Play", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        m_playButton = button;
                    }
                    else if (m_exitButton == null && name.IndexOf("Exit", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        m_exitButton = button;
                    }
                    else if (m_settingsButton == null && name.IndexOf("Settings", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        m_settingsButton = button;
                    }
                }
            }

            if (m_settingsView == null)
            {
                var settingsTransform = transform.Find("Settings");
                if (settingsTransform != null)
                {
                    m_settingsView = settingsTransform.gameObject;
                }
            }
        }

        private void SetSettingsVisible(bool isVisible)
        {
            if (m_settingsView != null)
            {
                m_settingsView.SetActive(isVisible);
            }
        }
    }
}
