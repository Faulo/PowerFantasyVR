using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{

    public string screenshot;
    public int x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        
        ScreenCapture.CaptureScreenshot(screenshot, 4);
        screenshot = "screenshot" + x;
        x += 1;
        Debug.Log("Screenshot" + x + " done!");
    }
}
