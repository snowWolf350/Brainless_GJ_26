using UnityEngine;

public class Bullet : MonoBehaviour
{
    int _damageAmountMin = 20;
    int _damageAmountMax = 40;
    int _damageAmount;
    public int GetRandomDamageAmount()
    {
        _damageAmount = Random.Range(_damageAmountMin, _damageAmountMax+1);
        return _damageAmount;
    }
}
