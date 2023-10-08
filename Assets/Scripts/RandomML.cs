using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Sprite [] dictimage;
    public Image image;

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
        int randomIndex = RandomML2();
        randomLabel = labels[randomIndex];
        TextML.text = randomLabel;
        image.sprite = dictimage[randomIndex];
    }

    private int RandomML2()
    {
        int index = rnd.Next(labels.Length);
        return index;
    }
}
