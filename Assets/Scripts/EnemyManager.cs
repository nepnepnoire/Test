using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public GameObject[] enemyPrefabs; // 不同类型敌人的预制件数组
    private List<(GameObject prefab, Vector3 position)> enemyData = new List<(GameObject, Vector3)>(); // 存储敌人的数据
    public List<Enemy> newEnemies = new List<Enemy>();
    void Start()
    {
        LoadEnemies();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadEnemies();
        }
    }

    private void LoadEnemies()
    {
        enemies.Clear(); // 清空现有的敌人列表
        enemyData.Clear(); // 清空之前记录的数据

        Enemy[] existingEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in existingEnemies)
        {
            GameObject prefab = enemy.gameObject; // 获取敌人的预制件
            Vector3 position = enemy.transform.position; // 获取敌人的位置
            enemies.Add(enemy);
            enemyData.Add((prefab, position)); // 存储敌人的类型和位置
        }
        foreach (Enemy enemy in enemies)
        {
            enemy.gameObject.SetActive(false); // 将现有敌人设置为不活动
        }
        newEnemies.Clear();
        // 根据记录的数据重新生成敌人
        foreach (var data in enemyData)
        {
            GameObject newEnemy = Instantiate(data.prefab, data.position, Quaternion.identity);
            newEnemy.SetActive(true);
            newEnemies.Add(newEnemy.GetComponent<Enemy>());
        }
    }
    public void DestroyEnemy(Enemy enemy,float time)
    {
        if (newEnemies.Contains(enemy))
        {
            newEnemies.Remove(enemy);
            Destroy(enemy.gameObject,time);
        }
        else
        {
            Debug.LogWarning("尝试销毁不存在的敌人");
        }
    }
    public void ReloadEnemies()
    {
        foreach (Enemy enemy in newEnemies)
        {
            Destroy(enemy.gameObject); 
        }
        newEnemies.Clear ();
        // 根据记录的数据重新生成敌人
        foreach (var data in enemyData)
        {
            GameObject newEnemy = Instantiate(data.prefab, data.position, Quaternion.identity);
            newEnemy.SetActive(true);
            newEnemies.Add(newEnemy.GetComponent<Enemy>());
        }
    }
}