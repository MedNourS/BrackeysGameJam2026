using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
// https://www.desmos.com/3d/rk1z9phpw1 link to original desmos graph

// NOTE: by step in comment i mean the number next to each text area or formula
// ALSO disregard any step where no variable is getting a value assigned
public class TentacleWrapping : MonoBehaviour
{
    // FOR TESTING ONLY
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
    // Step 17 and 18
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
    // Step 19 and 20
    private static Vector3[] Cross(Vector3[] normalized)
    {
        Vector3[] crossed = new Vector3[normalized.Length - 1];
        for (int i = 0; i < crossed.Length; i++)
        {
            crossed[i] = Vector3.Cross(normalized[i + 1], normalized[i]);
        }
        
        return crossed;
    }
    // This returns the vector of all vectors in the array added together
    // Used in step 29 and 30, as well as 21 and 22
    private static Vector3 AddVectorArray(Vector3[] toAdd)
    {
        Vector3 totalVector = new Vector3();
        for (int i = 0; i < toAdd.Length; i++)
        {
            totalVector += toAdd[i];
        }
        return totalVector;
    }
    // Desmos explanation: If A is (0,1,0) then the cross product will return 0, so get the dot product and if A aligns too closely with (0,1,0) then use (1,0,0) (important step!!)
    // Step 24 and 25
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

    // Step 26 to 28
    private static Vector3[] GetPerpendicularVector(Vector3 axis, Vector3 importantStepResult)
    {
        Vector3 U = Vector3.Cross(axis, importantStepResult);
        Vector3 V = Vector3.Cross(U, axis);
        Vector3[] vectors = new Vector3[2];
        vectors[0] = U;
        vectors[1] = V;
        return vectors;
    }
    // Step 29 and 30
    private static Vector3 GetCentroid(Vector3[] points)
    {
        Vector3 centroid =  AddVectorArray(points) / points.Length;
        return centroid;
    }
    // Step 31 and 32
    private static Vector3[] CenterPoints(Vector3[] points, Vector3 centroid)
    {
        Vector3[] centeredPoints = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            centeredPoints[i] = points[i] - centroid;
        }
        return centeredPoints;
    }
    // Step 33 and 34
    private static Vector3[] PutPointsOnPlane(Vector3[] centeredPoints, Vector3 axis)
    {
        Vector3[] pointsOnPlane = new Vector3[centeredPoints.Length];
        for (int i = 0; i < centeredPoints.Length; i++)
        {
            pointsOnPlane[i] = Vector3.ProjectOnPlane(centeredPoints[i], axis);
        }
        return pointsOnPlane;
        
    }
    // Step 38 and 39
    private static Vector2[] FlattenPoints(Vector3[] pointsOnPlane, Vector3[] perpendicularVectors)
    {
        Vector2[] flattenedPoints = new Vector2[pointsOnPlane.Length];
        for (int i = 0; i < pointsOnPlane.Length; i++)
        {
            flattenedPoints[i] = new Vector2(Vector3.Dot(pointsOnPlane[i], perpendicularVectors[0]), Vector3.Dot(pointsOnPlane[i], perpendicularVectors[1]));
        }
        return flattenedPoints;
    }
    // Step 41 to end basically
    private static bool IsWrapped(Vector2[] flatPoints)
    {
        float tolerance = 6.283185307179586f;
        float totalAngle = 0;
        for (int i = 0; i < flatPoints.Length - 1; i++)
        {
            totalAngle += math.atan2(flatPoints[i].x *flatPoints[i + 1].y - flatPoints[i].y *flatPoints[i + 1].x, flatPoints[i].x *flatPoints[i + 1].x + flatPoints[i].y *flatPoints[i + 1].y);
        }
        if (totalAngle > tolerance)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    #endregion

    #region callers
    // First folder of steps (9 to 22, not including a few)
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
        // Step 24 and 25
        Vector3 aligningAxis = GetNonAligningAxis(axis);
        // Step 26 to 28
        Vector3[] perpendicularVector = GetPerpendicularVector(axis, aligningAxis);
        // Step 31 and 32
        Vector3[] centeredPoints = CenterPoints(points, centroid);
        // Step 33 and 34
        Vector3[] pointsOnPlane = PutPointsOnPlane(centeredPoints, axis);
        // Step 38 and 39
        Vector2[] flattenedPoints = FlattenPoints(pointsOnPlane, perpendicularVector);
        return flattenedPoints;
    }
    #endregion

    public static GameObject GetWrappedAround(Vector3[] points, LayerMask layerMask)
    {
        // Step 29 text, step 30 math
        Vector3 centroid = GetCentroid(points);
        // make this lower if spherecast interacts with other objects
        float sphereCastTolerance = 1f;
        bool hit = Physics.SphereCast(centroid, sphereCastTolerance, Vector3.up, out RaycastHit hitInfo, 0.0001f, layerMask);

        // if the centroid has a gameobject at it, we actually do the math
        if (hit)
        {
            // step 9 folder
            Vector3 axis = GetAxis(points);

            // Step 23 to 39, skipping 29 and 30
            Vector2[] flattenedPoints = projecting(axis, points, centroid);
            Vector2[] flattenedNormalizedPoints = new Vector2[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                flattenedNormalizedPoints[i] = flattenedPoints[i].normalized;
            }

            // Entire wrapping detection (step 41 to end, skipping the visual ones (being anywhere where a variable is not assigned))
            if (IsWrapped(flattenedNormalizedPoints))
            {
                return hitInfo.transform.gameObject;
            }
            // The object is not wrapped at this else
            else
            {
                return null;
            }
        }
        // The object does not exist at this else
        else
        {
            return null;
        }
    }
}