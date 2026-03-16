using Infrastucture;
using Markers;
using Players;

namespace Infrastucture.States
{
    public class GameplayEntryState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly SpawnerEnemy m_spawnerEnemy;
        private readonly TargetMarkerObserver m_targetMarkerObserver;
        private readonly AIMLineMarker m_aimLineMarker;
        private readonly CameraFollow m_cameraFollow;

        private PlayerController m_playerController;

        public GameplayEntryState(
            StateMachine stateMachine,
            SpawnerEnemy spawnerEnemy,
            TargetMarkerObserver targetMarkerObserver,
            AIMLineMarker aimLineMarker,
            CameraFollow cameraFollow)
        {
            m_stateMachine = stateMachine;
            m_spawnerEnemy = spawnerEnemy;
            m_targetMarkerObserver = targetMarkerObserver;
            m_aimLineMarker = aimLineMarker;
            m_cameraFollow = cameraFollow;
        }

        public void Enter()
        {
            var playerSpawnPoint = ServiceLocator.Resolved<PlayerSpawnPoint>();
            ServiceLocator.Resolved<IPlayerFactorySettings>().position = playerSpawnPoint.transform.position;

            m_playerController = ServiceLocator.Resolved<IPlayerFactory>().Create();
            ServiceLocator.Register(m_playerController);

            if (m_targetMarkerObserver != null)
            {
                m_targetMarkerObserver.Initialize(m_playerController.GetComponent<PlayerMovement>());
            }

            if (m_aimLineMarker != null)
            {
                m_aimLineMarker.Initialize(m_playerController.transform);
            }

            if (m_cameraFollow != null)
            {
                m_cameraFollow.SetTarget(m_playerController.transform);
            }

            m_spawnerEnemy.Spawn();
            m_stateMachine.ChangeState<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}
