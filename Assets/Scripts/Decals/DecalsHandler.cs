using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalsHandler : MonoBehaviour
{
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private LayerMask ignoredLayers;

    private List<GameObject> _decals = new List<GameObject>();

    public void SpawnBulletHole(Transform collider, Vector3 position, Vector3 hitNormal)
    {
        if ((ignoredLayers & 1 << collider.gameObject.layer) != 0)
            return;

        GameObject decal = Instantiate(bulletHole, position, Quaternion.identity);
        decal.transform.forward = hitNormal;
        decal.transform.position = position + decal.transform.forward * 0.1f;
        decal.transform.parent = this.transform;
        _decals.Add(decal);
    }

    public void ClearDecals()
    {
        foreach (GameObject decal in _decals)
        {
            Destroy(decal);
        }
    }

    public void RemoveDecal(GameObject decal)
    {
        if (_decals.Contains(decal))
        {
            _decals.Remove(decal);
            Destroy(decal);
        }
    }
}