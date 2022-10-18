using System.Collections;
using ServerPlugin.Commands;
using UndefinedServer;
using UndefinedServer.Chats;
using UndefinedServer.Commands;
using UndefinedServer.Events.Chats;
using UndefinedServer.Plugins;
using Utils.Events;

namespace ServerPlugin;

public sealed class Main : Plugin, IEventCaller
{
    public override string Name { get; } = "ChatTest";
    public override PluginConfiguration Configuration { get; } = new MainPluginConfiguration();
    public static string[] OpPlayers = { "Takumi"};

    protected override void OnEnable()
    {
        UServer.Logger.Info("enabled " + Name);
        EventManager.RegisterEvents(this);
        ChatManager.RegisterChat<WorldChat>();
        var chat = ChatManager.RegisterChat<WhisperChat>();
        CommandManager.RegisterCommand<TestCommand>();
        CommandManager.RegisterCommand<MsgCommand>().ToChat = chat;

        print(ChatManager.Chats.Count+" loaded chat");
        print(CommandManager.Commands.Count + " loaded commands");
    }
    protected override void OnDisable()
    {
        UServer.Logger.Info("disabled " + Name);
        EventManager.UnregisterEvents(this);
    }

    [EventHandler]
    private void OnMessage(ChatEvent e)
    {
        string text = e.Message;
        print("[" + e.ChatType.DisplayName + "]" + e.Sender.Nickname + ": " + e.Message);
    }
    [EventHandler]
    private void OnCommand(PlayerCommandEvent e)
    {
        print("[" + e.Sender.Nickname + "] send command: " + e.Command.Prefix);
    }

    public static void print(string msg)
    {
        UServer.Logger.Info(msg);
    }

    public static bool IsOpPlayer(ServerPlayer pl) => ((IList)OpPlayers).Contains(pl.Nickname);
}
