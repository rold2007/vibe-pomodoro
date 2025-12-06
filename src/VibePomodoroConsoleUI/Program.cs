using Spectre.Console;
using VibePomodoroCore;

namespace VibePomodoroConsoleUI;

class Program
{
    static void Main(string[] args)
    {
        var timer = new PomodoroTimer(workMinutes: 25, breakMinutes: 5);
        var display = new TimerDisplay(timer);

        bool running = true;
        var lastTick = DateTime.UtcNow;
        int lastElapsedSeconds = -1;

        // Start the timer
        timer.Start();

        // Render static UI once
        AnsiConsole.Write(display.GetStaticRenderable());

        // Live update only the dynamic timer content
        AnsiConsole.Live(display.GetDynamicRenderable())
            .Start(ctx =>
            {
                while (running)
                {
                    // Only tick once per second
                    var now = DateTime.UtcNow;
                    if ((now - lastTick).TotalSeconds >= 1)
                    {
                        timer.Tick();
                        lastTick = now;
                    }

                    // Only refresh when seconds change
                    var currentState = timer.GetState();
                    if (currentState.ElapsedSeconds != lastElapsedSeconds)
                    {
                        ctx.UpdateTarget(display.GetDynamicRenderable());
                        lastElapsedSeconds = currentState.ElapsedSeconds;
                    }

                    // Handle user input with timeout
                    ConsoleKeyInfo? key = null;
                    try
                    {
                        if (Console.KeyAvailable)
                            key = Console.ReadKey(true);
                    }
                    catch
                    {
                        // Console input may not be available in some environments
                    }

                    if (key.HasValue)
                    {
                        switch (char.ToUpper(key.Value.KeyChar))
                        {
                            case ' ':
                                var state = timer.GetState();
                                if (state.IsRunning)
                                    timer.Pause();
                                else
                                    timer.Start();
                                break;

                            case 'S':
                                timer.SkipPhase();
                                break;

                            case 'R':
                                timer.Reset();
                                break;

                            case 'Q':
                                running = false;
                                break;
                        }
                    }

                    // Sleep for a short duration to keep UI responsive
                    System.Threading.Thread.Sleep(50);
                }
            });

        AnsiConsole.MarkupLine("[yellow]Pomodoro timer stopped.[/]");
    }
}
