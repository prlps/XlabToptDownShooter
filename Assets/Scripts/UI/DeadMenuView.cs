using UnityEngine;

namespace UI
{
    public class DeadMenuView : MonoBehaviour

    {
        public event Action GoToMenuClicked;
        
        [SerializeField] private Button m_goToMainMenuButton;

        private void OnEnable()
        {
            m_goToMainMenuButton.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            m_goToMainMenuButton.onClick.RemoveListener(OnClicked)
        }
        

    }
}