using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string ghoulIntroduction;

    public GameObject moodBar;
    public GameObject hintSign;
    public GameObject DialogueBox;
    public GameObject VictoryScreen;
    public GameObject DefeatScreen;
    public TextMeshProUGUI dialogueText;

    public movement player;
    public Moodbar mood;

    public List<Ghost> ghostList = new List<Ghost>();
    public List<Ghoul> ghoulList = new List<Ghoul>();
    public Ghoul currentGhoul;

    public bool intro = true;

    public UnityEvent displayHints;
    public UnityEvent displayCorpse;
    public UnityEvent gameLost;

    private void Start()
    {
        ghostList.Clear();
        ghoulList.Clear();
        moodBar.SetActive(false);
        hintSign.SetActive(false);
        VictoryScreen.SetActive(false);
        DefeatScreen.SetActive(false);

        ghostList.Add(new Ghost("Angry robust old man, always has a bottle in his hand. He has a rosacea, a loud voice, and smells strong. He coughs a lot. Worked as a miner for many years, until his health made it impossible for him to continue working. Neighbors say that sometimes he can become very violent towards his wife. Died of alcoholism.", 0));
        ghostList.Add(new Ghost("A baby. Died right after her birth.", 1));
        ghostList.Add(new Ghost("Young man in his thirties. Pale and thin. He used to work as a clown in a circus. Died on the stage because of a severe accident during performance. He brought a lot of laughters to kids and families. He used to sit in silence for a long time in the dressing room, and sighs.", 2));
        ghostList.Add(new Ghost("Two twin sisters. Around 10 years' old. Bodies curled up strangely because of their genetic disease. Died of their disease. Their family are too poor to offer both of them a proper medical treatment. They both refused to be the only one who gets an opportunity to be cured. They died hand in hand.", 3));
        ghostList.Add(new Ghost("Old woman, slightly fat. Sharp eyes, tender smile. After inheriting her husband's estate at an early age, she never gets married again and becomes a very successful businesswoman. People used to gossip about her a lot, saying all kinds of mean things, until she builds schools, hospitals for the whole region. Lived a long and peaceful life in her old age.",4));
        ghostList.Add(new Ghost("Teenage boy, has beautiful eyes, very kind character. He has two big buck teeth. Super smart in his class in a private school for elites, won the Mathematical Olympiad. Sadly got bullied by classmates because of his looking. Died of suicide.",5));
        ghostList.Add(new Ghost("A male game developper. Passionate. Curses at bugs every 15 minutes. Long hair, long fingers curled in a strange way. Protruding eyes. Made a world record for the longest time without going out of the apartment when he is alive.  Buried with his computer. Died of having too much junk food and not enough sleep.",6));
        ghostList.Add(new Ghost("A drag queen, around 40 years' old. Very sensitive and caring, yet has a hot-tempered personality. Stopped talking with his family for many years. Travelled around the world for performances. Died because he got choked by a huge olive during a party. He was in his fanciest clothes when he was sent to the hospital, surrounded by his loving friends.",7));
        ghostList.Add(new Ghost("A lottery winner, female first generation immigrant, around 80 years old. Strict mother at home, best bargainer at the market. Always wears the shabbiest clothes, saves every penny for her daughter's education. Died of age. She argued in anger with her family till the last day of her life.",8));

        GhoulHint A = new GhoulHint("Don't be afraid of being eaten, love will accompany you.", new int[] {1,3,7});
        GhoulHint B = new GhoulHint("Rage is my inner beast.", new int[] { 0, 6, 8 });
        GhoulHint C = new GhoulHint("Do you believe in the power of faith?", new int[] {4,6,7 });
        GhoulHint D = new GhoulHint("Look, I am so blue.", new int[] {2,5,6});
        GhoulHint E = new GhoulHint("I forget my umbrella everytime when it rains.", new int[] {1,2,7});
        GhoulHint F = new GhoulHint("My back hurts a lot these days, I think I am getting old.", new int[] { 0, 4, 8 });
        GhoulHint G = new GhoulHint("You may not believe, but most ghouls die young.", new int[] {1,3,5});
        GhoulHint H = new GhoulHint("I am a grown-up now, but sometimes I wonder, how does it feels to be...a female ghoul?", new int[] {4,7,8});
        GhoulHint I = new GhoulHint("To have a big brain is not always a blessing...but it's still nice to have it.", new int[] {4,5,6});
        GhoulHint J = new GhoulHint("La la la, la la la!", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });

        ghoulList.Add(new Ghoul(new GhoulHint[] {  B,F,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  A,E,G,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  D,E,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  A,G,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  C,F,H,I,G,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  D,G,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  B,C,D,I,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  A,C,E,H,J  }));
        ghoulList.Add(new Ghoul(new GhoulHint[] {  B,F,H,J  }));

        ghoulIntroduction = "Socrates once said : Know thyself. Who I am ? What exactly do I need ? \nI overturn the graves, looking for the meaning of life. Otherwise I can only eat other people's corpses to fill the void in my mind. \n\nI will give you some hints, bring me the corpse that fits my tastes so that I may not eat you.";
        DialogueBox.SetActive(true);
        dialogueText.text = ghoulIntroduction;

        currentGhoul = ghoulList[UnityEngine.Random.Range(0,ghoulList.Count-1)];


        displayHints.AddListener(DisplayHints);
        displayCorpse.AddListener(DisplayCorpse);
        gameLost.AddListener(LostGame);

    }


    private void Update()
    {
        if (intro && player.InteractPressed)
        {
            intro = false;
            DialogueBox.SetActive(false);
            player.canMove = true;
            hintSign.SetActive(true);
        }
        if (player.corpseRange && player.InteractPressed)
        {
            DialogueBox.SetActive(false);
            player.corpseRange = false;
            int match = currentGhoul.GhostMatch(player.currentCorpse);
            print(match);
            Moodbar mb = GetComponent<Moodbar>();
            if (match >= 3)
            {
                player.enabled = false;
                moodBar.SetActive(false);
                DialogueBox.SetActive(false);
                VictoryScreen.SetActive(true);
            }
            else if (match == 2)
            {
                mood.UpdateMoodbar(10);
            }
            else if (match == 1)
            {
                mood.UpdateMoodbar(30);
            }
            else
            {
                mood.UpdateMoodbar(40);
            }
        }

        if (!DialogueBox.activeSelf) { moodBar.SetActive(true); }
        else { moodBar.SetActive(false); }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    void DisplayHints()
    {
        DialogueBox.SetActive(true);
        dialogueText.text = string.Empty;
        foreach(var ghoulhint in currentGhoul.ghoulHints)
        {
            if (dialogueText.text == string.Empty)
                dialogueText.text = ghoulhint.hint;
            else
                dialogueText.text += "\n" + ghoulhint.hint;
        }
    }
    void DisplayCorpse()
    {
        DialogueBox.SetActive(true);
        dialogueText.text = string.Empty;
        dialogueText.text = ghostList[player.currentCorpse].summary;
    }
    void LostGame()
    {
        player.enabled = false;
        moodBar.SetActive(false);
        DialogueBox.SetActive(false);
        DefeatScreen.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
public class Ghost
{
    public string summary;
    public int id;
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
