using System.Collections.Generic;
using UnityEngine;


public class Wizard : Soldier, IUpgradeable
{
    [SerializeField] protected GameObject magicParticlePrefab;

    [SerializeField] private Wizard UpgradePrefab;

    protected override void Attack()
    {
        if(!_Chealth.IsDead())
        {
            if(Time.timeSinceLevelLoad >= resetHitTime)
            {
                resetAttackTime = Time.timeSinceLevelLoad + attackAnimTime;
                nextAttackTime = Time.timeSinceLevelLoad + attackRate;
                _AnimatorController.PlayAttackAnim();
                AudioSource.PlayClipAtPoint(onAttackClip, transform.position);
                var result = Physics2D.OverlapCircleAll(attackTransform.position, attackRange, whatIsTarget);
                foreach(var hit in result)
                {
                    var dgmble = hit.GetComponent<TeamObject>();
                    if(dgmble != null && dgmble.enabled && dgmble != this)
                    {
                        var particle = Instantiate(magicParticlePrefab, dgmble.transform.position, magicParticlePrefab.transform.rotation);
                        Destroy(particle, 2f);
                        dgmble.ApplyDamage(attackDamage);
                        return;   
                    }
                }
            }
        }
        
    }

    public GameObject Upgrade()
    {
        Destroy(gameObject);
        var newWizard = Instantiate(UpgradePrefab.gameObject, transform.position, UpgradePrefab.transform.rotation);
        return newWizard.gameObject;
    }
}
