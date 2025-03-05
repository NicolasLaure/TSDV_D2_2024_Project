using UnityEngine;

public class ProximityMineCollider : MonoBehaviour
{
    [SerializeField] private ProximityMine mineBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        mineBehaviour.StartExplosionSequence(other.gameObject);
    }
}