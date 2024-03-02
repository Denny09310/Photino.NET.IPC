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
            .RegisterChannel<string>("channel-with-response-of-same-type", (sender, e) =>
            {
                Console.WriteLine(e.Data);
                sender.Emit("Hello, Javascript 🚀!");
            })
            .Load("wwwroot/index.html");

        window.WaitForClose();
    }
}