using UI;
using UnityEngine;

namespace Infrastucture
{
    public class Bootstrap
    {
        [SerializeField] private MainMenuView m_view;
        [SerializeField] private SpawnerEnemy m_enemySpawner;
        
        private void Awake()
        {
            var stateMachine = new StateMachine();
            
            
            stateMachine.Initialize(
                new MainMenuState(stateMachine),
                new PauseMenuState(stateMachine),
                new DeadState(stateMachine),
                new GameplayState(stateMachine, m_enemySpawner));
                
        } 
    }
}