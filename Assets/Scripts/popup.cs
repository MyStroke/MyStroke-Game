using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popup : MonoBehaviour
{
    public GameObject box;

    private Countdown countdown;

    private void Start()
    {
        countdown = FindObjectOfType<Countdown>();
        box.SetActive(false);
    }

    void Update()
    {
        
    }

    public void popupbox()
    {
        box.SetActive(true);
        countdown.TimerOn = true;
    }

}
