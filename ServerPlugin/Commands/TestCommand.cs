using UndefinedNetworking.Chats;
using UndefinedNetworking.Commands;
using UndefinedNetworking.Gameplay.Chat;
using UndefinedServer;
using UndefinedServer.Chats;
using UndefinedServer.Commands;

namespace ServerPlugin.Commands
{
    public class TestCommand : Command
    {
        public override string Prefix => "send";

        public override ParameterType[]? Parameters => null;

        public override string Description => "Send АХААХХА";

        public override void Execute(ServerPlayer sender, CommandParameter[]? args, ChatType type)
        {
            for(int i =0; i < 5; i++)
            {
                sender.SendMessage(new ChatMessage(ServerSender.Instance,"АХААХХА"+i, type));
            }

        }
        public override void OnFailArgumentsLength(ISender sender, ChatType type)
        {
            Main.print("SUKA BLYAT in ["+type.DisplayName+"]");
        }
    }
    public class MsgCommand : Command
    {
        public override string Prefix => "msg";

        public override ParameterType[]? Parameters { get; } = { new PlayerParameter(true), new StringParameter(true) };

        public override string Description => "Send private message";
        public ChatType? ToChat;

        public override void Execute(ServerPlayer sender, CommandParameter[]? args, ChatType type)
        {
            if(args == null) return;
            if(args.Length < 2) return;
            string nickname = args[0];
            if(UServer.CurrentGame.TryGetPlayer(nickname, out var pl))
            {
                string msg = "";
                for(int i = 1; i < args.Length; i++) msg += args[i] + " ";
                msg = msg.Trim();
                sender.SendMessage(new ChatMessage("you to " + nickname, msg, ToChat is not null ? ToChat : type));
                pl!.SendMessage(new ChatMessage(sender, msg, ToChat is not null ? ToChat : type));
            }
        }
    }
}
