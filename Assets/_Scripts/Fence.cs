using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fence : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IHasProgress
{
    Health _fenceHealth;

    Bot _currentBot;

    [SerializeField] GameObject _hoverGameObject;
    [SerializeField] Transform _playerBotTransform;
    [SerializeField] Transform _enemySpawnTransform;
    [SerializeField] Transform _enemyTargetTranform;
    [SerializeField] Transform _shootTranform;

    bool _fenceIsTargeted;
    bool _botIsInTrigger;

    float _yShootOffset;


    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    private void Awake()
    {
        _fenceHealth = new Health(100);
    }
    private void Start()
    {
        _yShootOffset = _shootTranform.position.y;
    }
    private void Update()
    {
        if (_botIsInTrigger == false) return;
        if (_currentBot == null) return;
        //fence health is low and builder is inside the trigger

        _currentBot.Task();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverGameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverGameObject.SetActive(false);
    }
    public void HealHealth(float healAmount)
    {
        _fenceHealth.Heal(healAmount);

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            ProgressNormalized = _fenceHealth.GetHealthNormalized()
        });
    }

    public void TakeDamage(float damage)
    {
        _fenceHealth.TakeDamage(damage);

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            ProgressNormalized = _fenceHealth.GetHealthNormalized()
        });
    }

    
    public void BuilderEntered()
    {
        _botIsInTrigger = true;
    }
    public void BuilderExited()
    {
        _botIsInTrigger = false;
    }

    #region|___SETTERS___|
    public void SetCurrentBot(Bot bot)
    {
        _currentBot = bot;
    }

    public void SetFenceIsTargeted(bool value)
    {
        _fenceIsTargeted = value;
    }

    #endregion

    #region|___GETTERS___|
    public Transform GetPlayerBotTransform()
    {
        return _playerBotTransform;
    }
    public Transform GetEnemySpawnTransform()
    {
        return _enemySpawnTransform;
    }
    public Transform GetEnemyTaregetTransform()
    {
        return _enemyTargetTranform;
    }
    public Transform GetShootTransform()
    {
        return _shootTranform;
    }
    public Vector3 GetAimDirection()
    {
        Vector3 aimDir;

        aimDir = ((_enemySpawnTransform.position + new Vector3(0, _yShootOffset, 0))- _shootTranform.position).normalized;

        Debug.DrawLine(
    _shootTranform.position,
    _shootTranform.position + aimDir * 10f,
    Color.red,
    99
);
        return aimDir;
    }
    public bool FenceIsTargetedByEnemy()
    {
        return _fenceIsTargeted;
    }

    public bool FenceIsOccuppiedByBot()
    {
        if (_currentBot != null)
        {
            return true;
        }
        return false;
    }

    public Health GetFenceHealth()
    {
        return _fenceHealth;
    }
    #endregion
}
