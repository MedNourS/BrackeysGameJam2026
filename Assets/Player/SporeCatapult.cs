using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.InputSystem;

public class SporeCatapult : MonoBehaviour
{
    [SerializeField] private GameObject catapultObject;
    [SerializeField] private float holdThreshold;
    [SerializeField] private float holdTimer;
    [SerializeField] private float forceMagnitude;
    [SerializeField] private Transform direction;
    private bool isHolding = false;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = catapultObject.transform.position + new Vector3(0, 10, 0);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isHolding = true;
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            holdTimer = 0;
            isHolding = false;
        }

        if (isHolding)
        {
            holdTimer += Time.deltaTime;
        }
        if(holdTimer > holdThreshold)
        {
            Debug.Log("Held for: " + Camera.main.transform.rotation.eulerAngles);
            rb.AddForce(direction.forward * forceMagnitude);
        }
    }


}
