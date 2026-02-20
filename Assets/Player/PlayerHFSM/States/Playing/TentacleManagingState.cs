using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TentacleManagingState : State
{
    private List<GameObject> joints;
    private List<GameObject> tentacles;
    private List<Vector3> vector3Memory;

    private float time;

    // Constructor for passing context
    public TentacleManagingState(PlayerContext ctx) : base(ctx) { }

    public override void Enter()
    {
        joints = new List<GameObject> { context.player };
        tentacles = new List<GameObject>();
        vector3Memory = new List<Vector3>();

        for (int i = 0; i < context.maxTentacleLength; i++)
        {
            GameObject joint = Object.Instantiate(context.tentacleJoint);
            joint.transform.position = context.player.transform.position;
            joints.Insert(joints.Count - 1, joint);

            tentacles.Add(Object.Instantiate(context.tentacleConnector));


            vector3Memory.Add(joints[i].transform.position);
        }

        time = 0;
    }

    public override void Exit() { }

    public override void Update()
    {
        // Need to remove nailing
        float distance = Vector3.Distance(joints[joints.Count - 2].transform.position, joints[joints.Count - 1].transform.position);
        Debug.Log(distance);

        if (context.tentacleUpdateTime <= time && 0f < distance)
        {
            time = 0f;

            if (context.maxTentacleLength + 1 <= joints.Count)
            {
                Object.Destroy(joints[0]);
                joints = joints.GetRange(1, joints.Count - 1);
                vector3Memory = vector3Memory.GetRange(1, vector3Memory.Count - 1);
            }

            if (context.maxTentacleLength <= tentacles.Count)
            {
                Object.Destroy(tentacles[0]);
                tentacles = tentacles.GetRange(1, tentacles.Count - 1);
            }

            GameObject joint = Object.Instantiate(context.tentacleJoint);
            joint.transform.position = context.player.transform.position;
            joints.Insert(joints.Count - 1, joint);

            vector3Memory.Insert(vector3Memory.Count - 1, joint.transform.position);

            tentacles.Add(Object.Instantiate(context.tentacleConnector));

            for (int i = 0; i < joints.Count - 1; i++)
            {
                TentacleSpawning tentacleInfo = tentacles[i].GetComponent<TentacleSpawning>();
                tentacleInfo.point1 = joints[i].transform;
                tentacleInfo.point2 = joints[i + 1].transform;
                tentacleInfo.tentacleSize = (tentacles.Count - i) / Mathf.Pow(tentacles.Count, 0.8f);
            }
        }
        else
        {
            time += Time.deltaTime;
            for (int i = 0; i < joints.Count - 2; i++)
            {
                joints[i].transform.position = Vector3.Lerp(
                    vector3Memory[i],
                    vector3Memory[i + 1],
                    time / context.tentacleUpdateTime
                );
            }
        }
    }
}