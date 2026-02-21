using System;
using Unity.VisualScripting;
using UnityEngine;

public class TentacleSpawning : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float tentacleSize;
    void Start()
    {
        updateTentaclePosition();
    }

    void Update()
    {
        updateTentaclePosition();
    }

    private void updateTentaclePosition()
    {
        transform.position = (point1.position + point2.position) / 2;
        transform.localScale = new Vector3(tentacleSize, getPointDistance(), tentacleSize);

        //I dont understand this, but it works
        transform.rotation = Quaternion.FromToRotation(transform.forward, new Vector3(0, 1, 0)) * transform.rotation;
        transform.rotation = Quaternion.FromToRotation(transform.up, (point1.position - point2.position).normalized) * transform.rotation;

        point2.hasChanged = false;
    }

    private float getPointDistance()
    {
        float distance = Vector3.Distance(point1.position, point2.position);
        if(distance <= 0) return 0.1f;
        else return distance;
    }
}
