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
                Movement.AddMovementInput(1);
            }
            else
            {
                Movement.AddMovementInput(-1);
            }
        }
        else
        {
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
            target = GetFrontHitResult();
            if(target)
            {
                return false;
            }
            else
            {
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
        var result = Physics2D.OverlapCircleAll(attackTransform.position, attackRange, whatIsTarget);
        if(result.Length > 0)
        {
            foreach(var res in result)
            {
                var teamObj = res.GetComponent<TeamObject>();
                if(teamObj.GetTeam() != Team)
                {
                    return teamObj;
                }
            }
        }
        target = null;
        return null;
    }
}
