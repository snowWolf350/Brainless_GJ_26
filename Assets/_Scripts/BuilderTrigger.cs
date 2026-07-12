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
        if (other.transform.TryGetComponent(out Bot bot))
        {
            _parentFence.BuilderEntered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out Bot bot))
        {
            _parentFence.BuilderExited();
        }
    }
}
