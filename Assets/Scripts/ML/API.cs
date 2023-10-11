using UnityEngine; 
using System.Collections;
using System;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;
public class API: MonoBehaviour {
    [SerializeField] TextMeshProUGUI PredictionText;
    private const string URL = "http://oofypc9000.thddns.net:2320/predict";
    private string[] labels = {"Ulnar Deviation", "Hand Open", "Hand Close", "Thumb Touches Index Finger", "Wrist Flexion", "Five Fingertips Touch", "Relax Gesture"};
    public void GenerateRequest (byte[] ImageByte) {
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", ImageByte, "image.png", "image/png");
        StartCoroutine (ProcessRequest (URL, form));
    }

    private IEnumerator ProcessRequest (string uri, WWWForm Images) {
        using (UnityWebRequest request = UnityWebRequest.Post (uri,Images)) {
            request.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return request.SendWebRequest ();

            if (request.result == UnityWebRequest.Result.ConnectionError) {
                Debug.Log (request.error);
            } else {
                JSONNode prediction = JSON.Parse(request.downloadHandler.text);
                if (prediction["prediction"] == null) {
                    Debug.Log("Error");
                    PredictionText.text = "Null";
                    yield break;
                }
                string pred = labels[(int) prediction["argmax"]];
                PredictionText.text = pred;
                Debug.Log((string) prediction["argmax"]+" - "+pred);
            }
        }
    }

    public string GetPrediction(){
        return PredictionText.text;
    }
}