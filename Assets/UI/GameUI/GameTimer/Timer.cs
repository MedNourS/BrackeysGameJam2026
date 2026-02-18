using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float timeLeft;
    public static Timer Instance { get; private set; }

    [SerializeField] private TMP_Text text;

    void Start()
    {
        timeLeft = 300f;
    }
    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
        Debug.Log(text);
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        text.text = timeLeft.ToString();
    }

}