using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New BulletConfig", menuName = "ScriptableObjects/Weapons/BulletConfig")]
public class BulletConfigSO : ScriptableObject
{
    public Mesh mesh;
    public Material mat;
    public GameObject trailObject;
    public int layerIndex;

    public float bulletForce;
    public float lifeTime;
}
