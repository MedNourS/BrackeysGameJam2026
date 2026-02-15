using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Static instance as accessible reference to PlayerController from anywhere
    public static PlayerController Instance { get; private set; }
    
    public StateMachine playerHFSM { get; private set; }

    private void Awake()
    {
        // On instantiation, make sure there is only one PlayerController
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Create new top-level StateMachine
        playerHFSM = new StateMachine();
    }

    // Testing HFSM behaviour
    private void Start()
    {
        playerHFSM.ChangeState(new PlayingPlayerState());
        playerHFSM.ChangeDeepState(new State[] {
            new PlayingPlayerState(),
            new InteractionPlayerState(),
            new DialoguePlayerState()
        });
    }

    /*
    
    Expected Start behaviour:

    SM 0 Enter Playing
    SM 1 Enter Movement
    SM 2 Enter Grounded
    
    SM 1 Exit Movement
    SM 2 Exit Grounded
    
    SM 1 Enter Interaction
    SM 2 Enter Dialogue
     
    it works!
    */

    // Update HFSM methods
    private void Update() { playerHFSM.Update(); }
    private void FixedUpdate() { playerHFSM.FixedUpdate(); }
    private void LateUpdate() { playerHFSM.LateUpdate(); }
}