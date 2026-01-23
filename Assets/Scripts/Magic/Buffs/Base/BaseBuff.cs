namespace Magic.Buffs.Base
{
    public class BaseBuff : IBuff
    {
        
        protected BuffContainer container { get; private set; }
         
        public string Id { get; private set; }
        
        public BaseBuff(){}

        protected BaseBuff(string id)
        {
            Id = id;
        }
        
        public void Initialiaze(BuffContainer container);
        {
            this.container = container;
            OnInitialized();
        }

        protected virtual void OnInitialized()
        
        public void Deinitialise()
        {
            OnDeinitializing();
            
            container.Remove(this);
            container = null;
        }
        
        protected virtual void OnDeinitializing(){}
        
        public virtual void Update(float deltaTime) {}
        
        
    }
}