namespace Magic.Buffs
{
    public class IBuff
    {
        public string Id { get; }
        
        public float duration { get; }
        
        public Sprite Icon { get; }
        
        public float timer { get; }
        public  string Id { get; }

        public void Initialiaze(BuffContainer container);

        public void Deinitialize();

        public void Update(float deltaTime);

        public IBuff Clone();
        
    }
}