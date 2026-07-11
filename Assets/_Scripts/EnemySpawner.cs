using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _enemyGamObject;

    [SerializeField] List<Fence> _fenceList;
    List<Fence> _availibleFencesList;

    float _spawnTimer;
    float _spawnTimerMax = 4;

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if(_spawnTimer > _spawnTimerMax )
        {
            _spawnTimer = 0;

            _availibleFencesList.Clear();

            foreach (Fence _fence in _fenceList)
            {
                if (_fence.FenceIsOccuppied())
                {
                    continue;
                }
                _availibleFencesList.Add(_fence);
            }

            if (_availibleFencesList.Count == 0) return;

            Fence fence;


            fence = _availibleFencesList[Random.Range(0, _fenceList.Count)];

            fence.SetFenceIsTargeted(true);
            

            GameObject enemy = Instantiate(_enemyGamObject, fence.GetEnemySpawnTransform().position, Quaternion.identity);

            enemy.GetComponent<Enemy>().SetFence(fence);
        }
    }
}
