using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    private Transform player;
    void Awake() => player = GameObject.Find("Player").transform;

    private void Update()
    {
        
        if (player != null)
        {

            transform.LookAt(player);
        }
        
    }


}