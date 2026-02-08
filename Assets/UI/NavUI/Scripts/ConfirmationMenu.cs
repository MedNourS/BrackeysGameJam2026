using UnityEngine;
using UnityEngine.Events;

public class ConfirmationMenu : MonoBehaviour
{
    // We only use UnityEvents here so that they can be dynamically assigned in-editor
    [SerializeField] private UnityEvent yesEvent;
    [SerializeField] private UnityEvent noEvent;

    public void TriggerYes() => yesEvent.Invoke();
    public void TriggerNo() => noEvent.Invoke();
}
