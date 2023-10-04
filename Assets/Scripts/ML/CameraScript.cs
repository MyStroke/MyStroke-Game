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
    private WebCamTexture tex;
    private Color32[] input;
    private byte[] input_byte;
    public API api;
    public bool Black;
    public string dirPath;
    [SerializeField] RawImage _rawImage;
    [SerializeField] Button Pred_Button;
    
    void Start()
    {
        dirPath = Application.dataPath + "/../SaveImages/";

        if(!System.IO.Directory.Exists(dirPath)) {
            System.IO.Directory.CreateDirectory(dirPath);
        }

        CameraCheck();
        StartCoroutine(Predict());
    }

    void Update()
    {
    
    }

    private IEnumerator Predict()
    {
        while(true)
        {
            
            // print("predicted");
            // input = new Color32[tex.width * tex.height];
            Debug.Log("shape "+ tex.width + " " +tex.height);
            // input_byte = Color32ArrayToByteArray(input);
            // foreach( var bytee in input_byte ) { Debug.Log( bytee ); }
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(1f);

            Texture2D frame = new Texture2D(tex.width, tex.height);
            frame.SetPixels32(tex.GetPixels32());
            // Debug.Log("Black :"+Black);
            frame.Apply();
            // Black = IsImageMostlyBlack(frame);

            // if(Black == true)
            // {
            //     CameraCheck();
            // }
            
            // Convert the frame to a byte array (PNG format in this case)
            byte[] imageBytes = frame.EncodeToPNG();
            System.IO.File.WriteAllBytes(dirPath + "Image" + ".png", imageBytes);
            api.GenerateRequest(imageBytes);
        }
    }
    private void CameraCheck()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        // for debugging purposes, prints available devices to the console
        for (int i = 0; i < devices.Length; i++)
        {
            print("Webcam available: " + devices[i].name);
        }

        //Renderer rend = this.GetComponentInChildren<Renderer>();

        // assuming the first available WebCam is desired

        tex = new WebCamTexture(devices[0].name,320,240,30);
        Debug.Log("Use "+devices[0].name);
        //rend.material.mainTexture = tex;
        this._rawImage.texture = tex;
        tex.Play();
    }

    // public bool IsImageMostlyBlack(Texture2D texture, float blackThreshold = 1.0f)
    // {
    //     // Create a new Texture2D and load the image data.

    //     int width = texture.width;
    //     int height = texture.height;
    //     // Initialize counters for black and total pixels.
    //     int blackPixelCount = 0;
    //     int totalPixelCount = width * height;

    //     // Loop through each pixel.
    //     for (int x = 0; x < width; x++)
    //     {
    //         for (int y = 0; y < height; y++)
    //         {
    //             // Get the color of the pixel at (x, y).
    //             Color pixelColor = texture.GetPixel(x, y);
    //             // Check if the pixel is black (you can adjust the threshold as needed).
    //             if (pixelColor.r < blackThreshold && pixelColor.g < blackThreshold && pixelColor.b < blackThreshold)
    //             {
                    
    //                 blackPixelCount++;
    //             }
            
                
    //         }
    //     }

    //     // Calculate the percentage of black pixels.
    //     float blackPercentage = (float)blackPixelCount / totalPixelCount;
    //     Debug.Log(blackPercentage);

    //     // You can adjust the threshold percentage as needed.
    //     return blackPercentage >= blackThreshold;
    // }
}
