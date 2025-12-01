namespace VibePomodoroCore;

/// <summary>
/// Core business logic for the pomodoro timer.
/// </summary>
public class PomodoroTimer
{
    private TimerState _state;

    public PomodoroTimer(int workMinutes = 25, int breakMinutes = 5)
    {
        _state = new TimerState
        {
            WorkMinutes = workMinutes,
            BreakMinutes = breakMinutes,
            ElapsedSeconds = 0,
            IsWorking = true,
            IsRunning = false
        };
    }

    /// <summary>
    /// Gets the current state of the timer.
    /// </summary>
    public TimerState GetState() => new TimerState
    {
        WorkMinutes = _state.WorkMinutes,
        BreakMinutes = _state.BreakMinutes,
        ElapsedSeconds = _state.ElapsedSeconds,
        IsWorking = _state.IsWorking,
        IsRunning = _state.IsRunning
    };

    /// <summary>
    /// Starts the timer.
    /// </summary>
    public void Start()
    {
        _state.IsRunning = true;
    }

    /// <summary>
    /// Pauses the timer.
    /// </summary>
    public void Pause()
    {
        _state.IsRunning = false;
    }

    /// <summary>
    /// Advances the timer by one second.
    /// </summary>
    public void Tick()
    {
        if (!_state.IsRunning)
            return;

        _state.ElapsedSeconds++;

        // Check if current phase is complete
        if (_state.ElapsedSeconds >= _state.TotalSeconds)
        {
            _state.IsWorking = !_state.IsWorking;
            _state.ElapsedSeconds = 0;
        }
    }

    /// <summary>
    /// Resets the timer to the initial state.
    /// </summary>
    public void Reset()
    {
        _state.ElapsedSeconds = 0;
        _state.IsWorking = true;
        _state.IsRunning = false;
    }

    /// <summary>
    /// Skips to the next phase (work -> break or break -> work).
    /// </summary>
    public void SkipPhase()
    {
        _state.IsWorking = !_state.IsWorking;
        _state.ElapsedSeconds = 0;
    }
}
