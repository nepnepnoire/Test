using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missly : MonoBehaviour
{
    public GameObject misslyPrefab; // 导弹预制体
    public Transform target; // 目标
    public float moveSpeed = 100f;

    private void Start()
    {
        GameObject player = GameObject.Find("Player"); // 假设目标对象名为"Player"
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if ((target.position.x - transform.position.x) * transform.localScale.x < 0)
        {
            Vector3 nowScale = transform.localScale;
            nowScale.x *= -1;
            transform.localScale = nowScale;
            //Debug.Log("Opp");
        }
        transform.position = Vector3.MoveTowards(transform.position, target.position,moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player") 
        {
            Destroy(gameObject);
        }
    }
}
