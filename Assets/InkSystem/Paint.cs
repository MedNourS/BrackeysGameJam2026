using UnityEngine;
using UnityEngine.InputSystem;

public class Paint : MonoBehaviour
{
    [SerializeField] private Material infectionMat;
    [SerializeField] private GameObject prefab;

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.value, Mouse.current.position.y.value, 0));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                GameObject decalObject = Instantiate(prefab, hit.point, Quaternion.identity);
                decalObject.transform.forward = -hit.normal;
                decalObject.transform.position += hit.normal / 10;
                Debug.Log(hit.normal);
            }
        }

        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name);
                if(hit.collider.gameObject.CompareTag("Ink"))
                {
                    Debug.Log("HELLO");
                }
            }
        }
    }
}