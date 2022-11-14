using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CMovement))]
public class Soldier : TeamObject
{
    [SerializeField] private CMovement Movement;
    [SerializeField] private bool toRight;

    [SerializeField] protected bool CanMove = true;

    [SerializeField] private float attackRange;
    [SerializeField] private float attackRate;
    [SerializeField] private float attackDamage;

    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] protected Transform attackTransform;

    protected TeamObject target;

    void Update()
    {
        if(CheckForward())
        {
            if(toRight)
            {
                Movement.AddMovementInput(transform.right, 1);
            }
            else
            {
                Movement.AddMovementInput(transform.right, -1);
            }
        }
        else
        {
            Movement.AddMovementInput(Vector3.zero, 0);
            if(target)
            {
                TryAttack();
            }
        }
    }

    protected void TryAttack()
    {

    }

    protected void Attack()
    {

    }

    protected bool CheckForward()
    {
        if(CanMove)
        {
            var front = GetFrontHitResult();
            if(front)
            {
                if(front.GetTeam() != Team)
                {
                    target = front;
                    return false;
                }
                target = null;
                return false;
            }
            else
            {
                target = null;
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    private TeamObject GetFrontHitResult()
    {
        var result = Physics2D.OverlapCircle(attackTransform.position, attackRange, whatIsTarget);
        if(result)
        {
            var teamObj = result.GetComponent<TeamObject>();
            return teamObj;
        }
        target = null;
        return null;
    }
}
