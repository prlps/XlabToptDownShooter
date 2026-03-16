using UI;
using UnityEngine;

namespace Infrastucture.States
{
    public class MainMenuState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly MainMenuView m_view;

        public MainMenuState(StateMachine stateMachine, MainMenuView view)
        {
            m_stateMachine = stateMachine;
            m_view = view;

            if (m_view != null)
            {
                m_view.gameObject.SetActive(false);
            }
        }

        public void Enter()
        {
            if (m_view == null)
            {
                return;
            }

            m_view.gameObject.SetActive(true);
            m_view.PlayClicked += OnPlayClicked;
            m_view.ExitClicked += OnExitClicked;
        }

        public void Exit()
        {
            if (m_view == null)
            {
                return;
            }

            m_view.PlayClicked -= OnPlayClicked;
            m_view.ExitClicked -= OnExitClicked;
            m_view.gameObject.SetActive(false);
        }

        private void OnPlayClicked()
        {
            m_stateMachine.ChangeState<GameplayEntryState>();
        }

        private void OnExitClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }
    }
}
