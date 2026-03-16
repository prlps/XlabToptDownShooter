using System;
using System.Collections.Generic;

namespace Infrastucture.States
{
    public class StateMachine
    {
        private IState m_state;
        private readonly Dictionary<Type, IState> m_states = new();

        public void Initialize(params IState[] states)
        {
            if (m_states.Count > 0)
            {
                return;
            }

            foreach (var state in states)
            {
                if (state == null)
                {
                    continue;
                }

                m_states.Add(state.GetType(), state);
            }
        }

        public void ChangeState<T>() where T : class, IState
        {
            if (!m_states.TryGetValue(typeof(T), out var nextState))
            {
                return;
            }

            m_state?.Exit();
            m_state = nextState;
            m_state.Enter();
        }

        public void Update()
        {
            m_state?.Update();
        }
    }
}
