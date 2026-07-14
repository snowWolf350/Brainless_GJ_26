using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _enemyGamObject;

    [SerializeField] List<Fence> _fenceList;
    List<Fence> _availibleFencesList;

    float _spawnTimer;
    float _spawnTimerMax = 8;
    float _spawnTimerMin = 6;

    float _spawnWaitTime = 2;

    private void Awake()
    {
        _availibleFencesList = new List<Fence>();
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if(_spawnTimer > _spawnWaitTime)
        {
            _spawnTimer = 0;
            _spawnWaitTime = Random.Range(_spawnTimerMin, _spawnTimerMax);
            _availibleFencesList.Clear();

            foreach (Fence _fence in _fenceList)
            {
                if (_fence.FenceIsTargetedByEnemy())
                {
                    continue;
                }
                _availibleFencesList.Add(_fence);
            }

            if (_availibleFencesList.Count == 0) return;

            Fence fence;

            if (_availibleFencesList.Count > 1)
                fence = _availibleFencesList[Random.Range(0, _availibleFencesList.Count)];
            else
                fence = _availibleFencesList[0];

            fence.SetFenceIsTargeted(true);
            

            GameObject enemy = Instantiate(_enemyGamObject, fence.GetEnemySpawnTransform().position, Quaternion.identity);

            enemy.GetComponent<Enemy>().SetFence(fence);
        }
    }
}
