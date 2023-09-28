using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.RawImage _rawImage;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        // for debugging purposes, prints available devices to the console
        for (int i = 0; i < devices.Length; i++)
        {
            print("Webcam available: " + devices[i].name);
        }

        //Renderer rend = this.GetComponentInChildren<Renderer>();

        // assuming the first available WebCam is desired

        WebCamTexture tex = new WebCamTexture(devices[0].name);
        //rend.material.mainTexture = tex;
        this._rawImage.texture = tex;
        tex.Play();
    }
}