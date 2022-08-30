using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderTest : MonoBehaviour
{
    public Slider slider;
    public Text c_text;
    private float sliderValue;
    private float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        //œﬁ÷°60
        Application.targetFrameRate = 60;
        //≥ı ºªØ
        sliderValue = slider.value;
        sliderValue = 0;
    }

    private void ChangeSlider()
    {
        if(sliderValue < 100)
        {
            sliderValue += 0.02f;
            c_text.text = sliderValue + "%";
            slider.value = sliderValue / 100;
        }
        else
        {
            c_text.text = "OK";
            sliderValue = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeSlider();
    }
}
