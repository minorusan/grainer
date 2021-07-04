using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeAnalyticsInitBehaviour : MonoBehaviour
{
    private void Awake () {
        Amplitude amplitude = Amplitude.getInstance();
        amplitude.setServerUrl("https://api2.amplitude.com");
        amplitude.logging = true;
        amplitude.trackSessionEvents(true);
        amplitude.init("14eac46c07813e6e098837b07ae20d24");
        Destroy(this.gameObject);
    }
}
