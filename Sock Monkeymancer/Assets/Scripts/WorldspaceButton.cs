using UnityEngine;

public class WorldspaceButton : MonoBehaviour
{
    public WinEvent winEvent;
    void OnTriggerEnter(Collider col)
    {
        winEvent.Invoke();
    }
}
