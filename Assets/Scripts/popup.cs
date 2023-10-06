using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popup : MonoBehaviour
{
    public GameObject box;

    private Countdown countdown;
    private GameManager gameManager;
    private RandomML randomML;

    private void Start()
    {
        countdown = FindObjectOfType<Countdown>();
        gameManager = FindObjectOfType<GameManager>();
        randomML = FindObjectOfType<RandomML>();

        box.SetActive(false);
    }

    void Update()
    {
        
    }

    public void popupbox()
    {
        box.SetActive(true);
        gameManager.enabled = false;
        countdown.TimerOn = true;
        randomML.RandomMLBox();
    }

}
