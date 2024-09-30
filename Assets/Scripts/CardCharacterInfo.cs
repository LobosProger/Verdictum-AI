using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardCharacterInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text nameOfCharacter;
    [SerializeField] private Image personPicture;
    [SerializeField] private Sprite maleSprite;
    [SerializeField] private Sprite femaleSprite;

    public Action<Character, Sprite> OnClickedCharacter;
    
    private Character _capturedCharacter;
    
    public void ShowCharacterInfo(Character character)
    {
        nameOfCharacter.text = character.name;
        personPicture.sprite = character.sex.Contains("female") ? femaleSprite : maleSprite;
        
        _capturedCharacter = character;
        gameObject.GetComponent<ConsoleButton>().OnConsoleButtonPressed.AddListener(() => OnClickedCharacter?.Invoke(_capturedCharacter, personPicture.sprite));
    }
}
