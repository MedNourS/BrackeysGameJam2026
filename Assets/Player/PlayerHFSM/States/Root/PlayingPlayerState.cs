using UnityEngine;

public class PlayingPlayerState : SuperState 
{
    // Default child
    protected override State GetDefaultState() { return new MovementPlayerState(); }

    public override void Enter() {
        base.Enter();
    }
    public override void Exit() { 
        base.Exit();
    }

    public override void Update() { base.Update(); Debug.Log("Now Playing"); }
}
