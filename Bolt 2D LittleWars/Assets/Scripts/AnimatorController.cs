using UnityEngine;


public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayWalkAnim()
    {
        animator.SetTrigger("doMove");
    }
    public void PlayDeathAnim()
    {
        animator.SetTrigger("doDie");
    }
    public void PlayHurtAnim()
    {
        animator.SetTrigger("doHit");
    }
    public void PlayIdleAnim()
    {
        animator.SetTrigger("doStop");
    }
    public void PlayAttackAnim()
    {
        animator.SetTrigger("doAttack");
    }
}
