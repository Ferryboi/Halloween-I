using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IFlashable[] oFlashables = other.GetComponentsInParent<IFlashable>();

        for(int i = 0; i < oFlashables.Length; i++)
        {
            oFlashables[i].OnFlashStart();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IFlashable[] oFlashables = other.GetComponentsInParent<IFlashable>();

        for (int i = 0; i < oFlashables.Length; i++)
        {
            oFlashables[i].OnFlashEnd();
        }
    }
}
