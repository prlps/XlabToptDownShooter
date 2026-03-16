using UI;

namespace Infrastucture.States
{
    public class DeadState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly DeadMenuView m_deadMenuView;

        public DeadState(StateMachine stateMachine, DeadMenuView deadMenuView)
        {
            m_stateMachine = stateMachine;
            m_deadMenuView = deadMenuView;

            if (m_deadMenuView != null)
            {
                m_deadMenuView.gameObject.SetActive(false);
            }
        }

        public void Enter()
        {
            if (m_deadMenuView == null)
            {
                m_stateMachine.ChangeState<GameplayerExitState>();
                return;
            }

            m_deadMenuView.GoToMenuClicked += OnGoToMenuClicked;
            m_deadMenuView.gameObject.SetActive(true);
        }

        public void Exit()
        {
            if (m_deadMenuView == null)
            {
                return;
            }

            m_deadMenuView.GoToMenuClicked -= OnGoToMenuClicked;
            m_deadMenuView.gameObject.SetActive(false);
        }

        private void OnGoToMenuClicked()
        {
            m_stateMachine.ChangeState<GameplayerExitState>();
        }
    }
}
