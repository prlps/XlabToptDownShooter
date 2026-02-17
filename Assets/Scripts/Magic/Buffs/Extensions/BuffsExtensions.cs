namespace Magic.Buffs
{
    public static class BuffsExtensions
    {
        public static void Refresh(this IBuff buff, BuffContainer container)
        {
            if (buff == null || container == null)
            {
                return;
            }

            buff.Deinitialize();
            buff.Initialize(container);
        }
    }
}
