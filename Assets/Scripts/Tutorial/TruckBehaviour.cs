using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckBehaviour : MonoBehaviour
{
    public void EnableCollider()
    {
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Loader.ChangeScene(2);
    }
}
