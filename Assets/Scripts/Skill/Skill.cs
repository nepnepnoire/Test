using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    // Start is called before the first frame update
    public string skillName;
    public virtual void Activate()
    {
        Debug.Log("Skill0");
    }
}
