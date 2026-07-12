using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fence : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IHasProgress
{
    Health _fenceHealth;

    Builder _currentBuilder;

    [SerializeField] GameObject _hoverGameObject;
    [SerializeField] Transform _playerBotTransform;
    [SerializeField] Transform _enemySpawnTransform;
    [SerializeField] Transform _enemyTargetTranform;

    bool _fenceIsTargeted;
    bool _builderIsInTrigger;

    float _fixAmount = 10;
    float _fixTimer;
    float _fixTimerMax = 2;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    private void Awake()
    {
        _fenceHealth = new Health(100);
    }
    private void Update()
    {
        if (_builderIsInTrigger == false) return;

        if (_fenceHealth.GetHealthNormalized() == 1) return;

        //fence health is low and builder is inside the trigger

        _fixTimer += Time.deltaTime;
        if (_fixTimer > _fixTimerMax)
        {
            _fixTimer = 0;
            _fenceHealth.Heal(_fixAmount);

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                ProgressNormalized = _fenceHealth.GetHealthNormalized()
            });
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverGameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverGameObject.SetActive(false);
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
        _builderIsInTrigger = true;
    }
    public void BuilderExited()
    {
        _builderIsInTrigger = false;
        _fixTimer = 0;
    }

    #region|___SETTERS___|
    public void SetCurrentBuilder(Builder builder)
    {
        _currentBuilder = builder;
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
    public bool FenceIsTargetedByEnemy()
    {
        return _fenceIsTargeted;
    }

    public bool FenceIsOccuppiedByBot()
    {
        if (_currentBuilder != null)
        {
            return true;
        }
        return false;
    }
    #endregion
}
