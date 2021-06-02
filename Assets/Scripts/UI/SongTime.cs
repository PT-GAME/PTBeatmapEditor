using System;
using UnityEngine;
using TMPro;

public class SongTime : MonoBehaviour
{
    public AudioSource audioSource;
    public TMP_Text text;
    private float _nowTime;

    private int _mSec;
    private int _sec;
    private int _min;
    private int _hour;

    public bool changed;

    private void Start()
    {
        _nowTime = (float) (Math.Truncate(audioSource.time * 1) / 1);
    }

    private void Update()
    {
        if (changed) return;
        UpdateTime(audioSource.time);
    }

    public void UpdateTime(float songTime, bool changeAudioSourceTime = true)
    {
        if (changeAudioSourceTime)
            audioSource.time = _nowTime = songTime;
        else
        {
            _nowTime = songTime;
        }

        _mSec = (int) ((_nowTime - (int) _nowTime) * 100);
        _sec = (int) (_nowTime % 60);
        _min = (int) (_nowTime / 60 % 60);
        _hour = (int) (_nowTime / 60 / 60 % 60);

        text.text = $"{_hour:00}:{_min:00}:{_sec:00};{_mSec:00}";
    }
}