using UnityEngine;

public class Shooter : Bot
{
    float _shootTimer;
    float _shootTimerMax = 1.5f;
    float _shootForce = 40;

    [SerializeField] Transform _shootTransform;
    [SerializeField] GameObject _bullet;
    private void Update()
    {
        if (GetCurrentFence() == null) return;

        transform.rotation = Quaternion.LookRotation(GetCurrentFence().transform.position, Vector3.up);
    }
    public override void Task()
    {
        if (GetCurrentFence().FenceIsTargetedByEnemy() == false) return;

        _shootTimer += Time.deltaTime;

        if (_shootTimer > _shootTimerMax)
        {
            _shootTimer = 0;
            Vector3 aimDir = GetCurrentFence().GetAimDirection();
            GameObject bullet = Instantiate(_bullet, 
                GetCurrentFence().GetShootTransform().position,
                Quaternion.LookRotation(aimDir,Vector3.up));

            bullet.GetComponent<Rigidbody>().linearVelocity = aimDir * _shootForce;
        }
    }
}
