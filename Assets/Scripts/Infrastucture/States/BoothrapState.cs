using Infrastucture;
using Players;
using UnityEngine;

namespace Infrastucture.States
{
    public class BoothrapState : MonoBehaviour, IState
    {
        [SerializeField] private PlayerSpawnPoint m_spawnPoint;
        [SerializeField] private NavMeshMouseResolver m_mouseResolver;

        private StateMachine m_stateMachine;

        public void Initialize(StateMachine stateMachine)
        {
            m_stateMachine = stateMachine;
        }

        public void Enter()
        {
            var playerFactory = new PlayerFactory("Prefabs/Player");

            ServiceLocator.Register(m_spawnPoint);
            ServiceLocator.Register<IPlayerFactory>(playerFactory);
            ServiceLocator.Register<IPlayerFactorySettings>(playerFactory);
            ServiceLocator.Register(m_mouseResolver);

            m_stateMachine.ChangeState<GameplayEntryState>();
        }

        public void Exit()
        {
        }
    }
}
