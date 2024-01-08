using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> EnemyPrefabs = new List<GameObject>();

    private float _timer;

    private GameManager _gameManager;


    [SerializeField]
    private float _spawnRate;

    [SerializeField]
    private float spawnBoundary;

    void Start()
    {
        _gameManager = GetComponent<GameManager>();
    }
    void Update()
    {
        if (_gameManager.stateHandler.CurrentState != GameStateHandler.GameState.InPlay) return;


        _timer += 1 * Time.deltaTime;
        if(_timer > _spawnRate)
        {
            if (_gameManager.player == null) return;

            Vector3 pPos = _gameManager.player.transform.position;
            Vector3 spawnPos = new Vector3(pPos.x + Random.Range(-spawnBoundary, spawnBoundary), pPos.y + Random.Range(-spawnBoundary, spawnBoundary), 0);
            GameObject enemy = Instantiate(EnemyPrefabs[0], spawnPos, Quaternion.Euler(0, 0, 0));
            enemy.GetComponent<Enemy>().target = _gameManager.player;
            _gameManager.EntityList.Add(enemy);
            _timer = 0;
        }
    }
}
