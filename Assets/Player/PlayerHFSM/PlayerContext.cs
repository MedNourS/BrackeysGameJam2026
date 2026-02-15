using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContext : MonoBehaviour
{
    public PlayerController pc { get; }
    public CharacterController cc { get; }

    public InputActionMap playerMap { get; }
    public InputActionMap UIMap { get; }

    public Transform headTarget { get; }
    public Transform groundCheck { get; }
    public Transform ceilCheck { get; }

    public LayerMask terrainMask { get; }

    // Assign all objects based on the gameObject the playercontroller is attached to
    public PlayerContext(PlayerController controller)
    {
        pc = controller;
        cc = pc.GetComponent<CharacterController>();

        playerMap = pc.PlayerMap;
        UIMap = pc.UIMap;
    }
}