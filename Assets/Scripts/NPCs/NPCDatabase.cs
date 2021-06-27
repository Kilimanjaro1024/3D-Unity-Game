using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDatabase : MonoBehaviour
{
    public List<NPC> npcs = new List<NPC>();
    
    void Awake(){
        BuildDatabase();
    }

    void BuildDatabase(){
        npcs = new List<NPC>(){
            new NPC(0, "Jim The NPC", true, 
            new Dictionary<string, int>
            {
                {"Health", 20}
            })
        };
    }
}
