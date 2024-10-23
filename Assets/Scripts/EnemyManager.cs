using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Enemypool enemypool;
    public List<Enemy> enemies = new List<Enemy>();
    public int initialCount;

    void Start()
    {
        SpawnEnemies();
        enemypool.DestroyAllEnemy();
    }

    void Update()
    {

    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < initialCount; i++)
        {
            var enemyObj = enemypool.GetEnemy();
            enemies.Add(enemyObj.GetComponent<Enemy>());
            //enemyObj.transform.position = /* …Ë÷√≥ı ºŒª÷√ */;
        }
    }
}
