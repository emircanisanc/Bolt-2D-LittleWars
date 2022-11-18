using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CHealth))]
public abstract class TeamObject : MonoBehaviour, IDamageable
{
    [SerializeField] protected ETeam Team;
    [SerializeField] protected CHealth _Chealth;
    [SerializeField] protected AnimatorController _AnimatorController;

    public abstract float ApplyDamage(float damage);

    public float GetHealth()
    {
        return _Chealth.GetCurrentHealth();
    }

    public ETeam GetTeam()
    {
        return Team;
    }

    protected abstract void OnDeath();
}
