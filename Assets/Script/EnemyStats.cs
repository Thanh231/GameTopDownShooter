using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Enemy Stats",menuName = "Create Enemy Stats")]
public class EnemyStats : ActorStats
{
    [Header("XP bonus")]
    public float minXp;
    public float maxXp;

    [Header("Level Up")]
    public float hpUp;
    public float damageUp;
}
