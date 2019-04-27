using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    private float typingSpeed = 0.02f;
    private string[] TutorialSceneSentences = new string[17];
    private string[] Scene1Sentences = new string[16];
    private string[] Scene1Names1 = new string[16];
    private string[] Scene2Sentences = new string[15];
    private string[] Scene2Names = new string[15];
    private string[] VillageSceneSentences = new string[7];

    private AudioClip[] TutorialAudioClips;
    private AudioClip[] VillageAudioClips;
    private AudioClip[] Scene1AudioClips;
    private AudioClip[] Scene2AudioClips;
    private AudioClip[] CreditsAudioClips;

    private int test = 0;
    private int index;
    private Scene currentScene;
    private Dunga DungaScript;
    private Bak BakScript;
    private EnemyMarauder MarauderScript;
    private PlayableDirector playable;
    private bool Paused = false;
    private GameObject fadeScreen;
    private GameObject tutorialRock;
    private GameObject tutorialBarrel;
    private AudioSource player;

    public BoxCollider arrowCollider;
    public GameObject dialogueBox;
    public GameObject gameManager;
    public GameObject continueButton;
    public GameObject trigger;
    public GameObject archerBuffbar;
    public GameObject tutorialIconPrefab;
    public Animator littleSpriteAnimator;
    public Animator animator;
    public TextMeshProUGUI narrativeText;
    public TextMeshProUGUI nameText;

    void Start()
    {
        player = GetComponent<AudioSource>();
        fadeScreen = GameObject.Find("BlackFade");

        if (GameObject.Find("Character") != null)
            DungaScript = GameObject.Find("Character").GetComponent<Dunga>();

        tutorialBarrel = GameObject.Find("barrelClosed");
        tutorialRock = GameObject.Find("Spawning_Stone");
        currentScene = SceneManager.GetActiveScene();
        index = 0;

        #region Tutorial Scene Sentences
        TutorialSceneSentences[0] = "Master! I'm here to help. You've been asleep for a long time. I'm Lys, your loyal assistant.";
        TutorialSceneSentences[1] = "You’re probably wondering about what happened and what you are, so I’ll explain.";
        TutorialSceneSentences[2] = "You are a lesser-god.  A half spirit who watches over the world and protects your \"Chosen.\"";
        TutorialSceneSentences[3] = "What?? It's him?";
        TutorialSceneSentences[4] = "\"Dunga\" ? He's an odd one isn't he.";
        TutorialSceneSentences[5] = "Anyways, it will take you a bit of time to recover your full power.";
        TutorialSceneSentences[6] = "Even with your limited ability you can swap health bars and interfere with incoming airborne attacks";
        TutorialSceneSentences[7] = "Click and drag one health bar onto another to switch the amount of health they have. Give it a try.";
        TutorialSceneSentences[8] = "Excellent. The health values have now been exchanged.";
        TutorialSceneSentences[9] = "You can do the exact same thing with buffs.  Just grab the icon and move it to Dunga’s health bar.";
        TutorialSceneSentences[10] = "Fantastic!  That buff will increase the attack speed of the corresponding character.  Watch out if you see it on an enemy.";
        TutorialSceneSentences[11] = "That arrow is our next problem. You can click on it to stop its momentum. It will fall to the ground without harming our friend.";
        TutorialSceneSentences[12] = "Very good. Now we can protect our... chosen.";
        TutorialSceneSentences[13] = "In ages past you could affect the world in great ways. For now, you will have to deal with a much diminished power.";
        TutorialSceneSentences[14] = "See those stones behind Dunga?  Try dragging them towards him.";
        TutorialSceneSentences[15] = "You can also interact with other items likes barrels or crates.  Try smashing one of them atop an enemy.";
        TutorialSceneSentences[16] = "I believe you are now ready.  As you regain your powers, I will guide you on how to use them.";


        #endregion

        #region Tutorial Audio Clips
        TutorialAudioClips = new AudioClip[] { (AudioClip)Resources.Load("Audio/Sprite/Tutorial/ImLys"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/IllExplain"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/YouAreALesserGod"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/ItsHim"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/HesAnOddOne"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/FullPower"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/EvenWithYourLimitedAbility"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/ClickAndDrag"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/Excellent"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/SameWithBuffs"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/ThatBuff"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/ThatArrow"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/VeryGood"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/InAgesPast"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/SeeThoseStones"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/YouCanAlsoInteract"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Tutorial/YouAreNowReady")
        };
        #endregion

        #region Credits Audio Clips
        CreditsAudioClips = new AudioClip[] { (AudioClip)Resources.Load("Audio/Dunga/DungaSingingShort"),
                                             (AudioClip)Resources.Load("Audio/Dunga/Dunga3TimesMatterOfFact"),
                                             (AudioClip)Resources.Load("Audio/Bak/Bak(Scene6)(Take2)SupposedtoMean"),
                                             (AudioClip)Resources.Load("Audio/Dunga/Dunga2TimesQuestion"),
                                             (AudioClip)Resources.Load("Audio/Bak/Bak(Scene6)(Take2)WhatsADeveloper"),
                                             (AudioClip)Resources.Load("Audio/Dunga/DungaShort"),
                                             (AudioClip)Resources.Load("Audio/Bak/Bak(Scene6)(Take2).GroupParticipation"),
                                             (AudioClip)Resources.Load("Audio/Dunga/DungaYes"),
                                             (AudioClip)Resources.Load("Audio/Bak/Bak(Scene6)(Take1)BEARD"),
                                             (AudioClip)Resources.Load("Audio/Barkeep/Yoohoo"),
                                             (AudioClip)Resources.Load("Audio/Bak/Bak(Scene6)(Take2)NOTFAIR"),
                                             (AudioClip)Resources.Load("Audio/Bak/Bak(Scene6)(Take2)YES"),
        };
        #endregion

        #region Scene 1 Sentences
        Scene1Sentences[0] = "It’s gone.  It’s all gone.";
        Scene1Sentences[1] = "Dunga…";
        Scene1Sentences[2] = "Every farm, every crop, every longhouse just burnt to the ground.";

        Scene1Sentences[3] = "At least the... barkeep is still alive.";
        Scene1Sentences[4] = "Oddly convenient...";
        Scene1Sentences[5] = "Dunga Dunga.";

        Scene1Sentences[6] = "When the raiders were chasing me, the rest of them attacked the village.";
        Scene1Sentences[7] = "I feel like such a coward.  I should’ve stood my ground just like you did in the forest.";
        Scene1Sentences[8] = "Wait.. they couldn't have gone far with everything they took.";
        Scene1Sentences[9] = "Dunga, it's their ship!";

        Scene1Sentences[10] = "You need to go reclaim what they've taken.  Our people deserve better than this!";
        Scene1Sentences[11] = "Dunga Dunga?";
        Scene1Sentences[12] = "Me? I would certainly come, but uhh... I need to...";
        Scene1Sentences[13] = "AID THE WOUNDED! Yea, yea that makes sense.  Now go, Dunga!";
        Scene1Sentences[14] = "Duuuuunnnnggggaaaaa!!!";
        Scene1Sentences[15] = "Give 'em Hel!  Or... or condemn them to Hellheim!  Yes something like that...";
        #endregion

        #region Scene 1 Names
        Scene1Names1[0] = "Bak";
        Scene1Names1[1] = "Dunga";
        Scene1Names1[2] = "Bak";
        Scene1Names1[3] = "Bak";
        Scene1Names1[4] = "Bak";
        Scene1Names1[5] = "Dunga";
        Scene1Names1[6] = "Bak";
        Scene1Names1[7] = "Bak";
        Scene1Names1[8] = "Bak";
        Scene1Names1[9] = "Bak";
        Scene1Names1[10] = "Bak";
        Scene1Names1[11] = "Dunga";
        Scene1Names1[12] = "Bak";
        Scene1Names1[13] = "Bak";
        Scene1Names1[14] = "Dunga";
        Scene1Names1[15] = "Bak";
        #endregion

        #region Scene 1 Audio Clips
        Scene1AudioClips = new AudioClip[] { (AudioClip)Resources.Load("Audio/Bak/OpeningScene/AllGone"),
                                             (AudioClip)Resources.Load("Audio/Dunga/DungaSurprised"),
                                             (AudioClip)Resources.Load("Audio/Bak/OpeningScene/EveryFarm"),
                                             (AudioClip)Resources.Load("Audio/Bak/OpeningScene/Barkeep"),
                                             (AudioClip)Resources.Load("Audio/Bak/OpeningScene/OddlyConvenient"),
                                             (AudioClip)Resources.Load("Audio/Dunga/DungaHeyy2"),
                                             (AudioClip)Resources.Load("Audio/Bak/OpeningScene/RaiderChasing"),
                                             (AudioClip)Resources.Load("Audio/Bak/OpeningScene/ForestGround"),
                                             (AudioClip)Resources.Load("Audio/Bak/OpeningScene/EverythingTheyTook"),
                                             (AudioClip)Resources.Load("Audio/Bak/OpeningScene/ItsTheirShip"),
                                             (AudioClip)Resources.Load("Audio/Bak/OpeningScene/Reclaim"),
                                             (AudioClip)Resources.Load("Audio/Dunga/Dunga2TimesQuestion"),
                                             (AudioClip)Resources.Load("Audio/Bak/OpeningScene/IWouldCome"),
                                             (AudioClip)Resources.Load("Audio/Bak/OpeningScene/AidTheWounded"),
                                             (AudioClip)Resources.Load("Audio/Dunga/DungaLongYell"),
                                             (AudioClip)Resources.Load("Audio/Bak/OpeningScene/Helheim")
        };
        #endregion

        #region Scene 2 Sentences
        Scene2Sentences[0] = "Dunga Dungaaa!!";
        Scene2Sentences[1] = "Why you trying to attack our ship!?  Our Chieftain won’t like that, won’t like that at all.  My head’s on the chopping block now, so I’ll \ntake you down with me!";
        Scene2Sentences[2] = "Dunga...";
        Scene2Sentences[3] = "Dunga Dunga Dunga.";
        Scene2Sentences[4] = "I have the eyes of Odin’s raven, so piercing an arrow through your skull at this range should be no trouble at all!";
        Scene2Sentences[5] = "Dunga Dunga, Dunga!";
        Scene2Sentences[6] = "What!? How dare you say that about my mother!";
        Scene2Sentences[7] = "You ever face a hoard of ravaging vikings?  Well here’s your first lesson: ROLL OVER AND DIE!";
        Scene2Sentences[8] = "I saw it one night, the shrine, the divine power that shields you now; it calls to those who are worthy, and it called to me.";
        Scene2Sentences[9] = "But why you?  Why does such an imbecile gain the powers of the divine protector?  What makes YOU WORTHY!!??  WHY AM I NOT THE CHOSEN ONE!!!???";
        Scene2Sentences[10] = "...Dunga.";
        Scene2Sentences[11] = "You...you stumbled upon it?  B–by some sort of accident?";
        Scene2Sentences[12] = "Dunga.";
        Scene2Sentences[13] = "I will end your reign of pitiful ignorance, once...and for all.";
        Scene2Sentences[14] = "That’s right, you’re not the only one with magic on their side.";
        #endregion

        #region Village Scene Sentences
        VillageSceneSentences[0] = "Welcome to the new village, master! The people have been rebuilding ever since you helped Dunga fight off the raiders.";                                        
        VillageSceneSentences[1] = "This is our new home. Dunga has spread the word of your powers and the people have dedicated a shrine to you in the centre of town. ";
        VillageSceneSentences[2] = "It's not much right now, but as you help Dunga along his adventures, he will gain more followers for you.";
        VillageSceneSentences[3] = "This will increase your power significantly, allowing you to unlock more of your long-forgotten abilities!";
        VillageSceneSentences[4] = "Here in the village, Dunga can visit the Tavern and Blacksmith to trade for supplies and gear up for his next quest. You might have to help him find his way around, though...";
        VillageSceneSentences[5] = "Yeah he's not the brightest lad, is he?";
        VillageSceneSentences[6] = "Once you think Dunga is ready for his next adventure, go ahead and visit the map and choose his destination. His fate is in your hands, master!";
        #endregion

        #region Village Audio Clips
        VillageAudioClips = new AudioClip[] { (AudioClip)Resources.Load("Audio/Sprite/Village/Sprite_Village_Line1"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Village/Sprite_Village_Line2"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Village/Sprite_Village_Line3"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Village/Sprite_Village_Line4"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Village/Sprite_Village_Line5"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Village/Sprite_Village_Line6"),
                                             (AudioClip)Resources.Load("Audio/Sprite/Village/Sprite_Village_Line7"),
        };
        #endregion

        #region Scene 2 Names
        Scene2Names[0] = "Dunga";
        Scene2Names[1] = "Marauder";
        Scene2Names[2] = "Dunga";
        Scene2Names[3] = "Dunga";
        Scene2Names[4] = "Archer";
        Scene2Names[5] = "Dunga";
        Scene2Names[6] = "Marauder";
        Scene2Names[7] = "Marauder";
        Scene2Names[8] = "Chieftain";
        Scene2Names[9] = "Chieftain";
        Scene2Names[10] = "Dunga";
        Scene2Names[11] = "Chieftain";
        Scene2Names[12] = "Dunga";
        Scene2Names[13] = "Chieftain";
        Scene2Names[14] = "Chieftain";
        #endregion

        #region Scene 2 Audio Clips
        Scene2AudioClips = new AudioClip[] { (AudioClip)Resources.Load("Audio/Dunga/Dunga2Times2"),
                                             (AudioClip)Resources.Load("Audio/Marauder/GameScene/MarauderStage1"),
                                             (AudioClip)Resources.Load("Audio/Dunga/DungaPant"),
                                             (AudioClip)Resources.Load("Audio/Dunga/Dunga3TimesDefensive"),
                                             (AudioClip)Resources.Load("Audio/Archer/GameScene/Archer(Take2)"),
                                             (AudioClip)Resources.Load("Audio/Dunga/Dunga3TimesMatterOfFact"),
                                             (AudioClip)Resources.Load("Audio/Marauder/GameScene/MarauderStage3"),
                                             (AudioClip)Resources.Load("Audio/Marauder/GameScene/RaiderStage4Take1"),
                                             (AudioClip)Resources.Load("Audio/Chieftain/GameScene/Chieftain_Line1"),
                                             (AudioClip)Resources.Load("Audio/Chieftain/GameScene/Chieftain_Line7"),
                                             (AudioClip)Resources.Load("Audio/Dunga/Dunga1Time2"),
                                             (AudioClip)Resources.Load("Audio/Chieftain/GameScene/Chieftain_Line2"),
                                             (AudioClip)Resources.Load("Audio/Dunga/DungaQuestioning"),
                                             (AudioClip)Resources.Load("Audio/Chieftain/GameScene/Chieftain_Line3"),
                                             (AudioClip)Resources.Load("Audio/Chieftain/GameScene/Chieftain_Line4"),
                                             
        };
        #endregion


        if (currentScene.name == "TutorialScene")
        {
            gameManager = GameObject.Find("GameManager");
            Invoke("StartTutorialNarrative", 2.5f);
            GameObject.Find("Lys").GetComponent<Animator>().Play("TutorialEntrance");
            DeactivateClickables();
        }

        if (currentScene.name == "OpeningScene")
        {
            playable = GameObject.Find("Timeline").GetComponent<PlayableDirector>();
            BakScript = GameObject.Find("Bak").GetComponent<Bak>();
            Invoke("StartNarrative", 8.0f);
            Invoke("ToggleTimeline", 8.0f);
            ChangeSpeed(5.0f);
        }

        if (currentScene.name == "GameScene")
        {
            playable = GameObject.Find("Timeline").GetComponent<PlayableDirector>();
            gameManager = GameObject.Find("GameManager");
            gameManager.GetComponent<PauseGame>().ChangeStageEnemySpeed(2.5f);
            Invoke("StartGameNarrative", 4.5f);
            Invoke("ToggleTimeline", 6f);
            DungaScript.moveSpeed = 7.5f;
        }

        if (currentScene.name == "Village")
        {
            gameManager = GameObject.Find("GameManager");
            Invoke("StartVillageNarrative", 4f);
            GameObject.Find("Lys").GetComponent<Animator>().Play("VillageEntrance");
        }

        if (currentScene.name == "NewCredits")
        {
            Invoke("StartCreditsAudio", 0f);
        }

        Invoke("FadeOnOff", 2.0f);
    }

    void Update()
    {
        Debug.Log(index);
        if (currentScene.name == "TutorialScene")
        {
            if (narrativeText.text == TutorialSceneSentences[index])
            {
                continueButton.SetActive(true);
            }
        }

        else if (currentScene.name == "OpeningScene")
        {
            if (narrativeText.text == Scene1Sentences[index])
            {
                continueButton.SetActive(true);
            }
        }

        else if (currentScene.name == "GameScene")
        {
            if (narrativeText.text == Scene2Sentences[index])
            {
                continueButton.SetActive(true);
            }
        }

        else if (currentScene.name == "Village")
        {
            if (narrativeText.text == VillageSceneSentences[index])
            {
                continueButton.SetActive(true);
            }
        }

    }

    public IEnumerator TutorialSceneNarrative()
    {
        dialogueBox.SetActive(true);
        animator.SetBool("IsOpen", true);
        foreach (char letter in TutorialSceneSentences[index].ToCharArray())
        {
            narrativeText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }


    public IEnumerator Scene1Narrative()
    {
        dialogueBox.SetActive(true);
        animator.SetBool("IsOpen", true);
        foreach (char letter in Scene1Sentences[index].ToCharArray())
        {
            narrativeText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public IEnumerator Scene1NameChange()
    {
        foreach (char letter in Scene1Names1[index].ToCharArray())
        {
            nameText.text += letter;
            
            player.clip = Scene1AudioClips[index];
            player.Play();
            yield return new WaitForSeconds(0);
        }
    }

    public IEnumerator TutorialDialogueAudio()
    {
        AudioSource player = GetComponent<AudioSource>();
        player.clip = TutorialAudioClips[index];
        player.Play();
        yield return new WaitForSeconds(0);
    }

    public IEnumerator VillageDialogueAudio()
    {
        AudioSource player = GetComponent<AudioSource>();
        player.clip = VillageAudioClips[index];
        player.Play();
        yield return new WaitForSeconds(0);
    }

    public IEnumerator CreditsDialogueAudio()
    {
        AudioSource player = GetComponent<AudioSource>();
        player.clip = CreditsAudioClips[index];
        player.Play();
        yield return new WaitForSeconds(0);
    }

    public IEnumerator Scene1DialogueAudio()
    {
        AudioSource player = GetComponent<AudioSource>();
        player.clip = Scene1AudioClips[index];
        player.Play();
        yield return new WaitForSeconds(0);
    }

    public IEnumerator Scene2DialogueAudio()
    {
        AudioSource player = GetComponent<AudioSource>();
        player.clip = Scene2AudioClips[index];
        player.Play();
        yield return new WaitForSeconds(0);
    }


    public IEnumerator Scene2Narrative()
    {
        dialogueBox.SetActive(true);
        animator.SetBool("IsOpen", true);
        foreach (char letter in Scene2Sentences[index].ToCharArray())
        {
            narrativeText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public IEnumerator Scene2NameChange()
    {
        foreach (char letter in Scene2Names[index].ToCharArray())
        {
            nameText.text += letter;
            yield return new WaitForSeconds(0);
        }

    }

    public IEnumerator VillageSceneNarrative()
    {
        dialogueBox.SetActive(true);
        animator.SetBool("IsOpen", true);
        nameText.text = "Lys";
        foreach (char letter in VillageSceneSentences[index].ToCharArray())
        {
            narrativeText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }


    public void FadeOnOff()
    {
        if (fadeScreen.activeSelf == true)
        {
            fadeScreen.SetActive(false);
        }

        else
        {
            fadeScreen.SetActive(true);
        }

    }

    public void NextTutorialSentence()
    {
        continueButton.SetActive(false);

        if (index < TutorialSceneSentences.Length - 1)
        {
            index++;
            narrativeText.text = "";
            nameText.text = "Lys";

            if (index == 4)
            {
                GameObject.Find("Lys").GetComponent<Animator>().Play("CheckDunga");
                dialogueBox.SetActive(false);
                Invoke("StartTutorialNarrative", 2.5f);
                animator.SetBool("IsOpen", false);
            }

            else if (index == 5)
            {
                Invoke("StartTutorialNarrative", 1.5f);
                dialogueBox.SetActive(false);
                animator.SetBool("IsOpen", false);
                GameObject.Find("Lys").GetComponent<Animator>().Play("ReturnFromDunga");
            }


            else if (index == 8)
            {
                dialogueBox.SetActive(false);
                animator.SetBool("IsOpen", false);
                GameObject.Find("Lys").GetComponent<Animator>().Play("LysExplore1");
                gameManager.GetComponent<PauseGame>().HealthBarsOn();

            }

            else if (index == 9)
            {
                Invoke("StartTutorialNarrative", 0f);
                GameObject newIcon = Instantiate(tutorialIconPrefab, transform.position, Quaternion.identity);
                newIcon.transform.SetParent(archerBuffbar.transform);

            }

            else if (index== 10)
            {
                GameObject.Find("TutorialIcon(Clone)").GetComponent<TutorialBuffIcon>().active = true;
                dialogueBox.SetActive(false);
                animator.SetBool("IsOpen", false);
            }

            else if (index == 11)
            {
                dialogueBox.SetActive(false);
                animator.SetBool("IsOpen", false);
                gameManager.GetComponent<PauseGame>().ActivateCombat();
                gameManager.GetComponent<PauseGame>().Invoke("FreezeArrow", 2.5f);
                gameManager.GetComponent<PauseGame>().Invoke("StopCombat", 2.5f);
                gameManager.GetComponent<PauseGame>().Invoke("ToggleArrowCollider", 2.0f);
                Invoke("StartTutorialNarrative", 3f);
            }

            else if (index == 12)
            {
                gameManager.GetComponent<PauseGame>().ToggleArrowCollider();
                dialogueBox.SetActive(false);
                animator.SetBool("IsOpen", false);
            }

            else if (index == 13)
            {
                Invoke("StartTutorialNarrative", 0f);
                ActivateClickables();
            }

            else if (index == 16)
            {
                GameObject.Find("GameManager").GetComponent<PauseGame>().ActivateCombat();
                dialogueBox.SetActive(false);
                animator.SetBool("IsOpen", false);
            }

            else if (index == 17)
            {
                Invoke("StartTutorialNarrative", 0f);
            }

            else
            {
                Invoke("StartTutorialNarrative", 0f);
            }
        }

        else
        {
            FadeOnOff();
            fadeScreen.GetComponent<Animator>().Play("FadeOut");
            narrativeText.text = "";
            dialogueBox.SetActive(false);
            animator.SetBool("IsOpen", false);
            gameManager.GetComponent<PauseGame>().Invoke("EndTutorialScene", 3.0f);
        }
    }


    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < Scene1Sentences.Length - 1)
        {
            index++;
            narrativeText.text = "";
            nameText.text = "";

            if (index == 3)
            {
                ChangeSpeed(2.5f);
                BakScript.currentWaypoint = GameObject.Find("BakWP2");
                DungaScript.currentWaypoint = GameObject.Find("DungaWP2");
                dialogueBox.SetActive(false);
                Invoke("StartNarrative", 3.0f);
                animator.SetBool("IsOpen", false);
            }
            
            else if (index == 4)
            {
                ToggleTimeline();
                Invoke("ToggleTimeline", 5.5f);
                Invoke("StartNarrative", 0f);
                GameObject.Find("Barkeep").GetComponent<Barkeep>().Invoke("Yoohoo", 2.5f);
            }
            

            else if (index == 6)
            {
                ChangeSpeed(1.5f);
                BakScript.currentWaypoint = GameObject.Find("BakWP3");
                DungaScript.currentWaypoint = GameObject.Find("DungaWP3");
                dialogueBox.SetActive(false);
                Invoke("StartNarrative", 3.0f);
                animator.SetBool("IsOpen", false);
            }

            else if (index == 9)
            {
                ToggleTimeline();
                Invoke("ToggleTimeline", 5.0f);
                Invoke("StartNarrative", 0f);
            }

            else if (index == 10)
            {
                dialogueBox.SetActive(false);
                Invoke("StartNarrative", 1.0f);
                animator.SetBool("IsOpen", false);
            }

            else if (index == 14)
            {
                ChangeSpeed(7.5f);
                DungaScript.currentWaypoint = GameObject.Find("DungaWP4");
                Invoke("StartNarrative", 0f);
                GameObject.Find("cam2").GetComponent<CinemachineVirtualCamera>().m_LookAt = null;
                GameObject.Find("cam2").GetComponent<CinemachineVirtualCamera>().m_Follow = null;
            }


            else
                Invoke("StartNarrative", 0f);
        }

        else
        {
            FadeOnOff();
            fadeScreen.GetComponent<Animator>().Play("FadeOut");
            //gameManager.GetComponent<PauseGame>().combatActive = true;
            narrativeText.text = "";
            dialogueBox.SetActive(false);
            animator.SetBool("IsOpen", false);
            gameManager.GetComponent<PauseGame>().Invoke("EndOpeningScene" , 3.0f);
        }
    }

    public void NextGameSceneSentence()
    {
        continueButton.SetActive(false);
        if (index < Scene1Sentences.Length - 1)
        {
            player.Stop();
            index++;
            narrativeText.text = "";
            nameText.text = "";

            if (index == 1)
            {
                SetTime(6);
                ToggleTimeline();
                Invoke("ToggleTimeline", 2.5f);
                Invoke("StartGameNarrative", 0f);
            }

            else if (index == 2)
            {

                narrativeText.text = "";
                dialogueBox.SetActive(false);
                animator.SetBool("IsOpen", false);
                gameManager.GetComponent<PauseGame>().ChangeStageEnemySpeed(5.0f);
                gameManager.GetComponent<PauseGame>().ActivateCombat();
            }

            else if (index == 3)
            {
                narrativeText.text = "";
                dialogueBox.SetActive(false);
                animator.SetBool("IsOpen", false);
                DungaScript.currentWaypoint = GameObject.Find("DungaWP2");
                gameManager.GetComponent<PauseGame>().NextStage();
                ToggleTimeline();
                Invoke("ToggleTimeline", 7f);
            }

            else if (index == 5)
            {
                narrativeText.text = "";
                dialogueBox.SetActive(false);
                animator.SetBool("IsOpen", false);
                gameManager.GetComponent<PauseGame>().ActivateCombat();
                //ToggleTimeline();

                // Add freeze & zoom on arrow here 

            }

            else if (index == 7)
            {
                narrativeText.text = "";
                dialogueBox.SetActive(false);
                animator.SetBool("IsOpen", false);
                gameManager.GetComponent<PauseGame>().ActivateCombat();
            }


            else if (index == 8)
            {
                narrativeText.text = "";
                dialogueBox.SetActive(false);
                animator.SetBool("IsOpen", false);
                gameManager.GetComponent<PauseGame>().ActivateCombat();
                ToggleTimeline();
                Invoke("ToggleTimeline", 2f);
            }

            else if (index == 10)
            {
                SetTime(39);
                ToggleTimeline();
                Invoke("ToggleTimeline", 1f);
                Invoke("StartGameNarrative", 0f);
            }

            else if (index == 11)
            {
                SetTime(42);
                ToggleTimeline();
                Invoke("ToggleTimeline", 1f);
                Invoke("StartGameNarrative", 0f);
            }

            else if (index == 12)
            {
                SetTime(45);
                ToggleTimeline();
                Invoke("ToggleTimeline", 1f);
                Invoke("StartGameNarrative", 0f);
            }

            else if (index == 13)
            {
                SetTime(48);
                ToggleTimeline();
                Invoke("ToggleTimeline", 1f);
                Invoke("StartGameNarrative", 0f);
            }

            else if (index == 14)
            {
                narrativeText.text = "";
                dialogueBox.SetActive(false);
                animator.SetBool("IsOpen", false);
                gameManager.GetComponent<PauseGame>().ActivateCombat();
            }

            else
            {
                Invoke("StartGameNarrative", 0f);
            }
        }

        else
        {
            GameObject.Find("BlackFade").GetComponent<Animator>().Play("FadeOut");
            narrativeText.text = "";
            dialogueBox.SetActive(false);
            animator.SetBool("IsOpen", false);
        }
    }

    public void NextVillageSceneSentence()
    {
        continueButton.SetActive(false);
        if (index < VillageSceneSentences.Length - 1)
        {
            index++;
            narrativeText.text = "";

            //if (index == 7)
            //{
            //    dialogueBox.SetActive(false);
            //    animator.SetBool("IsOpen", false);
            //}

            //else {
            //    Invoke("StartVillageNarrative", 0f);
            //}
            Invoke("StartVillageNarrative", 0f);
        }

        else
        {
            FadeOnOff();
            fadeScreen.GetComponent<Animator>().Play("FadeOut");
            narrativeText.text = "";
            dialogueBox.SetActive(false);
            animator.SetBool("IsOpen", false);

            Invoke("LoadVillage", 2f);
        }
    }

    public void NextCreditsSceneSentence()
    {
        if (index < CreditsAudioClips.Length - 1)
        {
            index++;
            Invoke("StartCreditsAudio", 0f);
        }


        else
        {
            FadeOnOff();
            fadeScreen.GetComponent<Animator>().Play("FadeOut");
        }
    }

    public void EndConvo()
    {
        StopCoroutine(Scene1Narrative());
        GetComponent<EnemyMarauder>().moveSpeed = 2.5f;
    }

    private void StartTutorialNarrative()
    {
        dialogueBox.SetActive(true);
        StartCoroutine("TutorialSceneNarrative");
        StartCoroutine("TutorialDialogueAudio");
    }

    private void StartNarrative()
    {
        dialogueBox.SetActive(true);
        StartCoroutine("Scene1Narrative");
        StartCoroutine("Scene1NameChange");
        StartCoroutine("Scene1DialogueAudio");
    }

    public void StartGameNarrative()
    {
        dialogueBox.SetActive(true);
        StartCoroutine("Scene2Narrative");
        StartCoroutine("Scene2NameChange");
        StartCoroutine("Scene2DialogueAudio");
    }

    public void StartVillageNarrative()
    {
        dialogueBox.SetActive(true);
        StartCoroutine("VillageSceneNarrative");
        StartCoroutine("VillageDialogueAudio");
    }

    public void StartCreditsAudio()
    {
        StartCoroutine("CreditsDialogueAudio");

        if (index < CreditsAudioClips.Length - 1)
        {
            if (index == 8)
            {
                Invoke("NextCreditsSceneSentence", CreditsAudioClips[index].length + .5f);
                Invoke("Yoohoo2", CreditsAudioClips[index].length + .2f);
            }

            else if (index == 10)
            {
                GameObject.Find("Lys").GetComponent<Animator>().Play("LysCredits");
                Invoke("NextCreditsSceneSentence", CreditsAudioClips[index].length + 2f);
                Invoke("BakBeard", 2.0f);
            }

            else
            {
                Invoke("NextCreditsSceneSentence", CreditsAudioClips[index].length + 0.5f);
            }

        }

        else
        {
            FadeOnOff();
            fadeScreen.GetComponent<Animator>().Play("FadeOut");
            Invoke("LoadMenu", 2f);
        }
    }

    private void ChangeSpeed(float speed)
    {
        DungaScript.moveSpeed = speed;
        BakScript.moveSpeed = speed;
    }


    private void ToggleTimeline()
    {
        if (Paused == false)
            playable.playableGraph.GetRootPlayable(0).SetSpeed<Playable>(0);

        else
            playable.playableGraph.GetRootPlayable(0).SetSpeed<Playable>(1);

        Paused = !Paused;
    }

    private void SetTime(double _time)
    {
        playable.time = _time;
    }

    private void GetArrowCollider()
    {

    }

    private void DeactivateClickables()
    {
        tutorialRock.SetActive(false);
        tutorialBarrel.SetActive(false);
    }

    private void ActivateClickables()
    {
        tutorialRock.SetActive(true);
        tutorialBarrel.SetActive(true);
    }

    private void LoadVillage()
    {
        SceneManager.LoadScene("VillageScene");

    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }      

    private void LysDodge()
    {
        GameObject.Find("Lys").GetComponent<Animator>().Play("LysArrow");
    }

    private void Yoohoo2()
    {
        GameObject.Find("BarkeepSprite").GetComponent<Animator>().Play("Yoohoo2");
    }

    private void BakBeard()
    {
        GameObject.Find("BakBeard").GetComponent<SpriteRenderer>().enabled = true;
    }
}
