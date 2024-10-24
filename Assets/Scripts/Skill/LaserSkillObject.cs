using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSkillObject : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 200f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            Destroy(gameObject);
        }
    }
}
