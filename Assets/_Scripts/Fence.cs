using UnityEngine;

public class Fence : MonoBehaviour
{
    Builder _currentBuilder;

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
}
