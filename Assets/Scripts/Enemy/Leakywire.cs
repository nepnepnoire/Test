using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leakywire : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        health = 100;
        return ;
    }

    // Update is called once per frame
    public override void Update()
    {
        return;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player") 
        {
            collision.gameObject.SetActive(false);  
        }
    }
}
