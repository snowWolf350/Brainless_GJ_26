using UnityEngine;
using UnityEngine.EventSystems;

public class Fence : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Health _fenceHealth;

    Builder _currentBuilder;

    [SerializeField] GameObject _hoverGameObject;
    [SerializeField] Transform _enemySpawnTransform;
    [SerializeField] Transform _enemyTargetTranform;

    bool _fenceIsTargeted;

    private void Awake()
    {
        _fenceHealth = new Health(100);
    }
    public void SetCurrentBuilder(Builder builder)
    {
        _currentBuilder = builder;
    }

    public bool FenceIsOccuppied()
    {
        if (_currentBuilder != null)
        {
            return true;
        }
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverGameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverGameObject.SetActive(false);
    }

    public Transform GetEnemySpawnTransform()
    {
        return _enemySpawnTransform;
    }
    public Transform GetEnemyTaregetTransform()
    {
        return _enemyTargetTranform;
    }

    public void TakeDamage(float damage)
    {
        _fenceHealth.TakeDamage(damage);
    }
    public void SetFenceIsTargeted(bool value)
    {
        _fenceIsTargeted = value;
    }

    public bool FenceIsTargeted()
    {
        return _fenceIsTargeted;
    }
}
