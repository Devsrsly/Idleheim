using UnityEngine;

public class UnitRuntime
{
    public UnitStats stats;
    public int hp;
    private float nextAttackTime;

    public UnitRuntime(UnitStats stats)
    {
        this.stats = stats;
        hp = stats.maxHp;
        nextAttackTime = 0f;
    }

    public bool IsDead => hp <= 0;

    public bool CanAttack(float timeNow) => timeNow >= nextAttackTime && !IsDead;

    public int DealDamage(float timeNow)
    {
        nextAttackTime = timeNow + stats.attackInterval;
        return stats.attackDamage;
    }

    public void TakeDamage(int dmg)
    {
        hp = Mathf.Max(0, hp - dmg);
    }
}