namespace Enteties.Enemies
{
    public class EnemyStateMachine
    {
        public EnemyState currentState {get; private set; };
    
        public event Action<EnemyState, EnemyState> StateChanged;
        
    private EnemyStateMaschine()
    {
        currentState = EnemyState.Idle;
    }
    
    public void ChangeState(EnemyState nextState)
    {
        if (currentState is EnemyState.Dead || currentState == nextState)
        {
            return;
        }

        var previousState = currentState;
        currentState = nextState;

        StateChanged?.Invoke(previousState, currentState);
    }
}