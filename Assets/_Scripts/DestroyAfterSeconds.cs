using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float destroyTime = 1;

    private void Start()
    {
        Destroy(gameObject,destroyTime);
    }
}
