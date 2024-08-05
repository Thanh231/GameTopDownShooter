using UnityEngine;

[System.Serializable]
public class CollectableItem
{
    [Range(0f, 1f)] public float spawnRate;
    public int amount;
    public Collectable collectablePrefabs;
}
public class CollectableManager : Singleton<CollectableManager>
{
    [SerializeField] private CollectableItem[] items;
    
    public void Spawn(Vector3 spawnPosition)
    {
        if (items == null || items.Length <= 0) return;

        float spawnChecking = Random.value;
        foreach(var item in items) 
        {
            if (item == null || item.spawnRate < spawnChecking) continue;
            CreateCollectable(spawnPosition, item);
        }
    }

    private void CreateCollectable(Vector3 spawnPosition, CollectableItem item)
    {
        // if(item == null) return;
        for(int i = 0; i < item.amount; i++) 
        {
            Instantiate(item.collectablePrefabs,spawnPosition, Quaternion.identity);
        }
    }
}
