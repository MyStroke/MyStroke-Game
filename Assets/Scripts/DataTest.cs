
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DataTest : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerData plrdata {get; set;}
    private Button button;
    void Start()
    {
        plrdata = gameObject.GetComponent<PlayerData>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(UpdateScore);
    }

    public void UpdateScore()
    {
        plrdata.Score = (uint) Random.Range(0,100);
        plrdata.Time = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        plrdata.SendProfileToServer();
    }
    // Update is called once per frame

}
