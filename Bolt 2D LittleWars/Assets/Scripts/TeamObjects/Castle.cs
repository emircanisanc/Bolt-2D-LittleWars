using UnityEngine;

public class Castle : TeamObject, IUpgradeable
{
    [SerializeField] private Transform SpawnTransform;
    [SerializeField] private float canSpawnCheckRadius;
    [SerializeField] private LayerMask whatIsSoldier;
    [SerializeField] private GameObject destroyParticlePrefab;
    [SerializeField] private float extraHealthOnUpgrade;

    [SerializeField] private Wizard defender;

    public static int blueSoldiersCount;
    public static int redSoldiersCount;

    void Start()
    {
        UIManager.Instance.SetCastleHealth(Team, _Chealth.GetCurrentHealth());
    }

    public void SpawnSoldier(Soldier soldier)
    {
        if(CanSpawn())
        {
            if(Team == ETeam.BlueTeam)
            {
                blueSoldiersCount++;
            }
            else
            {
                redSoldiersCount++;
            }
            Instantiate(soldier.gameObject, SpawnTransform.position, soldier.transform.rotation).GetComponent<Soldier>();
        }
    }

    public bool CanSpawn()
    {
        return !Physics2D.OverlapCircle(SpawnTransform.position, canSpawnCheckRadius, whatIsSoldier);
    }

    public override float ApplyDamage(float damage)
    {
        if(_Chealth.GetCurrentHealth() > 0)
        {
            _AnimatorController.PlayHurtAnim();
            var now = _Chealth.ReduceHealth(damage);
            UIManager.Instance.SetCastleHealth(Team, now);
            if(_Chealth.GetCurrentHealth() <= 0)
            {
                _AnimatorController.PlayDeathAnim();
                OnDeath();
            }
            
            return now;
        }
        return 0;
    }

    public GameObject Upgrade()
    {
        defender = defender.Upgrade().GetComponent<Wizard>();
        _Chealth.UpgradeHealth(extraHealthOnUpgrade, true);
        UIManager.Instance.SetCastleHealth(Team, _Chealth.GetCurrentHealth());
        return gameObject;
    }

    protected override void OnDeath()
    {
        enabled = false;
        defender.ApplyDamage(99);
        GameManager.Instance.OnGameEnd(Team);
        Instantiate(destroyParticlePrefab, transform.position, destroyParticlePrefab.transform.rotation);
    }

}
