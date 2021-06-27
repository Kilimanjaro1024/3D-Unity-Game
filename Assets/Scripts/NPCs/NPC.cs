using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC 
{
    public int id;
    public string name;
    public Dictionary<string, int> stats = new Dictionary<string, int>();
    public bool alive;
    public int health;

    public NPC(int id, string name, bool alive, Dictionary<string, int> stats){
        this.id = id;
        this.name = name;
        this.alive = alive;
        
        this.stats = stats;
    }
    
    public NPC(NPC npc){
        this.id = npc.id;
        this.name = npc.name;
        this.alive = npc.alive;
        
        this.stats = npc.stats;
    }
}
