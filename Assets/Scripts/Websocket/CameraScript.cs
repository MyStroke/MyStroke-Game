using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraScript : MonoBehaviour
{   
    public WebCamTexture tex;
    public float[] input;
    public PredictionClient client = new PredictionClient();
    private string prediction = "Pred";

    private RawImage _rawImage;
    private Button Pred_Button;
    private TextMeshProUGUI PredictionText;

    void Start()
    {
        _rawImage = GameObject.Find("RawImage").GetComponent<RawImage>();
        Pred_Button = GameObject.Find("Toggle").GetComponent<Button>();
        PredictionText = GameObject.Find("Predictions").GetComponent<TextMeshProUGUI>();

        Pred_Button.onClick.AddListener(Predict);

        WebCamDevice[] devices = WebCamTexture.devices;

        // for debugging purposes, prints available devices to the console
        for (int i = 0; i < devices.Length; i++)
        {
            print("Webcam available: " + devices[i].name);
        }

        //Renderer rend = this.GetComponentInChildren<Renderer>();

        // assuming the first available WebCam is desired

        tex = new WebCamTexture(devices[0].name);
        //rend.material.mainTexture = tex;
        this._rawImage.texture = tex;
        tex.Play();
    }

    void Update()
    {
        PredictionText.text = prediction;
        
    }

    
    private void Predict()
    {
        // print("predicted");
        input = new float[tex.width * tex.height];
        print(tex.width + " " +tex.height);
        // print("Input"+input);
        if (client) {
            client.Predict(input, output , error);
        } else {
            Debug.Log("No Client found");
        }
    }

    private void output(float[] output)
    {
        var outputMax = output.Max();
        var maxIndex = Array.IndexOf(output, outputMax);
        prediction = "Prediction: " + Convert.ToChar(64 + maxIndex);
    }

    private void error(Exception exception)
    {
        print(exception);
    }
}
