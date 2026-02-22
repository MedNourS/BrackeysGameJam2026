using System.Collections;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    private IEnumerator enumerator;
    private TMP_Text text;
    private void Awake()
    {
        Debug.Log("hi");
    }

    private static IEnumerator tutorial()
    {
        yield return null;
       //yield return StartCoroutine(AwaitKeyPress(KeyCode.Space));
    }

    private static IEnumerator AwaitKeyPress(KeyCode keyCode)
    {
        while (!Input.GetKeyDown(keyCode))
        {
            yield return null;
        }
    }
}
