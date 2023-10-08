
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DataTest : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerData plrdata;

    void Start()
    {
        plrdata = FindObjectOfType<PlayerData>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void UpdateScore()
    {
        plrdata.Score = (uint) gameManager.scoreValue;
        plrdata.Time = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        plrdata.SendProfileToServer();
    }
}
