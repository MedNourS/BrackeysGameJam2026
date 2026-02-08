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

    private void Start()
    {
        playerHFSM.ChangeState(new PlayingPlayerState());
        playerHFSM.ChangeDeepState(new State[] {
            new PlayingPlayerState(),
            new InteractionPlayerState(),
            new DialoguePlayerState()
        });
    }

    // Update HFSM methods
    private void Update() { playerHFSM.Update(); }
    private void FixedUpdate() { playerHFSM.FixedUpdate(); }
    private void LateUpdate() { playerHFSM.LateUpdate(); }
}