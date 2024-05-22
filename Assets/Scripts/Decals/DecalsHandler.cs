using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalsHandler : MonoBehaviour
{
    [SerializeField] private GameObject bulletHole;
    private List<GameObject> decals = new List<GameObject>();
    public void SpawnBulletHole(Transform collider, Vector3 position, Vector3 hitNormal)
    {
        GameObject decal = Instantiate(bulletHole, position, Quaternion.identity);
        decal.transform.parent = collider;
        decal.transform.forward = hitNormal;
        decal.transform.position += decal.transform.forward * 0.1f;
        decals.Add(decal);
    }

    public void ClearDecals()
    {
        foreach (GameObject decal in decals)
        {
            Destroy(decal);
        }
    }
}
