using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Soldier
{
    [SerializeField] private float moveRange;
    [SerializeField] private GameObject arrowPrefab;

    protected override void Attack()
    {
        if(Time.timeSinceLevelLoad >= resetHitTime && target.GetTeam() != Team)
        {
            resetAttackTime = Time.timeSinceLevelLoad + attackAnimTime;
            nextAttackTime = Time.timeSinceLevelLoad + attackRate;
            _AnimatorController.PlayAttackAnim();
            if(onAttackClip)
            {
                AudioSource.PlayClipAtPoint(onAttackClip, transform.position);
            }
            var relativePos = target.transform.position - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var arrow = Instantiate(arrowPrefab, attackTransform.position, Quaternion.Euler(new Vector3(0, angle, 0)));
            arrow.GetComponent<Arrow>().CnsArrow(Team, attackDamage);
        }
    }

    protected override TeamObject GetFrontHitResult()
    {
        var result = Physics2D.LinecastAll(attackTransform.position,  attackTransform.position + transform.right * moveRange * (toRight? 1: -1), whatIsTarget);
        foreach(var hit in result)
        {
            var teamobj = hit.collider.GetComponent<TeamObject>();
            if(teamobj != this && teamobj.enabled)
            {
                return teamobj;
            }
        }
        return null;
    }

    protected override bool CheckForward()
    {
        target = GetFrontHitResult();
        if(target)
        {
            if(target.GetTeam() != Team)
            {
                return false;
            }
            else if(target.TryGetComponent<Castle>(out var castle))
            {
                var result = Physics2D.LinecastAll(attackTransform.position,  attackTransform.position + transform.right * attackRange * (toRight? 1: -1), whatIsTarget);
                foreach(var hit in result)
                {
                    var teamobj = hit.collider.GetComponent<TeamObject>();
                    if(teamobj != this && teamobj.enabled && teamobj.GetTeam() != Team)
                    {
                        target =  teamobj;
                        return false;
                    }
                }
                target = null;
            }
            else
            {
                var result = Physics2D.LinecastAll(attackTransform.position,  attackTransform.position + transform.right * attackRange * (toRight? 1: -1), whatIsTarget);
                foreach(var hit in result)
                {
                    var teamobj = hit.collider.GetComponent<TeamObject>();
                    if(teamobj != this && teamobj.enabled && teamobj.GetTeam() != Team)
                    {
                        target =  teamobj;
                        return false;
                    }
                }
                return false;
            }
        }
        else
        {
            var result = Physics2D.LinecastAll(attackTransform.position,  attackTransform.position + transform.right * attackRange * (toRight? 1: -1), whatIsTarget);
            foreach(var hit in result)
            {
                var teamobj = hit.collider.GetComponent<TeamObject>();
                if(teamobj != this && teamobj.enabled && teamobj.GetTeam() != Team)
                {
                    target =  teamobj;
                    return false;
                }
            }
            target = null;
        }
        return CanMove;
    }
}
