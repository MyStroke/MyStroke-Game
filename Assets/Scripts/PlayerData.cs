using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using Firebase.Firestore;
using FirebaseWebGLBridge = FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGLUtils = FirebaseWebGL.Examples.Utils;
using FirebaseWebGLObjects = FirebaseWebGL.Scripts.Objects;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using System;



#if !UNITY_WEBGL || UNITY_EDITOR
using Firebase.Firestore;
using Firebase.Extensions;
#endif


// Start is called before the first frame update
[Serializable]
public class PlayerData : MonoBehaviour
{

    FirebaseFirestore db;

    // import all files
    private GameManager gameManager;
    private Dictionary<string, object> newScoreData;

    //  public uint PlayerId { 
    //     get => playerId;
    //     set => playerId = value;
    // }
    // [SerializeField]
    // private uint playerId;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    // Update score from Unity Editor
    public void UpdateScore()
    {
        string newDocumentName = "score_" + System.Guid.NewGuid().ToString();
        DocumentReference docRef = db.Collection("user-score").Document("Doc-score");

        // ดึงข้อมูลปัจจุบันในคอลเลกชัน "score"
        docRef.GetSnapshotAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;

                // สร้างรายการคะแนนใหม่
                Dictionary<string, object> newScoreData = new Dictionary<string, object>
                {
                    { "score", gameManager.scoreValue },
                    { "time", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") }
                };

                Dictionary<string, object> scoreData = snapshot.ToDictionary();
                scoreData.Add(newDocumentName, newScoreData);

                // อัปเดตคอลเลกชัน "score" ใหม่
                docRef.SetAsync(scoreData).ContinueWith(updateTask =>
                {
                    if (updateTask.IsCompleted)
                    {
                        Debug.Log("Score updated");
                    }
                    else
                    {
                        Debug.Log("Score not updated");
                    }
                });
            }
        });
    }

    public uint Score
    {
        get => playerScore;
        set => playerScore = value;
    }

    [SerializeField] private uint playerScore;

    public string Time
    {
        get => time;
        set => time = value;
    }

    [SerializeField] private string time;
    // public PlayerData()
    // {
    //     // var random = new System.Random((int) DateTime.Now.Ticks & 0x0000FFFF);
    //     // PlayerId = (uint) random.Next();
    //     PlayerId = 999;
    // }

    public void SendProfileToServer()
    {
        Debug.Log("Clicked " + this);

#if UNITY_WEBGL || !UNITY_EDITOR
        string jsonData = JsonUtility.ToJson(this);
        FirebaseWebGLBridge.FirebaseFirestore.AddElementInArrayField("user-score", "Doc-score", "score", jsonData, gameObject.name, "DisplayInfo", "DisplayErrorObject");
        Debug.Log("Added data to the Player document in the Gameplay collection. : " + this);

#endif
    }

    // public override string ToString()
    // {
    //     var stringBuilder = new StringBuilder();
    //     stringBuilder.Append("PlayerId = " + playerId);
    //     stringBuilder.Append(" - Score = " + score);
    //     stringBuilder.Append(" - Acc = " + acc);
    //     return stringBuilder.ToString();
    // }

    public void DisplayInfo(string info)
    {
        Debug.Log(info);
    }

    public void DisplayErrorObject(string error)
    {
        var parsedError = FirebaseWebGLUtils.StringSerializationAPI.Deserialize(typeof(FirebaseWebGLObjects.FirebaseError), error) as FirebaseWebGLObjects.FirebaseError;
        Debug.Log(parsedError.message);
    }
}




