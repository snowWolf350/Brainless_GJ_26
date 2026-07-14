using TMPro;
using UnityEngine;

public class PlayDamageEffect : MonoBehaviour
{
    Animator _animator;


    [SerializeField] TextMeshProUGUI _TmPro;

    float _effectTimer;
    float _effectTimerMax = 1f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _effectTimer += Time.deltaTime;

        if (_effectTimer > _effectTimerMax)
        {
            gameObject.SetActive(false);
            _effectTimer = 0;
        }
    }
    public void PlayTextEffect(int damage)
    {
        gameObject.SetActive(true);
        _TmPro.text = damage.ToString();
        _animator.Play("damage",0,0);
    }
}
