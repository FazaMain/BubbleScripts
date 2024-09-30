using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.Rendering.Universal;
// Прикрутить Cinemachine

public class GameManager : MonoBehaviour
{
    // Тянуть ивенты для Fence
    [Header("Объекты")]
    public GameObject[] FenceArray;
    public List<GameObject> CompatibleFenceArray;
    public GameObject[] AiPlayersArray;
    public GameObject[] ItemsArray;
    public Color LevelColor;
    public Color[] ColorVariants;
    public Light2D BackgroundLight;

    public Transform[] BonusSpawnPoints;
    public GameObject Bonus;

    //Logic
    public List<int> UniqeNumbersList;
    [Header("Спавн")]
    public GameObject[] EnemyToSpawn;
    public Transform[] SpwanPositions;
    [HideInInspector]
    public bool GameOver;

    [Header("Счет/HP/Билеты/Раунды")]
    public int Score = 0;
    public int MaxHP = 100;
    public int HP = 70;
    public int Tickets = 2;
    public float RoundTime;
    public float FenceTime;
    public float ScorePlusTimer;
    public int TimeScoreToPlus;
    int Round = 0;
    [Header("UI")]
    public TextMeshProUGUI CurrentRoundText;
    public TextMeshProUGUI RoundText;
    public Animator RoundAnimator;
    public TextMeshProUGUI[] ScoreHpTicket;
    public Slider HpBar;
    public GameObject LevelUIPanel;
    public GameObject[] TicketsArray;
    public Animator HpBarAnimator;
    public bool VibroOn = true;
    public GameObject GameOverPanel;

    [Header("PlayerPrefs")]
    public int TotalScore;

    [Header("Sounds")]
    public AudioSource[] PlayerSounds;
    public AudioSource[] EnemySounds;
    public AudioSource PickupSound;
    public AudioSource GameStartSound;
    public AudioSource Music;

    public static event Action FenceOffSeq;

    public MenuController MC;
    public ScoreController SC;



    private void OnEnable()
    {
        //PREFSCLEANER();
        BonusScript.BonusPick += BonusPickup;  
    }
    private void OnDisable()
    {
        BonusScript.BonusPick -= BonusPickup;
    }


    void Mix(List<int> listToMix)
    {
        for (int i = 0; i < listToMix.Count; i++) // Спасибо Фишеру и Йетсу)))
        {
            int t = listToMix[i];
            int randInd = UnityEngine.Random.Range(i, listToMix.Count);
            listToMix[i] = listToMix[randInd];
            listToMix[randInd] = t;
        }
    }
    public void EventIgnite()
    {
        FenceOffSeq?.Invoke();
    }
    private void Start()
    {
        LevelStart();
        Music.Play();
    }

    #region LevelManager

    int currentFenceType;
    int currentColorType;
    bool LevelStarted = false;
    public void LevelStart()
    {
        StartCoroutine(ScorePlusOnTimeCo());
        StartCoroutine(RoundSwitch());
        Debug.Log("Начал лвл");
        UniqeNumbersList = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            UniqeNumbersList.Add(i);
        }
        Mix(UniqeNumbersList);
        if (PlayerPrefs.HasKey("TotalScore"))
        {
            TotalScore = PlayerPrefs.GetInt("TotalScore");
        }
        StartCoroutine(FenceSwitch());
    }
    public void RoundAnimation()
    {
        Round += 1;
        CurrentRoundText.text = "Round: " + Round;
        RoundText.text = "Round\n" + Round;
        RoundAnimator.SetTrigger("RoundTrigger");
    }
    IEnumerator RoundSwitch()
    {
        RoundAnimation();
        GameStartSound.Play();
        BonusSpawn();
        if(Round >= 5)
        {
            CompatibleFenceArray[0].SetActive(true); // Подключаем экстра препятствие
        }
        if (Round > PlayerPrefs.GetInt("BestRounds"))
        {
            PlayerPrefs.SetInt("BestRounds", Round);
        }
        yield return new WaitForSeconds(RoundTime);
        ScorePlus(300);
        StartCoroutine(RoundSwitch());
    }
    IEnumerator FenceSwitch()
    {
        if (LevelStarted)
        {
            FenceOffSeq();
        }
        LevelStarted = true;
        int i = 0;
        while (i == currentFenceType)
        {
            i = UnityEngine.Random.RandomRange(0, FenceArray.Length);
        }
        Debug.Log(i);
        ColorSwitch();
        yield return new WaitForSeconds(7f);
        FenceArray[i].SetActive(true);
        currentFenceType = i;
        yield return new WaitForSeconds(FenceTime);
        StartCoroutine(FenceSwitch());
    }
    IEnumerator ScorePlusOnTimeCo()
    {
        yield return new WaitForSeconds(ScorePlusTimer);
        ScorePlus(TimeScoreToPlus);
        StartCoroutine(ScorePlusOnTimeCo());
    }
    void ColorSwitch()
    {
        int i = UnityEngine.Random.RandomRange(0, ColorVariants.Length);
        if(i != currentColorType)
        {
            LevelColor = ColorVariants[i];
            BackgroundLight.color = LevelColor;
        }
        else
        {
            ColorSwitch();
        }
    }
    #endregion

    [HideInInspector]
    public int EnemyColor;

    public void EnemySpawn(int i)
    {
        StartCoroutine("EnemySpawnCo", i);
    }
    IEnumerator EnemySpawnCo(int i)
    {
        int x = UnityEngine.Random.RandomRange(0, SpwanPositions.Length);
        //SpawnAnimators[x].SetTrigger("Spawn");
        yield return new WaitForSeconds(0.5f);
        Instantiate(EnemyToSpawn[i], SpwanPositions[x].position, Quaternion.identity);
        EnemyColor = i;
    }
    #region PlayerExperienceManager
   
   public void  HPManagerStarter(bool b, int i)
    {
        if (!tgmMode) // Неуяз для тестов
        {
            StartCoroutine(HPManagerCo(b, i));
        }
        
    }
    IEnumerator HPManagerCo(bool Plus, int Amount)
    {
        if (Plus)
        {
            HP += Amount;
        }
        else
        {
            HpBarAnimator.SetTrigger("Hit");
            HP -= Amount;
            PlayerSounds[0].Play();
            Vibrate();
        }
        UIUpdate();
        yield return null;
        if(HP <= 0)
        {
            StartCoroutine(TicketLossCo());
        }
    }
    
    IEnumerator TicketLossCo()
    {
        PlayerSounds[1].Play();
        if (Tickets >= 1)
        {
            HP = 100;
            Tickets -= 1;
            Debug.Log("TicketSwitch");
            switch (Tickets)
            {          
                case 2:
                    Debug.Log("D3");
                    TicketsArray[0].SetActive(false);
                    break;
                case 1:
                    Debug.Log("D2");
                    TicketsArray[2].SetActive(false);
                    break;
                case 0:
                    Debug.Log("D1");
                    TicketsArray[1].SetActive(false);
                    break;
            }
        }
        else
        {
            HP = 0;
            Tickets = 0;
            foreach(GameObject obj in TicketsArray)
            {
                obj.SetActive(false);
            }
            foreach(GameObject obj in FenceArray)
            {
                obj.SetActive(false);
            }
            Debug.Log("GameOver");
            StopCoroutine(ScorePlusOnTimeCo());
            GameOver = true;
            GameOverSeq();
        }
        UIUpdate();
        int i = TotalScore += Score;
        yield return null;
    }
    public void GameOverSeq()
    {
        GameOverPanel.SetActive(true);
        SC.GameScore(Score, Round);
        Music.Stop();
    }
    void Vibrate()
    {
        if (VibroOn)
        {
           // Debug.Log("Вибрирую");
            VibroManager.Vibrate(250);
        }
        else
        {
            //Debug.Log("Не Вибрирую");
        }
    }
    void UIUpdate()
    {
        HpBar.value = HP;
        ScoreHpTicket[0].text = "Score: " + Score;
        ScoreHpTicket[1].text = HP.ToString(); //"HP: "
        //ScoreHpTicket[2].text = "Tickets: " + Tickets;
    }
    void ScorePlus(int ScoreToPlus)
    {
        Score += ScoreToPlus;
        UIUpdate();
    }
    #endregion


    #region Bonus

    IEnumerator BonusSpawnCo()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(Bonus, SpwanPositions[UnityEngine.Random.RandomRange(0, SpwanPositions.Length)].position, Quaternion.identity);
    }
    void BonusSpawn()
    {
        StartCoroutine(BonusSpawnCo());
    }
    void BonusPickup()
    {
        PickupSound.Play();
        ScorePlus(500);
    }
    #endregion

    ////////// ТЕХ ОТДЕЛ

    bool tgmMode = false;
    public void tgm()
    {
        tgmMode = !tgmMode;
    }
    public void PREFSCLEANER()
    {
        PlayerPrefs.SetInt("TotalScore", 0);
        PlayerPrefs.SetInt("BestRounds", 0);
    }
}