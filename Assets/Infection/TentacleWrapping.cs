using System;
using Unity.Mathematics;
using UnityEngine;

public class TentacleWrapping : MonoBehaviour
{
    private static Vector3[] createPoints(int amountOfPoints)
    {
        Vector3[] toreturn = new Vector3[amountOfPoints];
        const float rotations = 1.2f;

        for (int i = 1; i < (amountOfPoints + 1); i++)
        {
            float theta = i * (rotations * Mathf.PI) / amountOfPoints;
            float x = Mathf.Sin(theta);
            float y =  Mathf.Cos(theta);
            float z = -i/amountOfPoints;
            
            float noise = i / math.sqrt(amountOfPoints);

            x = (UnityEngine.Random.value * noise) + x;
            y = (UnityEngine.Random.value * noise) + y;
            z = (UnityEngine.Random.value * noise) + z;
            toreturn[i] = new Vector3(x, y, z);
        }
        return toreturn;
    }
    // This normalizes adjascent vectors
    private static Vector3[] normalizeAdjascent(Vector3[] toNormalize)
    {
        Vector3[] normalized = new Vector3[toNormalize.Length - 1];
        for (int i = 0; i < normalized.Length; i++)
        {
            normalized[i] = Vector3.Normalize(toNormalize[i + 1] - toNormalize[i]);
        }
        return normalized;
    }
    // This cross products adjascent vectors,
    private static Vector3[] cross(Vector3[] normalized)
    {
        Vector3[] crossed = new Vector3[normalized.Length - 1];
        for (int i = 0; i < crossed.Length; i++)
        {
            crossed[i] = Vector3.Cross(normalized[i + 1], normalized[i]);
        }
        
        return crossed;
    }
    // This returns the normalized vector of all vectors in the array added together
    private static Vector3 normalizeAllTogether(Vector3[] toAdd)
    {
        Vector3 totalVector = new Vector3();
        for (int i = 0; i < toAdd.Length; i++)
        {
            totalVector += toAdd[i];
        }
        totalVector = Vector3.Normalize(totalVector);
        return totalVector;
    }
}