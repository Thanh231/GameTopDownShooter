using System;

public class ActorStats : Stats
{
    public float hp;
    public float damage;
    public float moveSpeed;
    public float knockBackForce;
    public float knockBackTime;
    public float invicibleTime;
    public override bool IsMaxLevel()
    {
        return false;
    }

    public override void Load()
    {
        
    }

    public override void Save()
    {
       
    }

    public override void Upgrade(Action onSuccess = null, Action onFailed = null)
    {
        
    }
}
