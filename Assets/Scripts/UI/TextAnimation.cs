using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;
    [SerializeField] private float transitionEachCharDuration;
    [SerializeField] private int printingAmountSymbolsPerTransition = 1;
    [Space]
    [SerializeField] private AudioSource audioSource;
    
    private string _text;
    private void Awake()
    {
        if(string.IsNullOrEmpty(tmpText.text))
            return;
        
        _text = tmpText.text;
        tmpText.text = "";
    }

    [ContextMenu("Play")]
    public async void ShowAnimationText()
    {
        await ShowAnimationText(_text);
    }
    
    public async UniTask ShowAnimationText(string text)
    {
        _text = text;
        
        var printedAmountSymbols = 0;
        tmpText.text = "";
        
        for (int i = 0; i < _text.Length; i++)
        {
            tmpText.text += _text[i];
            printedAmountSymbols++;

            if (printedAmountSymbols >= printingAmountSymbolsPerTransition)
                printedAmountSymbols = 0;
            else 
                continue;

            if (!audioSource.isPlaying)
                audioSource.Play();
            
            await UniTask.WaitForSeconds(transitionEachCharDuration);
        }
        
        audioSource.Stop();
    }
}
