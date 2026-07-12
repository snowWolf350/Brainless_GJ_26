using UnityEngine;

public class BuilderTrigger : MonoBehaviour
{
    Fence _parentFence;

    private void Start()
    {
        _parentFence = transform.parent.GetComponent<Fence>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Builder builder))
        {
            _parentFence.BuilderEntered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out Builder builder))
        {
            _parentFence.BuilderExited();
        }
    }
}
