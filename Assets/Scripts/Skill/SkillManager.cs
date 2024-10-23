using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> skillPrefabs;
    public List<Skill> skills;
    void Start()
    {
        skills = new List<Skill>();
        foreach (var prefab in skillPrefabs)
        {
            Skill skillInstance = Instantiate(prefab).GetComponent<Skill>();
            skills.Add(skillInstance);
        }
    }

    public void UseSkill(int skillIndex)
    {
        if (skillIndex < 0 || skillIndex >= skills.Count) return;
        skills[skillIndex].Activate();
    }

// Update is called once per frame
}
