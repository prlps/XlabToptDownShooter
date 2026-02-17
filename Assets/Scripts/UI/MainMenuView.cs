using UnityEngine;

namespace UI
{
    public class MainMenuView
    {
        public event Action PlayClicked;
        public event Action ExitClicked;
        
        [SerializeField] private Button m_playButton;
        [SerializeField] private Button m_exitButton;

        private void OnEnable()
        {
            m_playButton.onClick.AddListener(OnPlayClick);
            m_exitButton.onClick.AddListener(OnExitClick);
        }

        private void OnDisable()
        {
            
        }
    }
}