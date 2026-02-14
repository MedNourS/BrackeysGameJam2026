using System;
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
    // Debug indexing for state machines
    /*
    private static int globalIndex;
    private int index;
    private bool assigned;

    public void Instantiate()
    {
        if (assigned != true)
        {
            index = globalIndex;
            globalIndex++;
            assigned = true;
        }
    }
    */

    private State currState;

    // Update all these methods so that they are accessible in derived states
    public void Update() => currState?.Update();
    public void FixedUpdate() => currState?.FixedUpdate();
    public void LateUpdate() => currState?.LateUpdate();

    // This is run from within states to change the state machine's state
    // As well as for state instantiation from null
    public void ChangeState(State newState, bool forceReentry = false, bool enterChildren = true) {
        // # Instantiate();

        // Dont reenter states unless forcing reentry
        if (currState != null && newState != null && currState.GetType() == newState.GetType() && !forceReentry) 
            return;

        // # if (currState != null)
        // #    Debug.Log("SM " + index + " exited " + currState);

        currState?.Exit();
        currState = newState;

        // Our states have decoupled ownership and existence, so null states can exist
        // (usually in child states of innactive super-states)
        // # Debug.Log("SM " + index + " entered " + currState);
        currState?.SetStateMachine(this);

        // Don't run the default child assignment if preventing enterChildren
        if (currState is SuperState ss && !enterChildren) ss.StopPropagation();
        
        currState?.Enter();
    }

    // Handle state changes more than 1 layer deep
    /// To test
    /// root -> a -> b -> c
    /// root.ChangeDeep([a],[a,b],[a,b,c])
    /// a.ChangeDeep([b],[b,c])
    public void ChangeDeepState(State[] path, bool forceReentry = false, int index = 0)
    {
        // # Instantiate();
        // # Debug.Log("SM " + this.index + " at depth " + index);

        // Quit if path is empty
        if (path.Length == 0) return;

        State next = path[index];

        // # Debug.Log("curr is " + currState + " next is " + next);

        // Skip this level if theyre the same state and move deeper
        if (currState == null || currState.GetType() != next.GetType() || forceReentry)
        {
            Debug.Log("Ready to enter");
            // Propagate through SSs to reach desired deep state (recursively)
            // Dont do it though if we are at the end of the path
            if (currState is SuperState ss && index < path.Length - 1)
            {
                ChangeState(next, forceReentry, false);
                ss.subSM.ChangeDeepState(path, forceReentry, index + 1);
            }
            else
                // # Debug.Log("end of chain, final entry");

                // Even if this is a superstate since its at the end of the chain we want to enter defauts
                ChangeState(next, forceReentry);
        }
        // Not reached end yet
        else if (index < path.Length - 1)
        {
            // # Debug.Log("Skipping entry");
            if (currState is SuperState ss)
                ss.subSM.ChangeDeepState(path, forceReentry, index + 1);
        }
    }

    // Access state without publicizing it 
    public State GetState() { return currState; }
}