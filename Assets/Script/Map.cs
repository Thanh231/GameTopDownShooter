using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform playerSpawnPos;
    public Transform[] enemySpawnPosition;

    public Transform GetEnemyPos
    {
        get
        {
            if(enemySpawnPosition == null || enemySpawnPosition.Length < 0) return null;

            int randomPos = Random.Range(0, enemySpawnPosition.Length);
            return enemySpawnPosition[randomPos];
        }
    }
}
