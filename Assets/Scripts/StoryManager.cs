using UnityEngine;
using UnityEngine.UI;

/* This script manages the story progression and position of the game
 * With this script, the game moves from one part to the next and can jump to various spots
 * Based on level and chapter
 * Proposed story progression at the bottom of this script
 */
public class StoryManager : MonoBehaviour
{
    [Header("Other scripts")]
    [SerializeField] private DelayedActivator delayedActivator;
    [SerializeField] private RoundManager roundManager;
    //[SerializeField] private MenuEnabler menuEnabler;

    [Header("Pop-Up UI Screens")]
    [SerializeField] private GameObject welcomeNoticeScreen;

    [SerializeField] private GameObject noticeScreen;
    [SerializeField] private GameObject levelUpScreen;

    [SerializeField] private GameObject questRequestScreen;
    [SerializeField] private GameObject questPassedScreen;
    [SerializeField] private GameObject challengeRequestScreen;
    [SerializeField] private GameObject challengePassedScreen;
    [SerializeField] private GameObject challengeFailedScreen;

    [Header("Screen Text")]
    [SerializeField] private Text noticeText;
    [SerializeField] private Text levelUpText;
    [SerializeField] private Text questRequestText;
    [SerializeField] private Text questPassedText;
    [SerializeField] private Text challengeRequestText;
    [SerializeField] private Text challengePassedText;
    [SerializeField] private Text challengeFailedText;

    [Header("Magic Buttons")]
    [SerializeField] private GameObject r_fireMagicButton;
    [SerializeField] private GameObject r_iceMagicButton;
    [SerializeField] private GameObject r_lightningMagicButton;
    [SerializeField] private GameObject r_defensiveMagicButton;

    [SerializeField] private GameObject r_spellLeftButton;
    [SerializeField] private GameObject r_spellRightButton;

    [SerializeField] private GameObject r_newMagic1Button;
    [SerializeField] private GameObject r_newMagic2Button;
    [SerializeField] private GameObject r_newMagic3Button;
    [SerializeField] private GameObject r_newMagic4Button;

    [SerializeField] private GameObject l_fireMagicButton;
    [SerializeField] private GameObject l_iceMagicButton;
    [SerializeField] private GameObject l_lightningMagicButton;
    [SerializeField] private GameObject l_defensiveMagicButton;

    [SerializeField] private GameObject l_spellLeftButton;
    [SerializeField] private GameObject l_spellRightButton;

    [SerializeField] private GameObject l_newMagic1Button;
    [SerializeField] private GameObject l_newMagic2Button;
    [SerializeField] private GameObject l_newMagic3Button;
    [SerializeField] private GameObject l_newMagic4Button;

    [SerializeField] private GameObject trainingRoomsButton;

    [Header("Quests and Challenges")]
    [SerializeField] private GameObject quest1; //fire 1
    [SerializeField] private GameObject quest2; //fire 2
    [SerializeField] private GameObject quest3; //ice 1
    [SerializeField] private GameObject quest4; //ice2
    [SerializeField] private GameObject quest5; //lightning 1
    [SerializeField] private GameObject quest6; //lightning 2
    [SerializeField] private GameObject quest7; //defensive 1
    [SerializeField] private GameObject quest8; //defensive 2

    [SerializeField] private GameObject challenge1; //fire 3
    [SerializeField] private GameObject challenge2; //ice 3
    [SerializeField] private GameObject challenge3; //fire & ice 1
    [SerializeField] private GameObject challenge4; //lightning 3
    [SerializeField] private GameObject challenge5; //fire, ice, & lightning 1
    [SerializeField] private GameObject challenge6; //defensive 3
    [SerializeField] private GameObject trainingRoom; //trainingRoom
    [SerializeField] private GameObject challenge7; //misc 1
    [SerializeField] private GameObject challenge8; //misc 2
    [SerializeField] private GameObject challenge9; //misc 3
    [SerializeField] private GameObject challenge10; //misc 4

    [Header("variables")]
    public int gameSequencePosition;
    public int playerLevel;



    // Start is called before the first frame update
    void Start()
    {
        //GoToChapter0();
    }

    public void DeactivateAllScreens()
    {
        welcomeNoticeScreen.SetActive(false);
        noticeScreen.SetActive(false);
        levelUpScreen.SetActive(false);
        questRequestScreen.SetActive(false);
        questPassedScreen.SetActive(false);
        challengeRequestScreen.SetActive(false);
        challengePassedScreen.SetActive(false);
        challengeFailedScreen.SetActive(false);

        quest1.gameObject.SetActive(false);
        quest2.gameObject.SetActive(false);
        quest3.gameObject.SetActive(false);
        quest4.gameObject.SetActive(false);
        quest5.gameObject.SetActive(false);
        quest6.gameObject.SetActive(false);
        quest7.gameObject.SetActive(false);
        quest8.gameObject.SetActive(false);


        challenge1.gameObject.SetActive(false);
        challenge2.gameObject.SetActive(false);
        challenge3.gameObject.SetActive(false);
        challenge4.gameObject.SetActive(false);
        challenge5.gameObject.SetActive(false);
        challenge6.gameObject.SetActive(false);
        challenge7.gameObject.SetActive(false);
        challenge8.gameObject.SetActive(false);
        challenge9.gameObject.SetActive(false);
        challenge10.gameObject.SetActive(false);

        roundManager.inGameStats.SetActive(false);

        // Include any other UI components that need to be managed
    }

    public void GameSequenceProgressor()
    {
        gameSequencePosition += 1;

        //disable ingame menu to avoid errors, this is quickly changed for quests and challenges tho
        //menuEnabler.NoMenusAllowed();

        // Clear all screens at the beginning to ensure a clean state
        DeactivateAllScreens();

        switch (gameSequencePosition)
        {
            // Chapter 0: Getting Started
            case 1:
                delayedActivator.ActivateGameObject(welcomeNoticeScreen);
                break;
            case 2:
                SetNoticeText("Hold pinch for 2 seconds to bring up the menu,\n hold fist to power up a spell.");
                delayedActivator.ActivateGameObject(noticeScreen);
                break;
            case 3:
                SetLevelUpText("Congratulations! You've reached Level 1!");
                delayedActivator.ActivateGameObject(levelUpScreen);
                break;
            case 4:
                SetNoticeText("Fire Magic is now available!");
                delayedActivator.ActivateGameObject(noticeScreen);
                r_fireMagicButton.SetActive(true);
                l_fireMagicButton.SetActive(true);
                break;
            case 5:
                SetNoticeText("You are now an Initiate Sorcerer.");
                delayedActivator.ActivateGameObject(noticeScreen);
                break;
            // Chapter 1: Fire Magic
            case 6:
                SetQuestRequestText("Use fire magic to hit nonmoving targets\n 5 times.");
                delayedActivator.ActivateGameObject(questRequestScreen);
                break;
            case 7:
                delayedActivator.ActivateGameObject(quest1);
                roundManager.StartQuest(quest1);
               // menuEnabler.MenusAreAllowed();
                break;
            case 8:
                roundManager.ResetRound();
                SetQuestPassedText("You have completed Fire Quest 1");
                delayedActivator.ActivateGameObject(questPassedScreen);
                break;
            case 9:
                SetLevelUpText("Level 2! Stats page info but with Level\n up info too.");
                delayedActivator.ActivateGameObject(levelUpScreen);
                break;
            case 10:
                SetQuestRequestText("Use fire magic to hit moving targets\n 5 times.");
                delayedActivator.ActivateGameObject(questRequestScreen);
                break;
            case 11:
                delayedActivator.ActivateGameObject(quest2);
                roundManager.StartQuest(quest2);
                // menuEnabler.MenusAreAllowed();
                break;
            case 12:
                roundManager.ResetRound();
                SetQuestPassedText("You have completed Fire Quest 2");
                delayedActivator.ActivateGameObject(questPassedScreen);
                break;
            case 13:
                SetLevelUpText("Level 3! Stats page info but with Level\n up info too.");
                delayedActivator.ActivateGameObject(levelUpScreen);
                break;
            case 14:
                SetChallengeRequestText("Something cool with fire\n magic.");
                delayedActivator.ActivateGameObject(challengeRequestScreen);
                break;
            case 15:
                roundManager.ResetRound();
                delayedActivator.ActivateGameObject(challenge1);
                roundManager.StartQuest(challenge1);
              //  menuEnabler.MenusAreAllowed();
                break;
            case 16:
                SetChallengePassedText("You passed the fire magic challenge!");
                delayedActivator.ActivateGameObject(challengePassedScreen);
                break;
            case 17:
                SetLevelUpText("Level 4! Congratulations!");
                delayedActivator.ActivateGameObject(levelUpScreen);
                break;
            case 18:
                SetNoticeText("Ice Magic is now available.");
                delayedActivator.ActivateGameObject(noticeScreen);
                r_iceMagicButton.SetActive(true);
                l_iceMagicButton.SetActive(true);
                break;
            case 19:
                SetNoticeText("Novice Sorcerer.");
                delayedActivator.ActivateGameObject(noticeScreen);
                break;
            // Chapter 2: Ice Magic
            case 20:
                SetQuestRequestText("Use ice magic to hit nonmoving\n targets 5 times.");
                delayedActivator.ActivateGameObject(questRequestScreen);
                break;
            case 21:
                delayedActivator.ActivateGameObject(quest3);
              //  menuEnabler.MenusAreAllowed();
                break;
            case 22:
                SetQuestPassedText("You beat Quest 3! Data: Time till \ncompletion, targets hit 5/5, spells cast, \naccuracy, grade, based on time and accuracy.");
                delayedActivator.ActivateGameObject(questPassedScreen);
                break;
            case 23:
                SetQuestRequestText("Use ice magic to hit moving \ntargets 5 times.");
                delayedActivator.ActivateGameObject(questRequestScreen);
                break;
            case 24:
                delayedActivator.ActivateGameObject(quest4);
             //   menuEnabler.MenusAreAllowed();
                break;
            case 25:
                SetQuestPassedText("You beat Quest 4! Data: Time till \ncompletion, targets hit 5/5, spells cast,\n accuracy, grade, based on time and accuracy.");
                delayedActivator.ActivateGameObject(questPassedScreen);
                break;
            case 26:
                SetLevelUpText("Level 5! Congratulations!");
                delayedActivator.ActivateGameObject(levelUpScreen);
                break;
            case 27:
                SetChallengeRequestText("Challenge with ice magic,\n level 3.");
                delayedActivator.ActivateGameObject(challengeRequestScreen);
                break;
            case 28:
                delayedActivator.ActivateGameObject(challenge2);
              //  menuEnabler.MenusAreAllowed();
                break;
            case 29:
                SetChallengePassedText("You passed the ice magic challenge! \nData: Time till completion, targets hit 5/5, spells cast,\n accuracy, grade, based on time and accuracy.");
                delayedActivator.ActivateGameObject(challengePassedScreen);
                break;
            case 30:
                SetLevelUpText("Level 6! Congratulations!");
                delayedActivator.ActivateGameObject(levelUpScreen);
                break;
            case 31:
                SetChallengeRequestText("Fire & Ice combined challenge.");
                delayedActivator.ActivateGameObject(challengeRequestScreen);
                break;
            case 32:
                delayedActivator.ActivateGameObject(challenge3);
             //   menuEnabler.MenusAreAllowed();
                break;
            case 33:
                SetChallengePassedText("You passed the Fire & Ice challenge! Data:\n Time till completion, targets hit 5/5, spells cast, accuracy, grade, \nbased on time and accuracy.");
                delayedActivator.ActivateGameObject(challengePassedScreen);
                break;
            case 34:
                SetLevelUpText("Level 7! Congratulations!");
                delayedActivator.ActivateGameObject(levelUpScreen);
                break;
            case 35:
                SetNoticeText("Lightning Magic is now available.");
                delayedActivator.ActivateGameObject(noticeScreen);
                r_lightningMagicButton.SetActive(true);
                l_lightningMagicButton.SetActive(true);
                break;
            case 36:
                SetNoticeText("Apprentice Sorcerer.");
                delayedActivator.ActivateGameObject(noticeScreen);
                break;
            // Chapter 3: Lightning Magic
            case 37:
                SetQuestRequestText("Use lightning magic to hit \nnonmoving targets.");
                delayedActivator.ActivateGameObject(questRequestScreen);
                break;
            case 38:
                delayedActivator.ActivateGameObject(quest5);
            //    menuEnabler.MenusAreAllowed();
                break;
            case 39:
                SetQuestPassedText("You beat the Lightning Quest! Time till completion,\n targets hit 5/5.");
                delayedActivator.ActivateGameObject(questPassedScreen);
                break;
            case 40:
                SetQuestRequestText("Use lightning magic to hit moving\n targets 5 times.");
                delayedActivator.ActivateGameObject(questRequestScreen);
                break;
            case 41:
                delayedActivator.ActivateGameObject(quest6);
             //   menuEnabler.MenusAreAllowed();
                break;
            case 42:
                SetQuestPassedText("You beat the Lightning Quest 2! Time\n till completion, targets hit 5/5.");
                delayedActivator.ActivateGameObject(questPassedScreen);
                break;
            case 43:
                SetChallengeRequestText("Lightning level 3 challenge.");
                delayedActivator.ActivateGameObject(challengeRequestScreen);
                break;
            case 44:
                delayedActivator.ActivateGameObject(challenge4);
             //   menuEnabler.MenusAreAllowed();
                break;
            case 45:
                SetChallengePassedText("You passed the Lightning level 3\n challenge! Data: Time till completion, targets hit 5/5.");
                delayedActivator.ActivateGameObject(challengePassedScreen);
                break;
            case 46:
                SetLevelUpText("Level 8! Congratulations!");
                delayedActivator.ActivateGameObject(levelUpScreen);
                break;
            case 47:
                SetChallengeRequestText("Combined Fire, Ice, and Lightning\n challenge.");
                delayedActivator.ActivateGameObject(challengeRequestScreen);
                break;
            case 48:
                delayedActivator.ActivateGameObject(challenge5);
             //   menuEnabler.MenusAreAllowed();
                break;
            case 49:
                SetChallengePassedText("You passed the combined elements \nchallenge!");
                delayedActivator.ActivateGameObject(challengePassedScreen);
                break;
            case 50:
                SetLevelUpText("Level 9! Congratulations!");
                delayedActivator.ActivateGameObject(levelUpScreen);
                break;
            case 51:
                SetNoticeText("Defensive Magic is now available.");
                delayedActivator.ActivateGameObject(noticeScreen);
                r_defensiveMagicButton.SetActive(true);
                l_defensiveMagicButton.SetActive(true);
                break;
            case 52:
                SetNoticeText("Adept Sorcerer.");
                delayedActivator.ActivateGameObject(noticeScreen);
                break;
            // Chapter 4: Defensive Magic
            case 53:
                SetQuestRequestText("Defensive magic level 1 quest.");
                delayedActivator.ActivateGameObject(questRequestScreen);
                break;
            case 54:
                delayedActivator.ActivateGameObject(quest7);
              //  menuEnabler.MenusAreAllowed();
                break;
            case 55:
                SetQuestPassedText("You passed the Defensive level 1 quest!");
                delayedActivator.ActivateGameObject(questPassedScreen);
                break;
            case 56:
                SetQuestRequestText("Defensive magic level 2 quest.");
                delayedActivator.ActivateGameObject(questRequestScreen);
                break;
            case 57:
                delayedActivator.ActivateGameObject(quest8);
              //  menuEnabler.MenusAreAllowed();
                break;
            case 58:
                SetQuestPassedText("You passed the Defensive level 2 \nquest!");
                delayedActivator.ActivateGameObject(questPassedScreen);
                break;
            case 59:
                SetChallengeRequestText("Offensive and Defensive challenge.");
                delayedActivator.ActivateGameObject(challengeRequestScreen);
                break;
            case 60:
                delayedActivator.ActivateGameObject(challenge6);
              //  menuEnabler.MenusAreAllowed();
                break;
            case 61:
                SetChallengePassedText("You passed the Offensive and \nDefensive challenge!");
                delayedActivator.ActivateGameObject(challengePassedScreen);
                break;
            case 62:
                SetLevelUpText("Level 10! Congratulations!");
                delayedActivator.ActivateGameObject(levelUpScreen);
                break;
            case 63:
                SetNoticeText("New Magics Available!");
                delayedActivator.ActivateGameObject(noticeScreen);
                r_newMagic1Button.SetActive(true);
                l_newMagic1Button.SetActive(true);
                r_newMagic2Button.SetActive(true);
                l_newMagic2Button.SetActive(true);
                r_newMagic3Button.SetActive(true);
                l_newMagic3Button.SetActive(true);
                r_newMagic4Button.SetActive(true);
                l_newMagic4Button.SetActive(true);
                r_spellRightButton.SetActive(true);
                l_spellRightButton.SetActive(true);
                r_spellLeftButton.SetActive(true);
                l_spellLeftButton.SetActive(true);
                break;
            case 64:
                SetNoticeText("You are now an Advanced Sorcerer.");
                delayedActivator.ActivateGameObject(noticeScreen);
                break;
            // Chapter 5: All Other Magics
            case 65:
                SetQuestRequestText("Training Room: Try all new magics.");
                delayedActivator.ActivateGameObject(questRequestScreen);
                break;
            case 66:
                delayedActivator.ActivateGameObject(trainingRoom);
             //   menuEnabler.MenusAreAllowed();
                break;
            case 67:
                SetQuestPassedText("Training complete! Ready for new challenges.");
                delayedActivator.ActivateGameObject(questPassedScreen);
             //   menuEnabler.MenusAreAllowed();
                break;
            case 68:
                SetChallengeRequestText("Miscellaneous Challenge 1.");
                delayedActivator.ActivateGameObject(challengeRequestScreen);
                break;
            case 69:
                delayedActivator.ActivateGameObject(challenge7);
               // menuEnabler.MenusAreAllowed();
                break;
            case 70:
                SetChallengePassedText("You passed the Miscellaneous Challenge 1!");
                delayedActivator.ActivateGameObject(challengePassedScreen);
                break;
            case 71:
                SetChallengeRequestText("Miscellaneous Challenge 2.");
                delayedActivator.ActivateGameObject(challengeRequestScreen);
                break;
            case 72:
                delayedActivator.ActivateGameObject(challenge8);
              //  menuEnabler.MenusAreAllowed();
                break;
            case 73:
                SetChallengePassedText("You passed the Miscellaneous Challenge 2!");
                delayedActivator.ActivateGameObject(challengePassedScreen);
                break;
            case 74:
                SetChallengeRequestText("Miscellaneous Challenge 3.");
                delayedActivator.ActivateGameObject(challengeRequestScreen);
                break;
            case 75:
                delayedActivator.ActivateGameObject(challenge9);
             //   menuEnabler.MenusAreAllowed();
                break;
            case 76:
                SetChallengePassedText("You passed the Miscellaneous Challenge 3!");
                delayedActivator.ActivateGameObject(challengePassedScreen);
                break;
            case 77:
                SetChallengeRequestText("Miscellaneous Challenge 4.");
                delayedActivator.ActivateGameObject(challengeRequestScreen);
                break;
            case 78:
                delayedActivator.ActivateGameObject(challenge10);
             //   menuEnabler.MenusAreAllowed();
                break;
            case 79:
                SetChallengePassedText("You passed the Miscellaneous Challenge 4!");
                delayedActivator.ActivateGameObject(challengePassedScreen);
                break;
            case 80:
                SetLevelUpText("Level 11! Congratulations!");
                delayedActivator.ActivateGameObject(levelUpScreen);
                break;
            case 81:
                SetNoticeText("Instant Death magic available.");
                delayedActivator.ActivateGameObject(noticeScreen);
                break;
            case 82:
                SetNoticeText("You are now a Master Sorcerer.");
                delayedActivator.ActivateGameObject(noticeScreen);
                break;
            // Chapter 6: Master Sorcerer
            case 83:
                SetNoticeText("Free Play Training Now Available (new button\n enabled in hand menu).");
                trainingRoomsButton.SetActive(true);
                delayedActivator.ActivateGameObject(noticeScreen);
                break;
            case 84:
                SetNoticeText("Thank you for playing. Currently, this is the end \nof the game. Should there be more?");
                delayedActivator.ActivateGameObject(noticeScreen);
                break; 
                
        }
    }


    private void SetNoticeText(string text)
    {
        noticeText.text = text;
    }

    private void SetLevelUpText(string text)
    {
        levelUpText.text = text;
    }

    private void SetQuestRequestText(string text)
    {
        questRequestText.text = text;
    }

    private void SetQuestPassedText(string text)
    {
        questPassedText.text = text;
    }

    private void SetChallengeRequestText(string text)
    {
        challengeRequestText.text = text;
    }

    private void SetChallengePassedText(string text)
    {
        challengePassedText.text = text;
    }

    private void SetChallengeFailedText(string text)
    {
        challengeFailedText.text = text;
    }

    public void GoToChapter0()
    {
        gameSequencePosition = 0;
        playerLevel = 0;

        GameSequenceProgressor();

        l_fireMagicButton.SetActive(true);
        l_iceMagicButton.SetActive(false);
        l_lightningMagicButton.SetActive(false);
        l_defensiveMagicButton.SetActive(false);

        l_spellLeftButton.SetActive(false);
        l_spellRightButton.SetActive(false);

        l_newMagic1Button.SetActive(false);
        l_newMagic2Button.SetActive(false);
        l_newMagic3Button.SetActive(false);
        l_newMagic4Button.SetActive(false);

        r_fireMagicButton.SetActive(true);
        r_iceMagicButton.SetActive(false);
        r_lightningMagicButton.SetActive(false);
        r_defensiveMagicButton.SetActive(false);

        r_spellLeftButton.SetActive(false);
        r_spellRightButton.SetActive(false);

        r_newMagic1Button.SetActive(false);
        r_newMagic2Button.SetActive(false);
        r_newMagic3Button.SetActive(false);
        r_newMagic4Button.SetActive(false);
        
        trainingRoomsButton.SetActive(false);
    }

    public void GoToChapter1()
    {
        gameSequencePosition = 5;
        playerLevel = 1;

        GameSequenceProgressor();

        l_fireMagicButton.SetActive(true);
        l_iceMagicButton.SetActive(false);
        l_lightningMagicButton.SetActive(false);
        l_defensiveMagicButton.SetActive(false);

        l_spellLeftButton.SetActive(false);
        l_spellRightButton.SetActive(false);

        l_newMagic1Button.SetActive(false);
        l_newMagic2Button.SetActive(false);
        l_newMagic3Button.SetActive(false);
        l_newMagic4Button.SetActive(false);

        r_fireMagicButton.SetActive(true);
        r_iceMagicButton.SetActive(false);
        r_lightningMagicButton.SetActive(false);
        r_defensiveMagicButton.SetActive(false);

        r_spellLeftButton.SetActive(false);
        r_spellRightButton.SetActive(false);

        r_newMagic1Button.SetActive(false);
        r_newMagic2Button.SetActive(false);
        r_newMagic3Button.SetActive(false);
        r_newMagic4Button.SetActive(false);

        trainingRoomsButton.SetActive(false);
    }

    public void GoToChapter2()
    {
        gameSequencePosition = 19;
        playerLevel = 4;

        GameSequenceProgressor();

        l_fireMagicButton.SetActive(true);
        l_iceMagicButton.SetActive(true);
        l_lightningMagicButton.SetActive(false);
        l_defensiveMagicButton.SetActive(false);

        l_spellLeftButton.SetActive(false);
        l_spellRightButton.SetActive(false);

        l_newMagic1Button.SetActive(false);
        l_newMagic2Button.SetActive(false);
        l_newMagic3Button.SetActive(false);
        l_newMagic4Button.SetActive(false);

        r_fireMagicButton.SetActive(true);
        r_iceMagicButton.SetActive(true);
        r_lightningMagicButton.SetActive(false);
        r_defensiveMagicButton.SetActive(false);

        r_spellLeftButton.SetActive(false);
        r_spellRightButton.SetActive(false);

        r_newMagic1Button.SetActive(false);
        r_newMagic2Button.SetActive(false);
        r_newMagic3Button.SetActive(false);
        r_newMagic4Button.SetActive(false);

        trainingRoomsButton.SetActive(false);
    }


    public void GoToChapter3()
    {
        gameSequencePosition = 36;
        playerLevel = 7;

        GameSequenceProgressor();

        l_fireMagicButton.SetActive(true);
        l_iceMagicButton.SetActive(true);
        l_lightningMagicButton.SetActive(true);
        l_defensiveMagicButton.SetActive(false);

        l_spellLeftButton.SetActive(false);
        l_spellRightButton.SetActive(false);

        l_newMagic1Button.SetActive(false);
        l_newMagic2Button.SetActive(false);
        l_newMagic3Button.SetActive(false);
        l_newMagic4Button.SetActive(false);

        r_fireMagicButton.SetActive(true);
        r_iceMagicButton.SetActive(true);
        r_lightningMagicButton.SetActive(true);
        r_defensiveMagicButton.SetActive(false);

        r_spellLeftButton.SetActive(false);
        r_spellRightButton.SetActive(false);

        r_newMagic1Button.SetActive(false);
        r_newMagic2Button.SetActive(false);
        r_newMagic3Button.SetActive(false);
        r_newMagic4Button.SetActive(false);

        trainingRoomsButton.SetActive(false);
    }

    public void GoToChapter4()
    {
        gameSequencePosition = 52;
        playerLevel = 9;

        GameSequenceProgressor();

        l_fireMagicButton.SetActive(true);
        l_iceMagicButton.SetActive(true);
        l_lightningMagicButton.SetActive(true);
        l_defensiveMagicButton.SetActive(true);

        l_spellLeftButton.SetActive(false);
        l_spellRightButton.SetActive(false);

        l_newMagic1Button.SetActive(false);
        l_newMagic2Button.SetActive(false);
        l_newMagic3Button.SetActive(false);
        l_newMagic4Button.SetActive(false);

        r_fireMagicButton.SetActive(true);
        r_iceMagicButton.SetActive(true);
        r_lightningMagicButton.SetActive(true);
        r_defensiveMagicButton.SetActive(true);

        r_spellLeftButton.SetActive(false);
        r_spellRightButton.SetActive(false);

        r_newMagic1Button.SetActive(false);
        r_newMagic2Button.SetActive(false);
        r_newMagic3Button.SetActive(false);
        r_newMagic4Button.SetActive(false);

        trainingRoomsButton.SetActive(false);
    }

    public void GoToChapter5()
    {
        gameSequencePosition = 64;
        playerLevel = 10;

        GameSequenceProgressor();

        l_fireMagicButton.SetActive(true);
        l_iceMagicButton.SetActive(true);
        l_lightningMagicButton.SetActive(true);
        l_defensiveMagicButton.SetActive(true);

        l_spellLeftButton.SetActive(true);
        l_spellRightButton.SetActive(true);

        l_newMagic1Button.SetActive(true);
        l_newMagic2Button.SetActive(true);
        l_newMagic3Button.SetActive(true);
        l_newMagic4Button.SetActive(true);

        r_fireMagicButton.SetActive(true);
        r_iceMagicButton.SetActive(true);
        r_lightningMagicButton.SetActive(true);
        r_defensiveMagicButton.SetActive(true);

        r_spellLeftButton.SetActive(true);
        r_spellRightButton.SetActive(true);

        r_newMagic1Button.SetActive(true);
        r_newMagic2Button.SetActive(true);
        r_newMagic3Button.SetActive(true);
        r_newMagic4Button.SetActive(true);

        trainingRoomsButton.SetActive(false);
    }

    public void GoToChapter6()
    {
        gameSequencePosition = 82;
        playerLevel = 11;

        GameSequenceProgressor();

        l_fireMagicButton.SetActive(true);
        l_iceMagicButton.SetActive(true);
        l_lightningMagicButton.SetActive(true);
        l_defensiveMagicButton.SetActive(true);

        l_spellLeftButton.SetActive(true);
        l_spellRightButton.SetActive(true);

        l_newMagic1Button.SetActive(true);
        l_newMagic2Button.SetActive(true);
        l_newMagic3Button.SetActive(true);
        l_newMagic4Button.SetActive(true);

        r_fireMagicButton.SetActive(true);
        r_iceMagicButton.SetActive(true);
        r_lightningMagicButton.SetActive(true);
        r_defensiveMagicButton.SetActive(true);

        r_spellLeftButton.SetActive(true);
        r_spellRightButton.SetActive(true);

        r_newMagic1Button.SetActive(true);
        r_newMagic2Button.SetActive(true);
        r_newMagic3Button.SetActive(true);
        r_newMagic4Button.SetActive(true);

        trainingRoomsButton.SetActive(true);
    }

    


    /*Solo Leveling Magic

    Proposed Story Path 4/18/2024

    **Chapter 0: Start**

    case 0: User starts game, main menu disappears.
    case 1: Welcome pops up with some ominous option.
    case 2: Notice: How-To explanation begins (hold pinch for 2 seconds to bring up the menu, hold fist to power up a spell).
    case 3: Level Up 1: Level 1!
    case 4: Notice: Fire Magic Now Available.
    case 5: Notice: (Initiate Sorcerer) (passcode).

    **Chapter 1: Fire Magic**

    case 6: Quest request 1: use fire magic to hit nonmoving targets 5 times.
    case 7: Quest 1: Fire level 1.
    case 8: Quest 1 Passed: data about time till completion, targets hit 5/5, spells cast, accuracy, grade, based on time and accuracy.
    case 9: Level Up 2: Level 2! Stats page info but with Level up info too (Passcode).
    case 10: Quest request 2: use fire magic to hit moving targets 5 times.
    case 11: Quest 2: Fire level 2.
    case 12: Quest 2 Passed: data about time till completion, targets hit 5/5, spells cast, accuracy, grade, based on time and accuracy.
    case 13: Level Up 3: Level 3! Stats page info but with Level up info too.
    case 14: Challenge 1 Request: something cool with fire magic.
    case 15: Challenge 1: Fire level 3.
    case 16: Challenge 1 Passed: data about time till completion, targets hit 5/5, spells cast, accuracy, grade, based on time and accuracy.
    case 17: Level Up 4: Level 4! 
    case 18: Notice: Ice Magic is now available.
    case 19: Notice: Novice Sorcerer (passcode).

    **Chapter 2: Ice Magic**

    case 20: Quest 3 Request.
    case 21: Quest 3: Ice level 1.
    case 22: Quest 3 Passed.
    case 23: Quest 4 Request.
    case 24: Quest 4: Ice level 2.
    case 25: Quest 4 Passed.
    case 26: Level Up 5: Level 5!
    case 27: Challenge 2 Request.
    case 28: Challenge 2: Ice level 3 (timed event).
    case 29: Challenge Passed.
    case 30: Level Up 6: Level 6!
    case 31: Challenge 3 Request.
    case 32: Challenge 3: Fire & Ice level 4 (timed event).
    case 33: Challenge Passed.
    case 34: Level Up 7: Level 7!
    case 35: Notice: Lightning Now Available.
    case 36: Notice: Apprentice Sorcerer (passcode).

    **Chapter 3: Lightning Magic**

    case 37: Quest 5 Request.
    case 38: Quest 5: Lightning level 1.
    case 39: Quest 5 Passed.
    case 40: Quest 6 Request.
    case 41: Quest 6: Lightning level 2.
    case 42: Quest 6 Passed.
    case 43: Challenge 4 Request.
    case 44: Challenge 4: Lightning level 3 (timed event).
    case 45: Challenge 4 Passed.
    case 46: Level Up 8: Level 8!
    case 47: Challenge 5 Request.
    case 48: Challenge 5: Fire, Ice, and Lightning (timed event).
    case 49: Challenge Passed.
    case 50: Level Up 9: Level 9!
    case 51: Notice: Defensive Magic Now Available.
    case 52: Notice: Adept Sorcerer (passcode).

    **Chapter 4: Defensive Magic**

    case 53: Quest 7 Request.
    case 54: Quest 7: Defensive level 1.
    case 55: Quest 7 Passed.
    case 56: Quest 8 Request.
    case 57: Quest 8: Defensive level 2.
    case 58: Quest 8 Passed.
    case 59: Challenge 6 Request.
    case 60: Challenge 6: Offensive and Defensive level 1 (timed).
    case 61: Challenge 6 Passed
    case 62: Level Up 10: Level 10!
    case 63: Notice: New Magics Available!
    case 64: Notice: Advanced Sorcerer (passcode).

    **Chapter 5: All Other Magics**

    case 65: Training Room Request.
    case 66: Training Room: Try all new magics.
    case 67: Training Passed.
    case 68: Challenge 7 Request.
    case 69: Challenge 7: Misc 1.
    case 70: Challenge 7 Passed.
    case 71: Challenge 8 Request.
    case 72: Challenge 8: Misc 2.
    case 73: Challenge 8 Passed.
    case 74: Challenge 9 Request.
    case 75: Challenge 9: Misc 3.
    case 76: Challenge 9 Passed.
    case 77: Challenge 10 Request.
    case 78: Challenge 10: Misc 4.
    case 79: Challenge 10 Passed.
    case 80: Level Up 11: Level 11!
    case 81: Notice: Instant Death magic available.
    case 82: Notice: Master Sorcerer (passcode).
    case 83: Notice: Free Play Training Now Available (new button enabled in hand menu).
    case 84: Notice: Thank you for playing. Currently, this is the end of the game. Should there be more?*/
}
