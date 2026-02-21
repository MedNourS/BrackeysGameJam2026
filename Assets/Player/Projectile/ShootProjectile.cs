using UnityEngine;
using UnityEngine.InputSystem;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public Transform player;
    [SerializeField] private Vector2 cursorPosition;
    [SerializeField] int[] screenSize = { Screen.height, Screen.width };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Update()
    {
        cursorPosition = Mouse.current.position.ReadValue();
        if (Mouse.current.leftButton.wasPressedThisFrame) Shoot(); // Shoot once
    }
    void Shoot()
    {
        Vector3 force = Camera.main.transform.forward*50;
        Vector3 offset = (Camera.main.transform.right * (cursorPosition.x - Screen.width/2)/Screen.width/2)*5000;
        GameObject newProjectile = Instantiate(projectile, player.position + Vector3.forward * 9 , player.rotation);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        rb.AddForce((force+offset));
    }
}
