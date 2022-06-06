using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{

    public AudioClip wrongAnswer;
    public AudioClip clearWorkField;
    public AudioClip setItem;
    public AudioClip win;
    public AudioSource audioSource;

    public static Sounds soundsClass;

    private void Awake()
    {
        if (soundsClass == null) soundsClass = this;
    }

    public void playWrongAnswer()
    {
        audioSource.clip = wrongAnswer;
        audioSource.Play();
    }

    public void playClearWorkField()
    {
        audioSource.clip = clearWorkField;
        audioSource.Play();
    }

    public void playSetItem()
    {
        audioSource.clip = setItem;
        audioSource.Play();
    }

    public void playWin()
    {
        audioSource.clip = win;
        audioSource.Play();
    }
}


