using UnityEngine;
using UnityEngine.EventSystems;
public class Builder : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("entered");
    }
}
