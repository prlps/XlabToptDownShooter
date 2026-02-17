using Infrastucture.States;
using UI;
using UnityEngine;

namespace Infrastucture
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private MainMenuView m_view;
        [SerializeField] private SpawnerEnemy m_enemySpawner;

        private StateMachine m_stateMachine;

        private void Awake()
        {
            m_stateMachine = new StateMachine();
            m_stateMachine.Initialize(
                new MainMenuState(m_view),
                new PauseMenuState(),
                new DeadState(),
                new GameplayState(m_enemySpawner));

            if (m_view != null)
            {
                m_view.PlayClicked += OnPlayClicked;
                m_view.ExitClicked += OnExitClicked;
            }

            m_stateMachine.ChangeState<MainMenuState>();
        }

        private void OnDestroy()
        {
            if (m_view != null)
            {
                m_view.PlayClicked -= OnPlayClicked;
                m_view.ExitClicked -= OnExitClicked;
            }
        }

        private void OnPlayClicked()
        {
            m_stateMachine.ChangeState<GameplayState>();
        }

        private static void OnExitClicked()
        {
            Application.Quit();
        }
    }
}
