using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string ghoulIntroduction;

    public List<Ghost> ghostList = new List<Ghost>();
    public List<Ghoul> ghoulList = new List<Ghoul>();


    private void Start()
    {
        ghostList.Clear();
        ghoulList.Clear();

        ghostList.Add(new Ghost("Angry robust old man, always has a bottle in his hand. \r\nHe has a rosacea, a loud voice, and smells strong. He coughs a lot.\r\nWorked as a miner for many years, until his health made it impossible for him to continue working. \r\nNeighbors say that sometimes he can become very violent towards his wife. \r\nDied of alcoholism.", false,false,false,false,true,false,false));


        //ghoulList.Add(new Ghoul(new GhoulHint("",{ }),new GhoulHint("",{ }),new GhoulHint("",{ })));
    
    
    
    
    
    }


    private void Update()
    {
        
    }

}
public class Ghost
{
    string summary;
    bool intelligence;
    bool love;
    bool luck;
    bool beauty;
    bool power;
    bool wealth;
    bool health;
    public Ghost(string summary, bool intelligence, bool love, bool luck, bool beauty, bool power, bool wealth, bool health)
    {
        this.summary = summary;
        this.intelligence = intelligence;
        this.love = love;
        this.luck = luck;
        this.beauty = beauty;
        this.power = power;
        this.wealth = wealth;
        this.health = health;
    }
}
public class Ghoul
{
    public GhoulHint[] ghoulHints;
    public Ghoul(GhoulHint hint1, GhoulHint hint2, GhoulHint hint3)
    {
        ghoulHints = new GhoulHint[3] { hint1, hint2, hint3};
    }
    public int GhostMatch(int GhostID)
    {
        int totalMatch = 0;

        foreach (GhoulHint ghoulHint in ghoulHints)
        {
            foreach (int id in ghoulHint.ghostMatchIds)
            {
                if (id == GhostID)
                {
                    totalMatch++; 
                }
            }
        }

        return totalMatch;
    }
}
public class GhoulHint
{
    public string hint;
    public int[] ghostMatchIds;
    public GhoulHint(string hint, int[] ghostMatchIds)
    {
        this.hint = hint;
        this.ghostMatchIds = ghostMatchIds;
    }
}
