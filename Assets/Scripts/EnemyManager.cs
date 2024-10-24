using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public GameObject[] enemyPrefabs; // ��ͬ���͵��˵�Ԥ�Ƽ�����
    private List<(GameObject prefab, Vector3 position)> enemyData = new List<(GameObject, Vector3)>(); // �洢���˵�����
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
        enemies.Clear(); // ������еĵ����б�
        enemyData.Clear(); // ���֮ǰ��¼������

        Enemy[] existingEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in existingEnemies)
        {
            GameObject prefab = enemy.gameObject; // ��ȡ���˵�Ԥ�Ƽ�
            Vector3 position = enemy.transform.position; // ��ȡ���˵�λ��
            enemies.Add(enemy);
            enemyData.Add((prefab, position)); // �洢���˵����ͺ�λ��
        }
        foreach (Enemy enemy in enemies)
        {
            enemy.gameObject.SetActive(false); // �����е�������Ϊ���
        }
        newEnemies.Clear();
        // ���ݼ�¼�������������ɵ���
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
            Debug.LogWarning("�������ٲ����ڵĵ���");
        }
    }
    public void ReloadEnemies()
    {
        foreach (Enemy enemy in newEnemies)
        {
            Destroy(enemy.gameObject); 
        }
        newEnemies.Clear ();
        // ���ݼ�¼�������������ɵ���
        foreach (var data in enemyData)
        {
            GameObject newEnemy = Instantiate(data.prefab, data.position, Quaternion.identity);
            newEnemy.SetActive(true);
            newEnemies.Add(newEnemy.GetComponent<Enemy>());
        }
    }
}