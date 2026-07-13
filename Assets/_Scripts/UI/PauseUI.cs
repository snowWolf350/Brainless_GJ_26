using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Button _continueButton;

    private void Awake()
    {
        _continueButton.onClick.AddListener(() =>
        {
            GameManager.Instance.SetGameState(GameManager.GameState.Playing);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
        Hide();
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= Instance_OnGameStateChanged;
    }
    private void Instance_OnGameStateChanged(object sender, GameManager.OnGameStateEventArgs e)
    {
        if (e.setState == GameManager.GameState.Paused)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }
}
