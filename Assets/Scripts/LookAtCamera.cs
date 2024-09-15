using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {


    private enum Mode {
        LookAt,
        LookAtInverted,
        CameraForward,
        CmeraForwardInverted,
    }

    [SerializeField] private Mode mode;

    private void LateUpdate() {
        switch (mode) {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamrea = transform.position - Camera.main.transform.position; 
                transform.LookAt(transform.position + dirFromCamrea);
                break; 
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;  
                break;
            case Mode.CmeraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }


}
