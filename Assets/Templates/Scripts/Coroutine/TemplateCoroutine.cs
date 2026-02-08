using System.Collections;
using UnityEngine;

public class TemplateCoroutine : MonoBehaviour
{
    // Coroutines are like methods that you can pause and go back to
    // Whenever you do yeild return (null or WaitForSeconds),
    // Unity pauses the coroutine for that amount of time and goes back to it
    private Coroutine coroutine;
    
    // This stops the coroutine if it is currently running and starts a new one
    public void RunCoroutine()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(DoSomething());
    }

    // Example coroutine that waits 2 seconds then moves an object from a start pos to an end pos
    // Use IEnumerator to define coroutines
    private IEnumerator DoSomething()
    {
        yield return new WaitForSeconds(2f); // dont forget the NEW keyword for all WaitFor uses

        // Example coroutine that smoothly moves something over the course of 5 seconds
        float elapsedSeconds = 0f;
        float runTime = 5f;

        // Lerp position every frame
        while (elapsedSeconds < runTime)
        {
            elapsedSeconds += Time.deltaTime;
            // thing.position = Vector3.Lerp(start, end, Mathf.Clamp01(elapsedSeconds / runTime));
            
            yield return null; // Dont wait any time past the next frame before running this loop again
        }
    }
}
