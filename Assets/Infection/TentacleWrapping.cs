using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
// https://www.desmos.com/3d/rk1z9phpw1 link to original desmos graph
public class TentacleWrapping : MonoBehaviour
{
    private static Vector3[] CreatePoints(int amountOfPoints)
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

    #region Helpers
    // This normalizes adjascent vectors together
    private static Vector3[] NormalizeAdjascent(Vector3[] toNormalize)
    {
        Vector3[] normalized = new Vector3[toNormalize.Length - 1];
        for (int i = 0; i < normalized.Length; i++)
        {
            normalized[i] = Vector3.Normalize(toNormalize[i + 1] - toNormalize[i]);
        }
        return normalized;
    }
    // This cross products adjascent vectors,
    private static Vector3[] Cross(Vector3[] normalized)
    {
        Vector3[] crossed = new Vector3[normalized.Length - 1];
        for (int i = 0; i < crossed.Length; i++)
        {
            crossed[i] = Vector3.Cross(normalized[i + 1], normalized[i]);
        }
        
        return crossed;
    }
    // This returns the normalized vector of all vectors in the array added together
    private static Vector3 AddVectorArray(Vector3[] toAdd)
    {
        Vector3 totalVector = new Vector3();
        for (int i = 0; i < toAdd.Length; i++)
        {
            totalVector += toAdd[i];
        }
        totalVector = Vector3.Normalize(totalVector);
        return totalVector;
    }
    // This is the important step on desmos
    // Desmos explanation: If A is (0,1,0) then the cross product will return 0, so get the dot product and if A aligns too closely with (0,1,0) then use (1,0,0) (important step!!)
    // TODO rename method maybe
    private static Vector3 GetNonAligningAxis(Vector3 axis)
    {
        Vector3 yVector = new(0, 1, 0);
        if (Vector3.Dot(axis, yVector) > 0.98)
        {
            return new Vector3(1, 0, 0);
        } 
        else
        {
            return yVector;
        }
    }
    // TODO rename variable to more descriptive
    // This gets the perpendicular vector to the axis
    private static Vector3[] GetPerpendicularVector(Vector3 axis, Vector3 importantStepResult)
    {
        Vector3 U = Vector3.Cross(axis, importantStepResult);
        Vector3 V = Vector3.Cross(U, axis);
        Vector3[] vectors = new Vector3[2];
        vectors[0] = U;
        vectors[1] = V;
        return vectors;
    }

    private static Vector3 GetCentroid(Vector3[] points)
    {
        Vector3 centroid =  AddVectorArray(points) / points.Length;
        return centroid;
    }

    private static Vector3[] CenterPoints(Vector3[] points, Vector3 centroid)
    {
        Vector3[] centeredPoints = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            centeredPoints[i] = points[i] - centroid;
        }
        return centeredPoints;
    }
    
    private static Vector3[] PutPointsOnPlane(Vector3[] centeredPoints, Vector3 axis)
    {
        Vector3[] pointsOnPlane = new Vector3[centeredPoints.Length];
        for (int i = 0; i < centeredPoints.Length; i++)
        {
            pointsOnPlane[i] = Vector3.ProjectOnPlane(centeredPoints[i], axis);
        }
        return pointsOnPlane;
        
    }
    
    private static Vector2[] FlattenPoints(Vector3[] pointsOnPlane, Vector3[] perpendicularVectors)
    {
        Vector2[] flattenedPoints = new Vector2[pointsOnPlane.Length];
        for (int i = 0; i < pointsOnPlane.Length; i++)
        {
            flattenedPoints[i] = new Vector2(Vector3.Dot(pointsOnPlane[i], perpendicularVectors[0]), Vector3.Dot(pointsOnPlane[i], perpendicularVectors[1]));
        }
        return flattenedPoints;
    }

    private static bool IsWrapped(Vector3[] flatPoints)
    {
        for (int i = 0; i < flatPoints.Length - 1; i++)
        {
            
        }
        return false;
    }
    #endregion

    #region callers
    private static Vector3 GetAxis(Vector3[] points)
    {
        //TODO Rename variables to be descriptive
        Vector3[] adjascentNormalized = NormalizeAdjascent(points);
        Vector3[] crossed = Cross(adjascentNormalized);
        Vector3 axis = AddVectorArray(crossed);
        axis = Vector3.Normalize(axis);
        return axis;
    }
    private static Vector2[] projecting(Vector3 axis, Vector3[] points, Vector3 centroid)
    {
        Vector3 aligningAxis = GetNonAligningAxis(axis);
        Vector3[] perpendicularVector = GetPerpendicularVector(axis, aligningAxis);
        Vector3[] centeredPoints = CenterPoints(points, centroid);
        Vector3[] pointsOnPlane = PutPointsOnPlane(centeredPoints, axis);
        Vector2[] flattenedPoints = FlattenPoints(pointsOnPlane, perpendicularVector);
        return flattenedPoints;
    }
    #endregion

    public static GameObject GetWrappedAround(Vector3[] points, LayerMask layerMask)
    {
        Vector3 centroid = GetCentroid(points);
        bool hit = Physics.SphereCast(centroid, 1, Vector3.up, out RaycastHit hitInfo, 0.0001f, layerMask);
        if (hit)
        {
            Vector3 axis = GetAxis(points);
            Vector2[] flattenedPoints = projecting(axis, points, centroid);
            Vector2[] flattenedNormalizedPoints = new Vector2[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                flattenedNormalizedPoints[i] = flattenedPoints[i].normalized;
            }
            bool wrapped = false;
            if (wrapped)
            {
                return hitInfo.transform.gameObject;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}