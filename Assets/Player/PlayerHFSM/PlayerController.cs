using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Static instance as accessible reference to PlayerController from anywhere
    public static PlayerController Instance { get; private set; }
    
    public PlayerContext Context { get; private set; }    // Context used in HFSM
    public StateMachine PlayerHFSM { get; private set; }  // Root FSM

    private void Awake()
    {
        // On instantiation, make sure there is only one PlayerController
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        PlayerHFSM = new StateMachine(); // Create new top-level StateMachine

        Context = gameObject.GetComponent<PlayerContext>();
    }

    // Enter default state
    private void Start()
    {
        PlayerHFSM.ChangeState(new PlayingPlayerState(this.Context));
    }

    // Update HFSM methods
    private void Update() { PlayerHFSM.Update(); }
    private void FixedUpdate() { PlayerHFSM.FixedUpdate(); }
    private void LateUpdate() { PlayerHFSM.LateUpdate(); }
}