// States that can themselves have sub-states must run their own StateMachines
// The top-level and all intermediary states inherit from this
public abstract class SuperState : State
{
    // SuperStates own their own StateMachine that manages further child states
    protected StateMachine subSM;

    // Recursively follows a path of states down as far as it goes, by chasing down subSMs
    public void EnterPath(State[] path, int index)
    {
        // End if reached end of path
        if (index >= path.Length) return;

        // Change child state
        State next = path[index];
        subSM.ChangeState(next); //

        // If child isnt leaf node, keep going
        if (next is SuperState ss)
            ss.EnterPath(path, index + 1);
    }

    // Method is necessary in all SuperStates, returns a default state
    protected abstract State GetDefaultState();

    // Creates new instance for state to allow re-entry into current state
    // Auto-sets children to their default state to avoid null child states
    public override void Enter()
    {
        subSM = new StateMachine();
        subSM.ChangeState(GetDefaultState());
    }

    /// To test
    /// root -> a -> b -> c
    /// root.ChangeDeep([a],[a,b],[a,b,c])
    /// a.ChangeDeep([b],[b,c])
    // Recursively change states as far down as the path goes
    public void EnterDeep(State[] path, int index)
    {
        subSM = new StateMachine();
        // Enter one level deeper
        subSM.ChangeDeepState(path, index + 1);
    }

    // Kill/deactivate all child states of the sub-fsm
    public override void Exit() { subSM.ChangeState(null); } 

    // Override methods to also update the sub StateMachine of this super-state
    public override void Update() { subSM.Update(); }
    public override void FixedUpdate() { subSM.FixedUpdate(); }
    public override void LateUpdate() { subSM.LateUpdate(); }
}