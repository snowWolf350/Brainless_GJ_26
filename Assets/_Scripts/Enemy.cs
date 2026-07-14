using TMPro;
using UnityEngine;
public class Enemy : MonoBehaviour, IHasProgress
{
    Fence _targetFence;

    [SerializeField] GameObject _damageParticles;
    [SerializeField] GameObject _damageText;
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
    

    int _maxDamage = 15;
    int _minDamage = 10;

    Health _enemyHealth;

    public event System.EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    private void Awake()
    {
        _enemyHealth = new Health(100);
    }

    private void Start()
    {
        _enemyHealth.onDeath += _enemyHealth_onDeath;
    }
    private void OnDestroy()
    {
        _enemyHealth.onDeath -= _enemyHealth_onDeath;
    }
    private void _enemyHealth_onDeath(object sender, System.EventArgs e)
    {
        _targetFence.SetFenceIsTargeted(false);
        Destroy(gameObject);
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
            int damage = bullet.GetRandomDamageAmount();

            Instantiate(_damageParticles, collision.GetContact(0).point, Quaternion.identity);


            _damageText.GetComponent<PlayDamageEffect>().PlayTextEffect(damage);

            _enemyHealth.TakeDamage(damage);
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
