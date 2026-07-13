using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[Serializable]
public class Bot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    NavMeshAgent _navAgent;
    Fence _currentFence;

    [SerializeField] GameObject _hoverGameObject;
    [SerializeField] GameObject _selectedGameObject;

    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    public virtual void Task()
    {
        Debug.Log("Default Implementation");
    }

    public void SendBotToPosition(Vector3 position)
    {
        _navAgent.SetDestination(position);
        if (_currentFence != null)
        {
            _currentFence.SetCurrentBot(null);
            _currentFence = null;
        }
    }

    public void SendBotToFence(Fence fence)
    {
        if (fence.FenceIsOccuppiedByBot())
        {
            Debug.Log("Fence occupied cant send here");
            return;
        }

        if (_currentFence != null)
        {
            _currentFence.SetCurrentBot(null);
        }

        _currentFence = fence;
        _currentFence.SetCurrentBot(this);
        _navAgent.SetDestination(fence.GetPlayerBotTransform().position);
    }


    public void EnableSelectedVisual()
    {
        _selectedGameObject.SetActive(true);
    }
    public void DisableSelectedVisual()
    {
        _selectedGameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverGameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverGameObject.SetActive(false);
    }

    public Fence GetCurrentFence()
    {
        return _currentFence;
    }
}
