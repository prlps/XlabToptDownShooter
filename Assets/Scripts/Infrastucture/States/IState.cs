namespace Infrastucture.States
{
    public interface IState
    {
        void Enter() { }
        void Update() { }
        void Exit() { }
    }
}
