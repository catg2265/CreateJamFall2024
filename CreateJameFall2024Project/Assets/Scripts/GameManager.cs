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

        ghostList.Add(new Ghost("Angry robust old man, always has a bottle in his hand. \r\nHe has a rosacea, a loud voice, and smells strong. He coughs a lot.\r\nWorked as a miner for many years, until his health made it impossible for him to continue working. \r\nNeighbors say that sometimes he can become very violent towards his wife. \r\nDied of alcoholism.", false, false, false, false, true, false, false));
        //If a characteristic equals 1 call that true, if it equals 0 call that false
        ghostList.Add(new Ghost("Insert text here inside citation", false, false, false, false, true, false, false));
        //Please add the rest of the ghosts

        GhoulHint A = new GhoulHint("Don't be afraid of being eaten, love will accompany you.", new int[] {2,4,8});
        GhoulHint B = new GhoulHint("Rage is my inner beast.", new int[] { 1, 7, 9 });
        GhoulHint C = new GhoulHint("Do you believe in the power of faith?", new int[] {5,7,8 });
        GhoulHint D = new GhoulHint("Look, I am so blue.", new int[] {3,6,7});
        GhoulHint E = new GhoulHint("I forget my umbrella everytime when it rains.", new int[] {2,3,8});
        GhoulHint F = new GhoulHint("My back hurts a lot these days, I think I am getting old.", new int[] { 1, 5, 9 });
        GhoulHint G = new GhoulHint("You may not believe, but most ghouls die young.", new int[] {2,4,6});
        GhoulHint H = new GhoulHint("I am a grown-up now, but sometimes I wonder, how does it feels to be...a female ghoul?", new int[] {5,8,9});
        GhoulHint I = new GhoulHint("To have a big brain is not always a blessing...but it's still nice to have it.", new int[] {5,6,7});
        GhoulHint J = new GhoulHint("La la la, la la la!", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

        ghoulList.Add(new Ghoul(new GhoulHint[] {  B,F,J  }));
        //Please add the rest of the "Ghouls"



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
    public Ghoul(GhoulHint[] ghoulHints)
    {
        this.ghoulHints = ghoulHints;
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
