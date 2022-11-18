using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CMovement))]
public class Soldier : TeamObject
{
    [SerializeField] private CMovement Movement;
    [SerializeField] protected bool toRight;
    [SerializeField] protected float hitAnimTime;
    [SerializeField] protected SpriteRenderer _SpriteRenderer;
    protected float resetHitTime; 

    [SerializeField] protected bool CanMove = true;

    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackRate;
    [SerializeField] protected float attackDamage;
    protected float nextAttackTime;
    [SerializeField] protected float attackAnimTime;
    protected float resetAttackTime;

    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] protected Transform attackTransform;

    [SerializeField] private ParticleSystem dustParticle;
    [SerializeField] protected AudioClip onAttackClip;

    protected TeamObject target;

    void Update()
    {
        if(!_Chealth.IsDead())
        {
            if(CheckForward())
            {
                if(!Movement.IsMoving())
                {
                    dustParticle.gameObject.SetActive(true);
                    dustParticle.Play();
                    _AnimatorController.PlayWalkAnim();
                }
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
                if(!Movement.IsMoving())
                {
                    dustParticle.gameObject.SetActive(false);
                    dustParticle.Pause();
                }
                Movement.AddMovementInput(0);
                if(target)
                {
                    TryAttack();
                }
                else
                {
                    _AnimatorController.PlayIdleAnim();
                }
            }
        }
    }

    protected void TryAttack()
    {
        if(Time.timeSinceLevelLoad >= nextAttackTime)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        if(Time.timeSinceLevelLoad >= resetHitTime)
        {
            resetAttackTime = Time.timeSinceLevelLoad + attackAnimTime;
            _AnimatorController.PlayAttackAnim();
            AudioSource.PlayClipAtPoint(onAttackClip, transform.position);
            var result = Physics2D.OverlapCircleAll(attackTransform.position, attackRange, whatIsTarget);
            List<TeamObject> targets = new List<TeamObject>();
            foreach(var hit in result)
            {
                var dgmble = hit.GetComponent<TeamObject>();
                if(dgmble != null && dgmble.enabled && dgmble != this)
                {
                    targets.Add(dgmble);
                }
            }
            if(targets.Count > 1)
            {
                targets.RemoveAll(IsCastle);
            }
            if(targets.Count > 0)
            {
                targets[0].ApplyDamage(attackDamage);
                nextAttackTime = Time.timeSinceLevelLoad + attackRate;
            }
            
        }
        
    }

    private static bool IsCastle(TeamObject item)
    {
        return item.TryGetComponent<Castle>(out var castle);
    }

    protected virtual bool CheckForward()
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
                target = null;
            }
            else
            {
                target = null;
                return false;
            }
        }
        return CanMove;
    }

    protected virtual TeamObject GetFrontHitResult()
    {
        var result = Physics2D.OverlapCircleAll(attackTransform.position, attackRange, whatIsTarget);
        foreach(var hit in result)
        {
            var teamobj = hit.GetComponent<TeamObject>();
            if(teamobj != this && teamobj.enabled)
            {
                return teamobj;
            }
        }
        return null;
    }

    protected override void OnDeath()
    {
        CanMove = false;
        _AnimatorController.PlayDeathAnim();
        this.enabled = false;
        if(TryGetComponent<Rigidbody2D>(out var rg2d))
        {
            Movement.AddMovementInput(0);
            rg2d.gravityScale = 0;
        }
        if(TryGetComponent<BoxCollider2D>(out var box2d))
        {
            box2d.enabled = false;
        }
        _SpriteRenderer.sortingOrder = 0;
        if(Team == ETeam.BlueTeam)
        {
            Castle.blueSoldiersCount--;
        }
        else
        {
            Castle.redSoldiersCount--;
        }
        Destroy(gameObject, 2f);
        
    }

    public override float ApplyDamage(float damage)
    {
        resetHitTime = Time.timeSinceLevelLoad + hitAnimTime;
        if(resetAttackTime <= Time.timeSinceLevelLoad)
        {
            _AnimatorController.PlayHurtAnim();
        }
        
        if(_Chealth.ReduceHealth(damage) <= 0)
        {
            OnDeath();
        }
        return _Chealth.GetCurrentHealth();
    }


}
