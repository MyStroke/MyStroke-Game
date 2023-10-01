using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine; 
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

public class CameraScript : MonoBehaviour
{   
    public WebCamTexture tex;
    public Color32[] input;
    public byte[] input_byte;
    public API api;
    [SerializeField] RawImage _rawImage;
    [SerializeField] Button Pred_Button;
    void Start()
    {
        Pred_Button.onClick.AddListener(Predict);
        WebCamDevice[] devices = WebCamTexture.devices;

        // for debugging purposes, prints available devices to the console
        for (int i = 0; i < devices.Length; i++)
        {
            print("Webcam available: " + devices[i].name);
        }

        //Renderer rend = this.GetComponentInChildren<Renderer>();

        // assuming the first available WebCam is desired

        tex = new WebCamTexture(devices[0].name,224, 224, 30);
        Debug.Log(tex.width + " " +tex.height);
        //rend.material.mainTexture = tex;
        this._rawImage.texture = tex;
        tex.Play();
        Texture2D frame = new Texture2D(tex.width, tex.height);
    }

    void Update()
    {
    
    }

    
    private void Predict()
    {
        // print("predicted");
        // input = new Color32[tex.width * tex.height];
        // print(tex.width + " " +tex.height);
        // input_byte = Color32ArrayToByteArray(input);
        // foreach( var bytee in input_byte ) { Debug.Log( bytee ); }
        Texture2D frame = new Texture2D(tex.width, tex.height);
        frame.SetPixels32(tex.GetPixels32());
        frame.Apply();

        // Convert the frame to a byte array (PNG format in this case)
        byte[] imageBytes = frame.EncodeToPNG();

        api.GenerateRequest(imageBytes);
    }
}
