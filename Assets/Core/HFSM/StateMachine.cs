using UnityEngine;

/// <summary>
/// This is just an object that stores/updates a state and that can be
/// accessed by other states to get/set state
/// 
/// The actual Update() methods are run by the parent MonoBehaviour,
/// and trickle down to child states
/// </summary>
public class StateMachine
{
    private State currState;

    // Update all these methods so that they are accessible in derived states
    public void Update() => currState?.Update();
    public void FixedUpdate() => currState?.FixedUpdate();
    public void LateUpdate() => currState?.LateUpdate();

    // This is run from within states to change the state machine's state
    // As well as for state instantiation from null
    public void ChangeState(State newState) {
        /// Idk what to do with this...
        //// Don't run state change if already in state instance
        //// Different instances of the same state though are valid ch
        //if (currState == newState) return;

        Debug.Log("exited" + currState);
        currState?.Exit();
        currState = newState;

        // Our states have decoupled ownership and existence, so null states can exist
        // (usually in child states of innactive super-states)
        Debug.Log("entered" + currState);
        currState?.SetStateMachine(this);
        currState?.Enter();
    }

    // Handle state changes more than 1 layer deep
    /// KNOWN BUG: Exiting states sets all children to null,
    /// meaning that we can exit and re-enter child states
    /// wait is this a bug in my current implementation?
    public void ChangeDeepState(State[] path, int index = 0)
    {
        // Quit if path is empty
        if (path.Length == 0) return;

        State next = path[index];

        // ChangeState and quit if reached end
        // or if by some mistake a non-ending state doesnt have children
        if (index >= path.Length - 1 || !(currState is SuperState ss)) {
            ChangeState(next);
            return;
        }

        Debug.Log("exited super " + currState);
        currState?.Exit();
        currState = next;

        // Our states have decoupled ownership and existence, so null states can exist
        // (usually in child states of innactive super-states)
        Debug.Log("entered super " + currState);

        currState?.SetStateMachine(this); // The magic line

        // Propagate through SSs to reach desired deep state (recursively)
        ss.EnterDeep(path, index);
    }

    // Access state without publicizing it 
    public State GetState() { return currState; }
}
