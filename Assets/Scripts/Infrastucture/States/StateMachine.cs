using System;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Infrastucture.States
{
    public sealed class StateMachine
    {
        private IState m_state;
        private readonly Dictionary<Type, IState> m_states = new();

        public void Initialize(params IState[] states)
        {
            m_states.Clear();

            foreach (var state in states)
            {
                if (state == null)
                {
                    continue;
                }

                m_states[state.GetType()] = state;
            }
        }

        public void ChangeState<T>() where T : class, IState
        {
            if (!m_states.TryGetValue(typeof(T), out var nextState))
            {
                return;
            }

            if (ReferenceEquals(m_state, nextState))
            {
                return;
            }

            m_state?.Exit();
            m_state = nextState;
            m_state.Enter();
        }

        public interface IState
        {
            void Enter();
            void Exit();
        }
    }

    public sealed class MainMenuState : StateMachine.IState
    {
        private readonly MainMenuView m_view;

        public MainMenuState(MainMenuView view)
        {
            m_view = view;
        }

        public void Enter()
        {
            if (m_view != null)
            {
                m_view.gameObject.SetActive(true);
            }
        }

        public void Exit()
        {
            if (m_view != null)
            {
                m_view.gameObject.SetActive(false);
            }
        }
    }

    public sealed class GameplayState : StateMachine.IState
    {
        private readonly SpawnerEnemy m_enemySpawner;

        public GameplayState(SpawnerEnemy enemySpawner)
        {
            m_enemySpawner = enemySpawner;
        }

        public void Enter()
        {
            if (m_enemySpawner != null)
            {
                m_enemySpawner.Spawn();
            }
        }

        public void Exit()
        {
        }
    }

    public sealed class PauseMenuState : StateMachine.IState
    {
        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }

    public sealed class DeadState : StateMachine.IState
    {
        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }

    public class BootstrapState : IState
    {
        public void Enter()
        {
            ServiceLocator.Register();
        }
    }
}
