using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    IHasProgress BaseObject;
    [SerializeField] Image _fillImage;

    private void Start()
    {
        BaseObject = transform.parent.GetComponent<IHasProgress>();

        BaseObject.OnProgressChanged += BaseObject_OnProgressChanged;
        Hide();
    }

    private void BaseObject_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        if (e.ProgressNormalized == 1 || e.ProgressNormalized == 0)
        {
            Hide();
        }
        else
        {
            Show();
        }
            _fillImage.fillAmount = e.ProgressNormalized;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
