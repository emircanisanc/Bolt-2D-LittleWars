using UnityEngine;

public class Castle : TeamObject
{
    [SerializeField] private Transform SpawnTransform;
    [SerializeField] private float canSpawnCheckRadius;
    [SerializeField] private LayerMask whatIsSoldier;

    public void SpawnSoldier(Soldier soldier)
    {
        if(CanSpawn())
        {
            Instantiate(soldier.gameObject, SpawnTransform.position, soldier.transform.rotation);
        }
    }

    public bool CanSpawn()
    {
        return !Physics2D.OverlapCircle(SpawnTransform.position, canSpawnCheckRadius, whatIsSoldier);
    }

}
