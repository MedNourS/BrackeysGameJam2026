using System;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class InkManager : MonoBehaviour
{
    [SerializeField] private GameObject inkPrefab;
    public static InkManager Instance;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        if(Instance != null) return;
        Instance = this;
    }

    // For testing purposes
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.value, Mouse.current.position.y.value, 0));
            if(Physics.Raycast(ray, out hit))
            {
                createInkBlob(hit.point, hit.normal, 1, 1);
            }
        }

        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            checkIfStandingOnInk(transform.position, Vector3.down);

            Vector3 size = GetComponent<MeshFilter>().mesh.bounds.size;

            Ray forwardRay = new Ray(transform.position, Vector3.forward);
            Ray backRay = new Ray(transform.position, Vector3.back);
            Ray upRay = new Ray(transform.position, Vector3.up);
            Ray downRay = new Ray(transform.position, Vector3.down);
            Ray leftRay = new Ray(transform.position, Vector3.left);
            Ray rightRay= new Ray(transform.position, Vector3.right);

            if(Physics.Raycast(forwardRay, out hit, size.z))
            {
                createInkBlob(hit.point, hit.normal, size.x, size.y);
            }

            if(Physics.Raycast(backRay, out hit, size.z))
            {
                createInkBlob(hit.point, hit.normal, size.x, size.y);
            }

            if(Physics.Raycast(upRay, out hit, size.y))
            {
                createInkBlob(hit.point, hit.normal, size.z, size.x);
            }

            if(Physics.Raycast(downRay, out hit, size.y))
            {
                createInkBlob(hit.point, hit.normal, size.z, size.x);
            }

            if(Physics.Raycast(leftRay, out hit, size.x))
            {
                createInkBlob(hit.point, hit.normal, size.z, size.y);
            }

            if(Physics.Raycast(rightRay, out hit, size.x))
            {
                createInkBlob(hit.point, hit.normal, size.z, size.y);
            }

        }
    }

    /// <summary>
    /// Creates a decal object
    /// </summary>
    /// <param name="pos">The position of the object</param>
    /// <param name="normal">The direction is it supposed to face</param>
    /// <param name="height">Height of the object</param>
    /// <param name="width">Width of the object</param>
    public void createInkBlob(Vector3 pos, Vector3 normal, float width, float height)
    {
        GameObject decalObject = Instantiate(inkPrefab, pos, Quaternion.identity);

        //Sets the size of the decal
        decalObject.GetComponent<DecalProjector>().size = new Vector3(width, height, 1);
 
        //Makes the object face the same direction as the normal
        decalObject.transform.forward = -normal;

        //Add an offset in the normal direction so the collider doesn't stick out, divide by 10 so it sticks out a bit
        //It also fixes the flickering
        decalObject.transform.position += normal  / 10;

        //Set the size of the box collider to be the same as the decal
        decalObject.GetComponent<BoxCollider>().size = new Vector3(width, height, 1);
    }

    /// <summary>
    /// Checks if the player is standing on ink
    /// </summary>
    /// <param name="pos">The position of the player</param>
    /// <param name="directionToCheck">The direction to check(Vector3.down, up, forward, etc)</param>
    /// <returns>Returns true if player is standing on ink</returns>
    public bool checkIfStandingOnInk(Vector3 pos, Vector3 directionToCheck)
    {
        //Sends a ray in the given direction
        Ray ray = new Ray(pos, directionToCheck);
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