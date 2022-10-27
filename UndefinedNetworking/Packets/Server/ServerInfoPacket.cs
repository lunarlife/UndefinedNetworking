using System.Collections.Generic;
using System.Linq;
using Networking;
using Networking.DataConvert;
using Networking.Packets;
using UndefinedNetworking.Commands;
using UndefinedNetworking.Gameplay.Chat;

namespace UndefinedNetworking.Packets.Server
{
    [DataObject]
    public sealed class ServerInfoPacket : Packet
    {
        [DataProperty]
        public int Tick { get; private set; }
        [DataProperty]
        public Identifier Identifier { get; private set; }
        [DataProperty]
        public ChatTypePacket[] Chats { get;  private set; }
        [DataProperty]
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
        
        [DataObject]
        public class ChatTypePacket
        {
            [DataProperty] public string Name;
            [DataProperty] public string DisplayName;
            [DataProperty] public bool CanUseCommands;
            [DataProperty] public bool CanWriteMessages;
        }
        [DataObject]
        public class CommandTypePacket
        {
            [DataProperty] public string Prefix;
            [DataProperty] public string Description;
            [DataProperty] public string[] ParametersTitles;
        }
    }
}