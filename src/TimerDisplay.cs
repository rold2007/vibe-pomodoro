using Spectre.Console;

namespace VibePomodoroConsole;

/// <summary>
/// Handles all UI rendering for the pomodoro timer using Spectre.Console.
/// </summary>
public class TimerDisplay
{
    private readonly PomodoroTimer _timer;

    public TimerDisplay(PomodoroTimer timer)
    {
        _timer = timer;
    }

    /// <summary>
    /// Renders the current state of the timer to the console.
    /// </summary>
    public void Render()
    {
        var state = _timer.GetState();
        
        try
        {
            Console.Clear();
        }
        catch
        {
            // Clear screen may fail in some environments, skip it
        }
        
        // Title
        AnsiConsole.MarkupLine("[bold cyan]Pomodoro Timer[/]");
        AnsiConsole.MarkupLine("");

        // Phase indicator
        var phase = state.IsWorking ? "Work" : "Break";
        var phaseColor = state.IsWorking ? "green" : "blue";
        AnsiConsole.MarkupLine($"Phase: [bold {phaseColor}]{phase}[/]");

        // Timer display
        var timeColor = state.IsWorking ? "green" : "blue";
        AnsiConsole.MarkupLine($"\n[bold {timeColor}]{state.RemainingMinutes:D2}:{state.RemainingSecondsInMinute:D2}[/]\n");

        // Progress bar
        var progress = (double)state.ElapsedSeconds / state.TotalSeconds;
        var progressPercent = (int)(progress * 100);
        AnsiConsole.MarkupLine($"Progress: {progressPercent}%");

        // Status
        var statusText = state.IsRunning ? "Running" : "Paused";
        var statusColor = state.IsRunning ? "yellow" : "gray";
        AnsiConsole.MarkupLine($"\nStatus: [{statusColor}]{statusText}[/]");

        // Controls
        AnsiConsole.MarkupLine("\n[dim]Controls: Space=Start/Pause | S=Skip Phase | R=Reset | Q=Quit[/]");
    }
}
