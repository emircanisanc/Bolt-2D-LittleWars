using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    
    private Rigidbody2D rg2D;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private AudioClip hitClip;
    private float damage;
    private ETeam Team;

    void Awake()
    {
        rg2D = GetComponent<Rigidbody2D>();
    }

    public void CnsArrow(ETeam team, float damage)
    {
        Team = team;
        this.damage = damage;
    }

    void Update()
    {
        if(damage != 0)
        {
            rg2D.velocity = transform.right * speedMultiplier;
        }   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<TeamObject>(out var tmobj))
        {
            if(tmobj.GetTeam() != Team)
            {
                AudioSource.PlayClipAtPoint(hitClip, transform.position);
                other.GetComponent<IDamageable>().ApplyDamage(damage);
                Destroy(gameObject);
            }
        }    
    }
}
