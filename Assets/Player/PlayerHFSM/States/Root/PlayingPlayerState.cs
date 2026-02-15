using UnityEngine;

public class PlayingPlayerState : SuperState 
{
    // Constructor for passing context
    public PlayingPlayerState(PlayerContext ctx) : base(ctx) { }

    // Default child
    protected override State GetDefaultState() { return new MovementPlayerState(context); }

    public override void Enter() {
        base.Enter();
    }
    public override void Exit() { 
        base.Exit();
    }

    public override void Update() { base.Update(); Debug.Log("Now Playing"); }
}
