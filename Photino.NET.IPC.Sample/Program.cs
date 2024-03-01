using System.Runtime.InteropServices;
using Photino.NET;
using Photino.NET.IPC;

internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        const string title = "Photino IPC Sample";

        var window = new PhotinoWindow()
            .SetTitle(title)
            .SetUseOsDefaultSize(false)
            .SetSize(2000, 1500)
            .Center()
            // Most event handlers can be registered after the
            // PhotinoWindow was instantiated by calling a registration 
            // method like the following RegisterWebMessageReceivedHandler.
            // This could be added in the PhotinoWindowOptions if preferred.
            .RegisterChannel<string>("test-channel", (sender, e) =>
            {
                Console.WriteLine(e.Data);
                sender.SendMessageToRenderer(e);
            })
            .Load("wwwroot/index.html");

        // You can add multiple handlers to the same channel
        var channel = InterProcessCommunication.Instance.GetChannel<string>("test-channel").Parse<string>();
        channel.MessageReceived += (s, e) => Console.WriteLine("Message received from second handler: {0}", e.Message.Data);

        window.WaitForClose();
    }
}