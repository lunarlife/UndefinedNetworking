using System.Collections.Generic;
using System.Linq;
using Networking;
using Networking.Packets;
using UndefinedNetworking.Commands;
using UndefinedNetworking.Gameplay.Chat;

namespace UndefinedNetworking.Packets.Server
{
    public sealed class ServerInfoPacket : Packet
    {
        public int Tick { get; private set; }
        public Identifier Identifier { get; private set; }
        public ChatTypePacket[] Chats { get;  private set; }
        public CommandTypePacket[] Commands { get;  private set; }
        
        public ServerInfoPacket(int tick, Identifier identifier, IReadOnlyList<ChatType> chats, IReadOnlyList<ICommand> commands)
        {
            Tick = tick;
            Identifier = identifier;
            Chats = new ChatTypePacket[chats.Count];
            Commands = new CommandTypePacket[commands.Count];
            for (var i = 0; i < chats.Count; i++)
            {
                var chat = chats[i];
                Chats[i] = new ChatTypePacket
                {
                    Name = chat.Name,
                    DisplayName = chat.DisplayName,
                    CanUseCommands = chat.CanUseCommands,
                    CanWriteMessages = chat.CanWriteMessages
                };
            }
            for (var i = 0; i < commands.Count; i++)
            {
                var command = commands[i];
                Commands[i] = new CommandTypePacket
                {
                    Prefix = command.Prefix,
                    Description = command.Description,
                    ParametersTitles = command.Parameters?.Select(type => type.Title).ToArray()
                };
            }
        }

        private ServerInfoPacket()
        {
            
        }
        
        public class ChatTypePacket
        {
            public string Name;
            public string DisplayName;
            public bool CanUseCommands;
            public bool CanWriteMessages;
        }
        public class CommandTypePacket
        {
            public string Prefix;
            public string Description;
            public string[] ParametersTitles;
        }
    }
}