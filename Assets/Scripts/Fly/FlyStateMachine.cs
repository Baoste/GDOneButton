
public class FlyStateMachine
{
    public FlyState currentState;

    public void Initialize(FlyState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState(FlyState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}