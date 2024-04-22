using Microsoft.Extensions.DependencyInjection;
using Photino.NET;
using Photino.NET.IPC;

internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        const string title = "Photino IPC Sample";

        var services = new ServiceCollection();

        services.AddInterProcessCommunication(typeof(Program).Assembly);

        var window = new PhotinoWindow()
            .SetTitle(title)
            .SetUseOsDefaultSize(false)
            .SetSize(2000, 1500)
            .Center()
            .RegisterDescriptors()
            .Load("wwwroot/index.html");

        window.WaitForClose();
    }
}

class TestChannelDescriptor : IChannelDescriptor
{
    public void RegisterChannel(PhotinoWindow window)
    {
        window.RegisterChannel<string>("PHOTINO_TEST_CHANNEL", (sender, e) =>
        {
            Console.WriteLine(e.Data);
            sender.Emit("Hello Javascript 🚀!");
        });
    }

    public void RegisterChannelServices(IServiceCollection services)
    {
        // TODO: For this example we don't need services
    }
}
