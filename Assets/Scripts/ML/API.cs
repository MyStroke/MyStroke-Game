using UnityEngine; 
using System.Collections;
using System;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;
public class API: MonoBehaviour {
    [SerializeField] TextMeshProUGUI PredictionText;
    private const string URL = "localhost:8000/predict";
    private string[] labels = {"Bare", "Fist", "Index-Off", "Ring-Off", "Thumb-Off"};
    public void GenerateRequest (byte[] ImageByte) {
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", ImageByte, "image.png", "image/png");
        StartCoroutine (ProcessRequest (URL, form));
    }

    private IEnumerator ProcessRequest (string uri, WWWForm Images) {
        using (UnityWebRequest request = UnityWebRequest.Post (uri,Images)) {
            request.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return request.SendWebRequest ();

            if (request.isNetworkError) {
                Debug.Log (request.error);
            } else {
                JSONNode prediction = JSON.Parse(request.downloadHandler.text);
                string pred = labels[(int) prediction["argmax"]];
                PredictionText.text = pred;
                Debug.Log((string) prediction["argmax"]+" - "+pred);
            }
        }
    }
}