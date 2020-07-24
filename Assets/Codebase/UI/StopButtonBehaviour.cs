using UnityEngine;
using UnityEngine.EventSystems;

public class StopButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        FindObjectOfType<StopWatchBehaviour>().BeginTimer();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FindObjectOfType<StopWatchBehaviour>().EndTimer();
    }
}
