using Spectre.Console;

namespace VibePomodoroConsole;

class Program
{
    static void Main(string[] args)
    {
        var timer = new PomodoroTimer(workMinutes: 25, breakMinutes: 5);
        var display = new TimerDisplay(timer);

        bool running = true;

        // Start the timer
        timer.Start();

        while (running)
        {
            display.Render();
            timer.Tick();

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

            // Update every 100ms
            System.Threading.Thread.Sleep(100);
        }

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[yellow]Pomodoro timer stopped.[/]");
    }
}
