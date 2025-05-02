using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField][Range(0f, 50f)] int poolSize = 5;
    [SerializeField][Range(0.1f, 30f)] float spawnTimer = 1f;

    GameObject[] pool;

    void Awake () {
        PopulatePool();
    }

    void Start() {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool () {
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++) {
            pool[i] = Instantiate(EnemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectsInPool() {
        for (int i= 0; i < pool.Length; i++) {
            if (pool[i].activeInHierarchy == false) {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy() {
        while(true) {
            EnableObjectsInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
