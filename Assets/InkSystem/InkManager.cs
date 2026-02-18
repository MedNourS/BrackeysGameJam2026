using UnityEngine;
using UnityEngine.InputSystem;

public class InkManager : MonoBehaviour
{
    [SerializeField] private GameObject inkPrefab;
    public static InkManager Instance;

    private void Awake()
    {
        if(Instance != null) return;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.value, Mouse.current.position.y.value, 0));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                createInkBlob(hit.point, hit.normal);
            }
        }

        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            checkIfStandingOnInk(transform.position);
        }
    }

    //Create a decal
    public void createInkBlob(Vector3 pos, Vector3 normal)
    {
        GameObject decalObject = Instantiate(inkPrefab, pos, Quaternion.identity);
        //Makes the object face the same direction as the opposite of the normal
        decalObject.transform.forward = -normal;
        //Add an offset in the normal direction so the collider sticks out, divide by 10 so it sticks out just a tiny bit
        decalObject.transform.position += normal / 10;
    }

    //Checks if player is standing on ink
    public bool checkIfStandingOnInk(Vector3 pos)
    {
        //Sends a ray down (-y)
        Ray ray = new Ray(pos, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            //If ray hits a collider with the "Ink" tag
            if(hit.collider.gameObject.CompareTag("Ink"))
            {
                return true;
            }
        }
        return false;
    }
}