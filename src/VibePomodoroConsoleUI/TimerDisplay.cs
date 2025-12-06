using Spectre.Console;
using Spectre.Console.Rendering;
using VibePomodoroCore;

namespace VibePomodoroConsoleUI;

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
    /// Returns the static UI content (title and controls).
    /// </summary>
    public IRenderable GetStaticRenderable()
    {
        var markup = "[bold cyan]Pomodoro Timer[/]\n\n[dim]Controls: Space=Start/Pause | S=Skip Phase | R=Reset | Q=Quit[/]\n";
        return new Markup(markup);
    }

    /// <summary>
    /// Returns the dynamic UI content (timer, phase, progress, status).
    /// </summary>
    public IRenderable GetDynamicRenderable()
    {
        var state = _timer.GetState();
        var phase = state.IsWorking ? "Work" : "Break";
        var phaseColor = state.IsWorking ? "green" : "blue";
        var timeColor = state.IsWorking ? "green" : "blue";
        var progress = (double)state.ElapsedSeconds / state.TotalSeconds;
        var progressPercent = (int)(progress * 100);
        var statusText = state.IsRunning ? "Running" : "Paused";
        var statusColor = state.IsRunning ? "yellow" : "grey";

        var markup = $@"
Phase: [bold {phaseColor}]{phase}[/]
[bold {timeColor}]{state.RemainingMinutes:D2}:{state.RemainingSecondsInMinute:D2}[/]
Progress: {progressPercent}%

Status: [{statusColor}]{statusText}[/]
";
        return new Markup(markup);
    }
}
