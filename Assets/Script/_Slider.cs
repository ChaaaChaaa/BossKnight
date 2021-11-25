using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Slider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text sliderValue;

    private void Start()
    {
        slider = GetComponent<Slider>();
        sliderValue = transform.Find("Text").GetComponent<Text>();
    }

    public void SetSlider(int _max, int _current)
    {
        slider.maxValue = _max;
        slider.value = _current;

        SetText();
    }

    void SetText()
    {
        sliderValue.text = slider.value + " / " + slider.maxValue;
    }

    public void Increase(int _value)
    {
        slider.value += _value;
        SetText();
    }
    
    public void Decrease(int _value)
    {
        slider.value -= _value;
        SetText();
    }
    
}
