// States that can themselves have sub-states must run their own StateMachines
// The top-level and all intermediary states inherit from this

public abstract class SuperState : State
{
    // Constructor for passing context
    public SuperState(PlayerContext ctx) : base(ctx) { }

    // SuperStates own their own StateMachine that manages further child states
    public StateMachine subSM { get; protected set; }
    private bool enterChildren = true;

    // Don't enter default child state
    public void StopPropagation() { enterChildren = false; }

    // Method is necessary in all SuperStates, returns a default state
    protected abstract State GetDefaultState();

    // Creates new instance for state to allow re-entry into current state
    // Auto-sets children to their default state to avoid null child states
    // EXCEPT if entering from ChangeDeepState()
    public override void Enter()
    {
        subSM = new StateMachine();

        if (enterChildren)
            subSM.ChangeState(GetDefaultState());

        enterChildren = true;
    }

    // Kill/deactivate all child states of the sub-fsm
    public override void Exit() { subSM.ChangeState(null); } 

    // Override methods to also update the sub StateMachine of this super-state
    public override void Update() { subSM.Update(); }
    public override void FixedUpdate() { subSM.FixedUpdate(); }
    public override void LateUpdate() { subSM.LateUpdate(); }
}