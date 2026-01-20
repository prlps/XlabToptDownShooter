namespace Magic.Buffs.Extensions
{
    public class BuffsExtensions
    {
        public static void Refresh(this IBuff buff)
        {
            buff.Deinitialize();
            buff.Initialiaze();
        }
    }
}