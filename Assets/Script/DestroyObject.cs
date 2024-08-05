using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] float destroyAfter;
    void Start()
    {
        Destroy(gameObject,destroyAfter);
    }
}
