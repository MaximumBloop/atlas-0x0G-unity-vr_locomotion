using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas winCanvas;
    private GameState currentGameState;

    [Header("Events")]
    public PauseEvent pauseEvent;
    public PlayEvent playEvent;
    public WinEvent winEvent;

    void Start()
    {
        SwitchGameState(new Playing(this));
    }

    void SwitchGameState(GameState gameState)
    {
        currentGameState?.StateEnd();
        currentGameState = gameState;
        currentGameState.StateStart();
    }

    public void HandleWin()
    {
        SwitchGameState(new Win(this));
    }
}

internal abstract class GameState
{
    protected GameManager manager;
    protected GameState(GameManager manager)
    {
        this.manager = manager;
    }
    public virtual void StateStart() { }
    public virtual void StateUpdate() { }
    public virtual void StateEnd() { }
}

internal class Playing : GameState
{
    public Playing(GameManager manager) : base(manager) {}
    public override void StateStart()
    {
        manager.playEvent.Invoke();
    }

    public override void StateUpdate()
    {

    }

    public override void StateEnd()
    {

    }
}

internal class Paused : GameState
{
    public Paused(GameManager manager) : base(manager) { }

    public override void StateStart()
    {
        manager.pauseEvent.Invoke();
    }

    public override void StateUpdate()
    {

    }

    public override void StateEnd()
    {

    }
}

internal class Win : GameState
{
    public Win(GameManager manager) : base(manager) {}

    public override void StateStart()
    {
        manager.winEvent.Invoke();
    }

    public override void StateUpdate()
    {

    }

    public override void StateEnd()
    {
        
    }
}