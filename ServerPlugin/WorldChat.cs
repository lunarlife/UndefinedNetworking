using UndefinedNetworking.Chats;
using UndefinedNetworking.Gameplay.Chat;
using UndefinedServer;

namespace ServerPlugin
{
    internal class WorldChat : ChatType
    {
        public override string DisplayName => "World";

        public override bool CanUseCommands => true;

        public override bool CanWriteMessages => true;

        public override void Execute(ISender sender, string message)
        {
            foreach(ServerPlayer pl in UServer.CurrentGame.Players)
                pl.SendMessage(new ChatMessage(sender, message,this));
        }
    }
    internal class WhisperChat : ChatType
    {
        public override string DisplayName => "Whisper";

        public override bool CanUseCommands => true;

        public override bool CanWriteMessages => false;

        public override void Execute(ISender sender, string message)
        {
            
        }
    }
}
