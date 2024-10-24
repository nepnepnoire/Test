using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHeart : MonoBehaviour
{
    public int id;
    public bool isPickedUp = false;

    public void Start()
    {
        isPickedUp = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("HIA");
        if (other.CompareTag("Player")) // 检查碰撞体是否是玩家
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();   
            if(playerController != null)
            {
                switch(id)
                {
                    case 1:
                        Debug.Log("1");
                        playerController.unlockAttach = true;
                        break;
                    case 2:
                        playerController.unlockGlide = true;
                        break;
                    case 3:
                        playerController.unlockHook = true;
                        break;
                }
            }
            isPickedUp = true;
            Destroy(gameObject); 
            
        }
    }
}
