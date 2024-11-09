using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string ghoulIntroduction;

    public List<Ghost> ghostList = new List<Ghost>();
    public List<Ghoul> ghoulList = new List<Ghoul>();
    public Ghoul currentGhoul;

    public bool activateGhostMatch = false;

    private void Start()
    {
        ghostList.Clear();
        ghoulList.Clear();

        ghostList.Add(new Ghost("Angry robust old man, always has a bottle in his hand.\r\nHe has a rosacea, a loud voice, and smells strong. He coughs a lot.\r\nWorked as a miner for many years, until his health made it impossible for him to continue working.\r\nNeighbors say that sometimes he can become very violent towards his wife.\r\nDied of alcoholism.", 0));
        ghostList.Add(new Ghost("A baby. Died right after her birth.", 1));
        ghostList.Add(new Ghost("Young man in his thirties. Pale and thin.\r\nHe used to work as a clown in a circus.\r\nDied on the stage because of a severe accident during performance.\r\nHe brought a lot of laughters to kids and families.\r\nHe used to sit in silence for a long time in the dressing room, and sighs.", 2));
        ghostList.Add(new Ghost("Two twin sisters. Around 10 years' old.\r\nBodies curled up strangely because of their genetic disease.\r\nDied of their disease.\r\nTheir family are too poor to offer both of them a proper medical treatment.\r\nThey both refused to be the only one who gets an opportunity to be cured.\r\nThey died hand in hand.", 3));
        ghostList.Add(new Ghost("Old woman, slightly fat. Sharp eyes, tender smile.\r\nAfter inheriting her husband's estate at an early age,\r\nshe never gets married again and becomes a very successful businesswoman.\r\nPeople used to gossip about her a lot, saying all kinds of mean things,\r\nuntil she builds schools, hospitals for the whole region.\r\nLived a long and peaceful life in her old age.",4));
        ghostList.Add(new Ghost(@"Teenage boy, has beautiful eyes, very kind character.He has two big buck teeth.
        Super smart in his class in a private school for elites, won the Mathematical Olympiad.
        Sadly got bullied by classmates because of his looking.
        Died of suicide.",5));
        ghostList.Add(new Ghost(@"A male game developper. Passionate. Curses at bugs every 15 minutes.
        Long hair, long fingers curled in a strange way. Protruding eyes.
        Made a world record for the longest time without going out of the apartment when he is alive. 
        Buried with his computer.
        Died of having too much junk food and not enough sleep.",6));
        ghostList.Add(new Ghost(@"A drag queen, around 40 years' old. Very sensitive and caring, yet has a hot-tempered personality.
        Stopped talking with his family for many years. Travelled around the world for performances.
        Died because he got choked by a huge olive during a party.
        He was in his fanciest clothes when he was sent to the hospital, surrounded by his loving friends.",7));
        ghostList.Add(new Ghost(@"A lottery winner, female first generation immigrant, around 80 years' old.
        Strict mother at home, best bargainer at the market.
        Always wears the shabbiest clothes, saves every penny for her daughter's education.
        Died of age. She argued in anger with her family till the last day of her life.",8));

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
        ghoulList.Add(new Ghoul(new GhoulHint[] {  A,E,G,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  D,E,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  A,G,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  C,F,H,I,G,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  D,G,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  B,C,D,I,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  A,C,E,H,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  B,F,H,J  }));


        currentGhoul = ghoulList[UnityEngine.Random.Range(0,ghoulList.Count-1)];
    }


    private void Update()
    {
        if (activateGhostMatch)
        {

            activateGhostMatch = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
public class Ghost
{
    string summary;
    int id;
    public Ghost(string summary, int id)
    {
        this.summary = summary;
        this.id = id;
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
