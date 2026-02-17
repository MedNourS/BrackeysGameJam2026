using UnityEngine;
using UnityEngine.InputSystem;

public class Paint : MonoBehaviour
{
    [SerializeField] private Material infectionMat;

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.value, Mouse.current.position.y.value, 0));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                hit.collider.gameObject.GetComponent<MeshRenderer>().material = infectionMat;
                Debug.Log(hit.textureCoord + " " + hit.textureCoord2 + " " + hit.triangleIndex);
            }
        }
    }
}
