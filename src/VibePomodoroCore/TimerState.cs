namespace VibePomodoroCore;

/// <summary>
/// Represents the current state of the pomodoro timer.
/// </summary>
public class TimerState
{
    public int WorkMinutes { get; set; }
    public int BreakMinutes { get; set; }
    public int ElapsedSeconds { get; set; }
    public bool IsWorking { get; set; }
    public bool IsRunning { get; set; }

    public int TotalSeconds => IsWorking ? WorkMinutes * 60 : BreakMinutes * 60;
    public int RemainingSeconds => TotalSeconds - ElapsedSeconds;
    public int RemainingMinutes => RemainingSeconds / 60;
    public int RemainingSecondsInMinute => RemainingSeconds % 60;
}
