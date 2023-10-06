using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomML : MonoBehaviour
{
    private static string[] labels = { "Bare", "Fist", "Index-Off", "Ring-Off", "Thumb-Off" };
    private static System.Random rnd = new System.Random();
    public string randomLabel;

    // import all files
    private API data;
    private GameManager gameManager;

    // TextUI for ML
    public TextMeshProUGUI TextML;

    void Start()
    {
        data = FindObjectOfType<API>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
    }

    public void RandomMLBox()
    {
        randomLabel = RandomString();
        TextML.text = randomLabel;
    }


    private static string RandomString()
    {
        int index = rnd.Next(labels.Length);
        return labels[index];
    }
}
