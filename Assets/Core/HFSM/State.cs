// All States must derive from this (as leaf nodes) or the SuperState class
public abstract class State
{
    // Context is passed down through states from the PlayerController
    protected PlayerContext context;

    // The parent state machine is owned by the parent state and holds sibling states to this
    // It is needed to call methods like ChangeState()
    protected StateMachine parentSM; 

    // Constructor for state passes context down
    public State(PlayerContext ctx) { context = ctx; }

    // Identification of the parent stateMachine
    // Run before State.Enter() so that this state has a reference to its parentSM
    public void SetStateMachine(StateMachine stateMachine) { parentSM = stateMachine; }

    // Methods that a state can have
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void LateUpdate() { }
}