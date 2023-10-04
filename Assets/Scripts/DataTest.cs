
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        plrdata.Score = 10;
        plrdata.Acc = 30;
        plrdata.SendProfileToServer();
    }
    // Update is called once per frame

}
