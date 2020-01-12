using UnityEngine;
using WaterRippleForScreens;

public class ScreenRippleAtPosition : MonoBehaviour
{
    public Transform RippleLocation;

    public void Ripple()
    {
        FindObjectOfType<RippleEffect>().SetNewRipplePosition(RippleLocation.transform.position);
    }
}
