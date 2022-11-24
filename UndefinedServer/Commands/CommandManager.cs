using System.Collections.Generic;
using System.Linq;
using UndefinedNetworking;
using UndefinedNetworking.Commands;
using UndefinedNetworking.Exceptions;
using UndefinedNetworking.Packets.Command;
using UndefinedServer.Chats;
using UndefinedServer.Events;
using UndefinedServer.Events.Chats;
using Utils.Enums;
using Utils.Events;

namespace UndefinedServer.Commands
{
    public class CommandManager
    {
        private static readonly Enum<Command> ECommands = new();
        private readonly CommandManager _instance;
        
        public static IReadOnlyList<Command> Commands => ECommands.Values;
        
        public CommandManager()
        {
            _instance = _instance is null ? this : throw new InstanceException($"{nameof(CommandManager)} is already exists");
            this.RegisterListener();
           
        }

        public static Command GetCommnand(string prefix)
        {
            if (!ECommands.Contains(prefix)) throw new CommandException($"unknown command {prefix}");
            return ECommands[prefix];
        }

        public static bool TryGetCommand(string prefix, out Command? command)
        {
            if (ECommands.Contains(prefix))
            {
                command = ECommands[prefix];
                return true;
            }
            command = null;
            return false;
        }
        public static Command GetCommnand(int id)
        {
            if (!ECommands.Contains(id)) throw new CommandException($"unknown command {id}");
            return ECommands[id];
        }

        public static bool TryGetCommand(int id, out Command? command)
        {
            if (ECommands.Contains(id))
            {
                command = ECommands[id];
                return true;
            }
            command = null;
            return false;
        }

        public static T RegisterCommand<T>() where T : Command, new()
        {
            if (Undefined.IsEnabled) throw new CommandException("Commands can be register only before server is enabled. Use is on OnEnable()");
            var command = new T();
            if (string.IsNullOrEmpty(command.Prefix))
                throw new CommandException(
                    $"Register command {nameof(T)} failed. {nameof(command.Prefix)} cant be null or empty");
            if (command.Parameters is null || command.Parameters.Length < 2)
            {
                ECommands.AddMember(command.Prefix, command);
                return command;
            }

            var isRequired = false;
            for (var i = command.Parameters.Length - 1; i >= 0; i--)
            {
                var current = command.Parameters[i];
                if(current.IsRepeatable && i != command.Parameters.Length - 1) 
                    throw new CommandException(
                        $"Register command {nameof(T)} failed. Only latest argument can be {nameof(current.IsRepeatable)}");
                if (isRequired && !current.IsRequired)
                    throw new CommandException(
                        $"Register command {nameof(T)} failed. Only latest arguments can be not required");
                if (current.IsRequired) isRequired = true;
            }
            ECommands.AddMember(command.Prefix, command);
            return command;
        }

        private static bool IsComplete(Player sender, string[] pr, int commandId, out ParameterType? par)
        {
            var command = ECommands[commandId];
            for (var i = 0; i < command.Parameters!.Length; i++)
            {
                var parameter = command.Parameters[i];
                if (parameter.GetCompletes(sender) is not { } completes || completes.Contains(pr[i])) continue;
                par = parameter;                    
                return false;
            }
            par = null;
            return true;
        } 
        private static void IsComplete(Player sender, CommandCompleteRequest request)
        {
            if (ECommands.Contains(request.CommandId))
            {
                var command = ECommands[request.CommandId];
                if (command.Parameters is not null)
                {
                    var completes = new ParameterCompletesPacket[command.Parameters.Length];
                    for (var i = 0; i < command.Parameters.Length; i++)
                    {
                        var parameter = command.Parameters[i];
                        completes[i] = new ParameterCompletesPacket(parameter.GetCompletes(sender), parameter.IsRepeatable, parameter.Title);
                    }
                    sender.Client.SendPacket(new CommandCompleteAnswer(completes));
                }
                else sender.Client.SendPacket(new CommandCompleteAnswer(null));
            }
            else
                sender.Client.Disconnect(DisconnectCause.InvalidPacket, "unknown command");
        }
        
        [EventHandler]
        private void OnCommandPacket(PacketReceiveEvent e)
        {
            if (e.Packet is CommandCompleteRequest ccp)
            {
                IsComplete(Undefined.CurrentGame.GetPlayer(e.Client.Identifier), ccp);
                return;
            }
            if(e.Packet is not CommandPacket packet) return;
            if (!ChatManager.TryGetChat(packet.ChatId, out _))
            {
                e.Client.Disconnect(DisconnectCause.InvalidPacket, "unknown chat");
                return;
            }
            if (!ECommands.Contains(packet.CommandId))
            {
                e.Client.Disconnect(DisconnectCause.InvalidPacket, "unknown command");
                return;
            }
            var chat = ChatManager.GetChat(packet.ChatId);
            var command = ECommands[packet.CommandId];
            if (!chat.CanUseCommands)
            {
                e.Client.Disconnect(DisconnectCause.InvalidPacket, $"cant usage commands in chat {chat.DisplayName}");
                return;
            }
            var player = Undefined.CurrentGame.GetPlayer(e.Client.Identifier);
            var prs = new List<CommandParameter>();
            if (command.Parameters is { } parameters)
            {
                if(packet.Parameters is null || (!command.Parameters[^1].IsRepeatable && packet.Parameters?.Length != command.Parameters.Length)) 
                {   
                    command.OnFailArgumentsLength(player, chat);
                    return;
                }
                for (var i = 0; i < packet.Parameters!.Length; i++)
                {
                    var parameter = command.Parameters[i > command.Parameters.Length - 1 ? command.Parameters.Length - 1 : i];
                    if (!parameter.CompareWith(packet.Parameters[i]))
                    {
                        parameter.OnFailedUsage(player, chat);
                        return;
                    }
                    prs.Add(new CommandParameter(parameter, packet.Parameters[i]));
                }
            }
            var commandEvent = new PlayerCommandEvent(command, player, chat);
            command.Execute(player, prs.ToArray(), chat);
        }
        [EventHandler]
        private void OnServerClosed(ServerClosedEvent e)
        {
            EventManager.UnregisterEvents(this);
        }
    }
}