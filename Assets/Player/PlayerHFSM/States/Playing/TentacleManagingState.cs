using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TentacleManagingState : State
{
    /* Making a list of joints (between tentacles) and tentacles */
    private List<GameObject> joints; // [furthest joint, ... ,  closest joint, player object]
    private List<GameObject> tentacles;

    /* Makes a list of vector3 positions where joints are, which is used for lerping (smooth movement :D) */
    private List<Vector3> vector3Memory;

    /* Timer so that joints and tentacles dont update every frame */
    private float time;

    // Constructor for passing context
    public TentacleManagingState(PlayerContext ctx) : base(ctx) { }

    public override void Enter()
    {
        /* Populate the joints, tentacles and vector3 lists */
        joints = new List<GameObject> { context.player };
        tentacles = new List<GameObject>();
        vector3Memory = new List<Vector3> { context.player.transform.position };

        /* Add [context.maxTentacleLength] amount of tentacles, and [context.maxTentacleLength + 1] joints */
        for (int i = 0; i < context.maxTentacleLength; i++)
        {
            /* Instantiate a joint, set the position to the player's, and putting it in the before last space of the list */
            GameObject joint = Object.Instantiate(context.tentacleJoint);
            joint.transform.position = context.player.transform.position;
            joints.Insert(joints.Count - 1, joint);

            /* Adding a tentacle */
            tentacles.Add(Object.Instantiate(context.tentacleConnector));

            /* Adding the joints vector3 position to the list */
            vector3Memory.Add(joints[i].transform.position);
        }

        time = 0;
    }

    public override void Exit() { }

    public override void Update()
    {
        float distance = Vector3.Distance(joints[joints.Count - 2].transform.position, joints[joints.Count - 1].transform.position);

        Debug.Log($"Joints count: {joints.Count}");
        Debug.Log($"Memory count: {vector3Memory.Count}");

        /* Case where it is time to add a new node */
        if (context.tentacleUpdateTime <= time && 0f < distance)
        {
            /* Reset the timer */
            time = 0f;

            /* If there are more joints than necessary */
            if (context.maxTentacleLength + 1 <= joints.Count)
            {
                /* Destroy the end (joint furthest away from the player) */
                Object.Destroy(joints[0]);
                joints = joints.GetRange(1, joints.Count - 1);
                vector3Memory = vector3Memory.GetRange(1, vector3Memory.Count - 1);
            }

            /* If there are more tentacles than necessary */
            if (context.maxTentacleLength <= tentacles.Count)
            {
                /* Destroy the last tentacle */
                Object.Destroy(tentacles[0]);
                tentacles = tentacles.GetRange(1, tentacles.Count - 1);
            }


            /* Make a new joint, and insert it into the list */
            GameObject joint = Object.Instantiate(context.tentacleJoint);
            joint.transform.position = context.player.transform.position;
            joints.Insert(joints.Count - 1, joint);

            /* Same thing with vector3memory and tentacles */
            vector3Memory.Insert(vector3Memory.Count - 1, joint.transform.position);
            tentacles.Add(Object.Instantiate(context.tentacleConnector));

            /* Yaroslav should get this part: get the script of every tentacle and set their point1 and 2 */
            for (int i = 0; i < joints.Count - 1; i++)
            {
                TentacleSpawning tentacleInfo = tentacles[i].GetComponent<TentacleSpawning>();
                tentacleInfo.point1 = joints[i].transform;
                tentacleInfo.point2 = joints[i + 1].transform;
                tentacleInfo.tentacleSize = 1f;
            }
            GameObject capturedObject = TentacleWrapping.GetWrappedAround(convertObjectToVector3(joints));
            if(capturedObject != null)
            {
                context.capturedObject = capturedObject;
                parentSM.ChangeState(new CapturedPlayerState(context));
            }
        }
        /* Case where we update the lerp */
        else
        {
            time += Time.deltaTime;

            /* Declan should get this part: Lerp every joint to be in between the memory position to the next memory position */
            for (int i = 0; i < joints.Count - 2; i++)
            {
                joints[i].transform.position = Vector3.Lerp(
                    vector3Memory[i], // previous (or current) official position of the joint
                    vector3Memory[i + 1], // next position of the joint
                    time / context.tentacleUpdateTime
                );
            }
        }
    }

    private Vector3[] convertObjectToVector3(List<GameObject> objects)
    {
        Vector3[] vectors = new Vector3[objects.Count];
        for(int i = 0; i < objects.Count; i++)
        {
            vectors[i] = objects[i].transform.position;
        }
        return vectors;
    }
}