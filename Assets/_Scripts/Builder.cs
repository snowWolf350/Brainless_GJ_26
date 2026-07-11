using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
public class Builder : MonoBehaviour, IPointerEnterHandler
{
    NavMeshAgent _navAgent;
    Fence _currentFence;

    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    public void SendBuilderToFence(Fence fence)
    {
        if(fence.FenceIsOccuppied())
        {
            Debug.Log("Fence occupied cant send here");
            return;
        }

        _currentFence = fence;
        _currentFence.SetCurrentBuilder(this);
        _navAgent.SetDestination(fence.transform.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("entered");
    }
}
