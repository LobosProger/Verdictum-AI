using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConsoleButtonToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip switchedOnSound;
    [Space]
    
    public UnityEvent OnToggledOn;
    
    private AudioSource _buttonAudioSource;

    private void Awake()
    {
        if (toggle.isOn)
            toggle.interactable = false;
            
        _buttonAudioSource = GetComponent<AudioSource>();
        toggle.onValueChanged.AddListener(OnSwitchedToggle);
    }

    private async void OnSwitchedToggle(bool isOn)
    {
        if (isOn)
        {
            toggle.interactable = false;
            
            _buttonAudioSource.clip = clickSound;
            _buttonAudioSource.Play();
            
            GlitchEffect.S_instance.ShowGlitchOnScreen();
            await UniTask.WaitForSeconds(0.1f);
            
            _buttonAudioSource.clip = switchedOnSound;
            _buttonAudioSource.Play();
            
            OnToggledOn?.Invoke();
        }
        else
        {
            toggle.interactable = true;
        }
    }
}
