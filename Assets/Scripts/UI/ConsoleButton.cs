using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsoleButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private bool useSymbolsTxt = true;
    [Space] 
    [SerializeField] private TMP_Text buttonTxt;
    [SerializeField] private TMP_Text symbolsTxt;
    [SerializeField] private AudioClip selectSound;
    [SerializeField] private AudioClip clickSound;
    [Space]
    
    public UnityEvent OnConsoleButtonPressed;
    
    private AudioSource _buttonAudioSource;

    private void Awake()
    {
        _buttonAudioSource = GetComponent<AudioSource>();
        
        if(useSymbolsTxt)
            symbolsTxt.gameObject.SetActive(false);
    }

    public void SetButtonText(string text)
    {   
        buttonTxt.text = text;
    }

    public async void OnPointerClick(PointerEventData eventData)
    {
        _buttonAudioSource.clip = clickSound;
        _buttonAudioSource.Play();

        GlitchEffect.S_instance.ShowGlitchOnScreen();
        await UniTask.WaitForSeconds(0.1f);
        
        OnConsoleButtonPressed?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonAudioSource.clip = selectSound;
        _buttonAudioSource.Play();
        
        if(useSymbolsTxt)
            symbolsTxt.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(useSymbolsTxt)
            symbolsTxt.gameObject.SetActive(false);
    }
}
