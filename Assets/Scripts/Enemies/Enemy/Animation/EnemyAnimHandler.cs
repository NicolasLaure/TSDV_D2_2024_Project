using UnityEngine;

public class EnemyAnimHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private readonly int takeDamage = Animator.StringToHash("TakeDamage");
    private readonly int die = Animator.StringToHash("Die");

    public void PlayTakeDamage()
    {
        animator.SetTrigger(takeDamage);
    }

    public void PlayDie()
    {
        animator.SetTrigger(die);
    }
}