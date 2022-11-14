using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CHealth))]
public abstract class TeamObject : MonoBehaviour, IDamageable
{
    [SerializeField] protected ETeam Team;

    protected CHealth _Chealth;

    public float ApplyDamage(float damage)
    {
        if(_Chealth.ReduceHealth(damage) <= 0)
        {
            OnDeath();
        }
        return _Chealth.GetCurrentHealth();
    }

    public float GetHealth()
    {
        return _Chealth.GetCurrentHealth();
    }

    public ETeam GetTeam()
    {
        return Team;
    }

    void Awake()
    {
        _Chealth = GetComponent<CHealth>();
    }

    protected void OnDeath()
    {

    }
}