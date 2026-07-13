using UnityEngine;

public class Bullet : MonoBehaviour
{
    int _damageAmountMin = 30;
    int _damageAmountMax = 40;
    int _damageAmount;
    public int GetDamageAmount()
    {
        _damageAmount = Random.Range(_damageAmountMin, _damageAmountMax+1);
        return _damageAmount;
    }
}
