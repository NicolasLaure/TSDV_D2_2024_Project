using UnityEngine;

public class EnemyAnimHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private readonly int _takeDamage = Animator.StringToHash("TakeDamage");
    private readonly int _die = Animator.StringToHash("Die");

    public void PlayTakeDamage()
    {
        animator.SetTrigger(_takeDamage);
    }

    public void PlayDie()
    {
        animator.SetTrigger(_die);
    }
}