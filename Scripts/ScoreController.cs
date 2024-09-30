using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [Header("Test")]
    int CurrentScore;

    [Header("UI")]
    public TextMeshProUGUI PlayerScore;
    public TextMeshProUGUI PlayerRound;
    public TextMeshProUGUI[] PlayersTextArray;



    [Header("Logic")]
    public List<int> ScoreArray;
    public List<int> PlacesArray;
    public List<String> PlayersNamesArray;


    public void GameScore(int Score, int Round)
    {
        CurrentScore = Score;
        ScoreSave();
        PlayerScore.text = "SCORE: " + Score;
        PlayerRound.text = "ROUND: " + Round;
        Time.timeScale = 0;
        ScoreRandomiser();
        ShufflePlayersNames(PlayersNamesArray);
        ScoreArray[0] = CurrentScore;
        ScoreArray.Sort();
        ScoreArray.Reverse();
        Texter();
    }
    void ScoreSave()
    {
        int i = PlayerPrefs.GetInt("TotalScore") + CurrentScore;
        Debug.Log("current score: " + CurrentScore + "TotalScore: " + PlayerPrefs.GetInt("TotalScore"));
        PlayerPrefs.SetInt("TotalScore", i);
        Debug.Log("NewTotalScore: " + PlayerPrefs.GetInt("TotalScore"));
    }
    void Texter()
    {
        for (int i = 0; i < ScoreArray.Count; i++)
        {
            PlayersTextArray[i].text = PlacesArray[i].ToString() + ". " + PlayersNamesArray[i] + ": " + ScoreArray[i];
        }
        PlayersTextArray[ScoreArray.IndexOf(CurrentScore)].text = PlacesArray[ScoreArray.IndexOf(CurrentScore)].ToString() + ". Player: " + ScoreArray[ScoreArray.IndexOf(CurrentScore)];
    }
    int Randomizer(int min, int max)
    {
        return Mathf.RoundToInt(UnityEngine.Random.Range(min, max) / 100) * 100;
    }
    void ScoreRandomiser()
    {
        int Max = CurrentScore + 300;
        int Min = CurrentScore - 1000;
        if(Min < 0)
        {
            Min = 0;
        }
        for (int i = 0; i < ScoreArray.Count; i++)
        {
            int x = Randomizer(Min, Max);

            while (x == CurrentScore)
            {
                x = Randomizer(Min, Max);
            }
            ScoreArray[i] = x;
        }
    }
    void ShufflePlayersNames(List<String> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            String temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
