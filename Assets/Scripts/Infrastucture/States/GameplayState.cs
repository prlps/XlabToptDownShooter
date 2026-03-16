using Infrastucture;
using Players;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastucture.States
{
    public class GameplayState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly CameraFollow m_cameraFollow;

        private PlayerController m_playerController;

        public GameplayState(StateMachine stateMachine, CameraFollow cameraFollow)
        {
            m_stateMachine = stateMachine;
            m_cameraFollow = cameraFollow;
        }

        public void Enter()
        {
            m_playerController = ServiceLocator.Resolved<PlayerController>();

            if (m_playerController != null)
            {
                if (m_cameraFollow != null)
                {
                    m_cameraFollow.SetTarget(m_playerController.transform);
                }

                m_playerController.health.died += OnDied;
            }
        }

        public void Update()
        {
            var keyboard = Keyboard.current;
            if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame)
            {
                m_stateMachine.ChangeState<PauseMenuState>();
            }
        }

        public void Exit()
        {
            if (m_playerController != null)
            {
                m_playerController.health.died -= OnDied;
                m_playerController = null;
            }
        }

        private void OnDied()
        {
            m_stateMachine.ChangeState<DeadState>();
        }
    }
}
