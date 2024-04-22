using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [Header("Other Scripts")]
    [SerializeField] private StoryManager storyManager;

    [Header("Quests")]
    [SerializeField] private GameObject fireQuest1;
    [SerializeField] private GameObject fireQuest2;
    [SerializeField] private GameObject iceeQuest1;
    [SerializeField] private GameObject iceeQuest2;
    [SerializeField] private GameObject lightningQuest1;
    [SerializeField] private GameObject lightningQuest2;
    [SerializeField] private GameObject defensiveQuest1;
    [SerializeField] private GameObject defensiveQuest2;

    [Header("Challenges")]
    [SerializeField] private GameObject fireChallenge1;
    [SerializeField] private GameObject iceChallenge1;
    [SerializeField] private GameObject fireAndIceChallenge1;
    [SerializeField] private GameObject lightningChallenge1;
    [SerializeField] private GameObject fireIceAndLightningChallenge1;
    [SerializeField] private GameObject defensiveChallenge1;
    [SerializeField] private GameObject miscChallenge1;
    [SerializeField] private GameObject miscChallenge2;
    [SerializeField] private GameObject miscChallenge3;
    [SerializeField] private GameObject miscChallenge4;


    [Header("Real-Time Display")]
    public GameObject inGameStats;
    public Text realTimeText;
    public Text realTimeTargetsHitText;

    [Header("Stats Display Panel")]
    public GameObject statsDisplayPanel;
    public Text finalTimeText;
    public Text finalTargetsHitText;
    public Text finalSpellsCastText;
    public Text finalAccuracyText;
    public Text finalGradeText;

    private float startTime;
    private bool isQuestActive = false;
    private bool isChallengeActive = false;

    private int targetsHit = 0;
    private int spellsCast = 0;
    private int targetsGoal = 0;

    public void Update()
    {
        if (isQuestActive || isChallengeActive)
        {
            UpdateTime();
        }
    }

    public void StartQuest(GameObject quest)
    {
        ResetRound();
        inGameStats.SetActive(true);
        quest.SetActive(true);
        isQuestActive = true;
        startTime = Time.time;
        targetsGoal = 5;
        // Make sure UI elements are active
        realTimeText.gameObject.SetActive(true);
        realTimeTargetsHitText.gameObject.SetActive(true);
        realTimeTargetsHitText.text = "Targets Hit: 0";  // Reset the display on start
    }

    public void StartChallenge(GameObject challenge)
    {
        ResetRound();
        inGameStats.SetActive(true);
        challenge.SetActive(true);
        isChallengeActive = true;
        startTime = Time.time;
        targetsGoal = 15;
        // Make sure UI elements are active
        realTimeText.gameObject.SetActive(true);
        realTimeTargetsHitText.gameObject.SetActive(true);
        realTimeTargetsHitText.text = "Targets Hit: 0";  // Reset the display on start
    }

    private void UpdateTime()
    {
        float elapsedTime = Time.time - startTime;
        realTimeText.text = FormatTime(elapsedTime);
        if(isQuestActive)
        {
            realTimeTargetsHitText.text = "Targets Hit: " + targetsHit + "/5";
        }

        if(isChallengeActive)
        {
            realTimeTargetsHitText.text = "Targets Hit: " + targetsHit + "/15";
        }

        if (isChallengeActive && elapsedTime >= 180.0f)  // 3 minutes deadline
        {
            EndChallenge(false);
        }
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void EndChallenge(bool success)
    {
        inGameStats.SetActive(false);
        if (!success)
        {
            DisplayStats("F"); // Display stats with an F grade
        }
        else
        {
            CalculateAccuracyAndGrade();
        }
        ResetRound();
    }

    public void ResetRound()
    {
        isQuestActive = false;
        isChallengeActive = false;
        realTimeText.gameObject.SetActive(false);
        realTimeTargetsHitText.gameObject.SetActive(false);
        inGameStats.SetActive(false);
        targetsHit = 0;
        spellsCast = 0;
    }

    public void IncrementTargetsHit()
    {
        targetsHit++;
        if (targetsHit >= targetsGoal)
        {
            if (isQuestActive)
                CalculateAccuracyAndGrade();
            else if (isChallengeActive)
                EndChallenge(true);
        }
    }

    public void IncrementSpellsCast()
    {
        spellsCast++;
    }

    private void CalculateAccuracyAndGrade()
    {
        float accuracy = (float)targetsHit / spellsCast;
        float timeTaken = Time.time - startTime;
        string grade = DetermineGrade(accuracy, timeTaken);
        finalTimeText.text = FormatTime(Time.time - startTime);
        finalTargetsHitText.text = "Targets Hit: " + targetsHit + "/" + targetsHit;
        finalSpellsCastText.text = "Spells Cast: " + spellsCast;
        finalAccuracyText.text = "Accuracy: " + ((float)targetsHit / spellsCast).ToString("P2");
        finalGradeText.text = "Grade: " + grade;
        
        //move on to quest completed screen
        storyManager.GameSequenceProgressor();

       // DisplayStats(grade);
    }

    private string DetermineGrade(float accuracy, float timeTaken)
    {
        if (accuracy >= 0.9 && timeTaken < 60) return "S";
        else if (accuracy >= 0.8 && timeTaken < 120) return "A";
        else if (accuracy >= 0.7 && timeTaken < 150) return "B";
        else if (accuracy >= 0.6 && timeTaken < 180) return "C";
        else if (accuracy >= 0.5) return "D";
        else return "E";
    }

    private void DisplayStats(string grade)
    {
        statsDisplayPanel.SetActive(true);
        finalTimeText.text = FormatTime(Time.time - startTime);
        finalTargetsHitText.text = "Targets Hit: " + targetsHit;
        finalSpellsCastText.text = "Spells Cast: " + spellsCast;
        finalAccuracyText.text = "Accuracy: " + ((float)targetsHit / spellsCast).ToString("P2");
        finalGradeText.text = "Grade: " + grade;
    }
}