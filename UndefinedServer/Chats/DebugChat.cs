using UndefinedNetworking.Chats;
using UndefinedNetworking.Gameplay.Chat;

namespace UndefinedServer.Chats
{
    public class DebugChat : ChatType
    {
        public override string DisplayName => "Debug";
        public override bool CanUseCommands => false;
        public override bool CanWriteMessages => false;
        public bool IsEnabled { get; }
        
        public override void Execute(ISender sender, string message)
        {
            
        }
    }
}