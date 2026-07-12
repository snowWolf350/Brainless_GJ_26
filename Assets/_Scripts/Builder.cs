
using UnityEngine;

public class Builder : Bot
{

    float _fixAmount = 10;
    float _fixTimer;
    float _fixTimerMax = 2;


    public override void Task()
    {
        if (GetCurrentFence().GetFenceHealth().GetHealthNormalized() == 1) return;
        _fixTimer += Time.deltaTime;
        if (_fixTimer > _fixTimerMax)
        {
            _fixTimer = 0;
            
            GetCurrentFence().HealHealth(_fixAmount);
        }
    }
}
