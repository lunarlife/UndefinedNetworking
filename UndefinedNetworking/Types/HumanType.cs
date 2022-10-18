using Utils.Enums;

namespace UndefinedNetworking.Types
{
    public abstract class HumanType : EnumType
    {
        public abstract string HumanName { get; }
        public abstract string MainSpritePath { get; }
    }
}