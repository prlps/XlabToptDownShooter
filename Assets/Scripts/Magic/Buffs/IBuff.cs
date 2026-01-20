namespace Magic.Buffs
{
    public class IBuff
    {
        public  string Id { get; }

        public void Initialiaze(BuffContainer container);

        public void Deinitialize();

        public void Update(float deltaTime);
    }
}