using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    float Count = 0.0f;
    public Text myText;
    public bool over = false;
    // Update is called once per frame
    void Update()
    {
        Count += Time.deltaTime;
        string minutes = ((int)Count / 60).ToString();
        string seconds = (Count % 60).ToString("f2");
        myText.text = "Alive for: " + minutes + "." + seconds;

    }
}
