using UnityEngine;

public class Shooter : Bot
{
    float _shootTimer;
    float _shootTimerMax = 5;
    float _shootForce = 40;

    [SerializeField] Transform _shootTransform;
    [SerializeField] GameObject _bullet;

    public override void Task()
    {
        if (GetCurrentFence().FenceIsTargetedByEnemy() == false) return;

        _shootTimer += Time.deltaTime;

        if (_shootTimer > _shootTimerMax)
        {
            _shootTimer = 0;
            GameObject bullet = Instantiate(_bullet, _shootTransform);
            bullet.GetComponent<Rigidbody>().linearVelocity = _shootTransform.forward * _shootForce;
        }
    }
}
