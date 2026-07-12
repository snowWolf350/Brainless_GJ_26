using UnityEngine;
public class Enemy : MonoBehaviour, IHasProgress
{
    Fence _targetFence;

    enum EnemyState
    {
        walking,
        attacking
    }

    EnemyState _currentEnemyState;

    Transform _fenceTransform;

    float _attackTimer;
    float _attackTimerMax = 4;
    float _moveSpeed = 0.5f;
    

    int _maxDamage = 20;
    int _minDamage = 10;

    Health _enemyHealth;

    public event System.EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    private void Awake()
    {
        _enemyHealth = new Health(100);
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
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                ProgressNormalized = _enemyHealth.GetHealthNormalized()
            });

            Destroy(bullet.gameObject);
        }
    }
    public void SetFence(Fence fence)
    {
        _targetFence = fence;
        _fenceTransform = _targetFence.GetEnemyTaregetTransform();
    }
}
