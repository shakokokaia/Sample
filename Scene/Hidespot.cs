using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidespot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //VisionTarget visionTarget = other.GetComponent<VisionTarget>();
        //if(visionTarget != null && !visionTarget.detected)
        //{
            //visionTarget.SetVisible(false);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        //VisionTarget visionTarget = other.GetComponent<VisionTarget>();
        //if (visionTarget != null)
        //{
            //visionTarget.SetVisible(true);
        //}
    }
}
