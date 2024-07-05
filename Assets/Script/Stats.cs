using System;

public abstract class Stats 
{
    public abstract void Save();
    public abstract void Load();
    public abstract void Upgrade(Action onSuccess = null, Action onFailed = null);
    public abstract bool IsMaxLevel();
}