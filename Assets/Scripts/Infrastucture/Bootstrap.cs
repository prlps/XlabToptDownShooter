using Infrastucture.States;
using Markers;
using UI;
using UnityEngine;

namespace Infrastucture
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private BoothrapState m_boothrapState;
        [SerializeField] private SpawnerEnemy m_enemySpawner;
        [SerializeField] private DeadMenuView m_deadMenuView;
        [SerializeField] private TargetMarkerObserver m_targetMarkerObserver;
        [SerializeField] private AIMLineMarker m_aimLineMarker;
        [SerializeField] private CameraFollow m_cameraFollow;
        [SerializeField] private PauseMenuView m_pauseMenuView;

        private readonly StateMachine m_stateMachine = new();

        private void Awake()
        {
            m_boothrapState.Initialize(m_stateMachine);
            ServiceLocator.Register(m_enemySpawner);

            m_stateMachine.Initialize(
                m_boothrapState,
                new PauseMenuState(m_stateMachine, m_pauseMenuView),
                new DeadState(m_stateMachine, m_deadMenuView),
                new GameplayerExitState(),
                new GameplayEntryState(
                    m_stateMachine,
                    m_enemySpawner,
                    m_targetMarkerObserver,
                    m_aimLineMarker,
                    m_cameraFollow),
                new GameplayState(m_stateMachine, m_cameraFollow));

            m_stateMachine.ChangeState<BoothrapState>();
        }

        private void Update()
        {
            m_stateMachine.Update();
        }
    }
}
