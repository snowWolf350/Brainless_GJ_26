using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Fence _targetFence;

    enum EnemyState
    {
        walking,
        attacking
    }

    EnemyState _currentEnemyState;

    Transform _fenceTransform;
    Vector3 SpawnPos;

    float _attackTimer;
    float _attackTimerMax = 4;
    float _moveSpeed = 2;
    

    int _maxDamage = 20;
    int _minDamage = 10;

    Health _enemyHealth;
    private void Awake()
    {
        _enemyHealth = new Health(100);
    }
    private void Start()
    {
        SpawnPos = transform.position;
    }

    private void Update()
    {
        switch (_currentEnemyState)
        {
            case EnemyState.walking:

                transform.position = Vector3.MoveTowards(transform.position,_fenceTransform.position,_moveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, _fenceTransform.position) < 0.1f)
                {
                    _currentEnemyState = EnemyState.attacking;
                }
                break;
            case EnemyState.attacking:
                _attackTimer += Time.deltaTime;

                if(_attackTimer > _attackTimerMax)
                {
                    _attackTimer = 0;
                    int damage = Random.Range(_minDamage, _maxDamage + 1);
                    _targetFence.TakeDamage(damage);
                }
                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Bullet bullet))
        {
            _enemyHealth.TakeDamage(bullet.GetDamageAmount());
        }
    }
    public void SetFence(Fence fence)
    {
        _targetFence = fence;
        _fenceTransform = _targetFence.GetEnemyTaregetTransform();
    }
}
