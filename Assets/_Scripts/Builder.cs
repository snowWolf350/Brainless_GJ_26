using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
public class Builder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    NavMeshAgent _navAgent;
    Fence _currentFence;

    [SerializeField] GameObject _hoverGameObject;

    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    public void SendBuilderToFence(Fence fence)
    {
        if(fence.FenceIsOccuppiedByBot())
        {
            Debug.Log("Fence occupied cant send here");
            return;
        }

        if (_currentFence != null)
        {
            _currentFence.SetCurrentBuilder(null);
        }

        _currentFence = fence;
        _currentFence.SetCurrentBuilder(this);
        _navAgent.SetDestination(fence.GetPlayerBotTransform().position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverGameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverGameObject.SetActive(false);
    }
}
