using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemypool : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab;
    private List<GameObject> pool = new List<GameObject>();

 
    public GameObject GetEnemy()
    {
        foreach (var enemy in pool)
        {
            if (enemy.activeInHierarchy)
            {
                Debug.Log("enemy pool");
                enemy.SetActive(false);
                return enemy;
            }
        }
        var newEnemy = Instantiate(enemyPrefab);
        pool.Add(newEnemy);
        return newEnemy;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
    }
    public void DestroyAllEnemy()
    {
        foreach (var enemy in pool)
        {
            if (enemy.activeInHierarchy)
            {
                enemy.SetActive(false);
            }
        }
    }
}
