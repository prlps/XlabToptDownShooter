using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState m_state;
    private Dictionary<Type, IState> m_states = new();

    public void Initialize(params IState[] states)
    {
        if (m_state.Count > 0) return;
    }
    
    public void ChangedState<T>()
    {
        where T : State
        {
            m_state?.Exit();
            {
                m_state = m_states[typeof(T)];
            }
            m_state.Enter;

        }
    }
        public interface IState
        {
            public void State();
            
            public void Exit();
        }
    
        public class MainMenuState : IState
        {
            private readonly StateMachine m_stateMachine;
            
            public MainMenuState(StateMachine m_stateMachine)
                
                
            public void Enter() =>  throw new NotImplementedException();
            
            public void Exit() =>  throw new NotImplementedException();

        }
        
        
}
