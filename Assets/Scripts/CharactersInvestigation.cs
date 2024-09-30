using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharactersInvestigation : MonoBehaviour
{
    [Header("Characters cards")]
    [SerializeField] private CardCharacterInfo cardTemplate;
    [SerializeField] private Transform cardContainer;
    [Space]
    [Header("Dossier window")]
    [SerializeField] private WindowView dossierWindow;
    [SerializeField] private Image characterImg;
    [SerializeField] private TMP_Text chatTitle; 
    [SerializeField] private TMP_Text nameText; 
    [SerializeField] private TMP_Text sexText; 
    [SerializeField] private TMP_Text ageText; 
    [SerializeField] private TMP_Text roleText; 
    [SerializeField] private TextAnimation characterDescriptionText; 
    
    private void Awake()
    {
        IdeaCreator.s_OnLoadedGameWorld += ShowCharactersList;
    }

    private void ShowCharactersList(GameData gameData)
    {
        foreach (var eachLocation in gameData.locations)
        {
            foreach (var character in eachLocation.characters)
            {
                var createdCard = Instantiate(cardTemplate, cardContainer);
                
                createdCard.gameObject.SetActive(true);
                createdCard.ShowCharacterInfo(character);
                createdCard.OnClickedCharacter += ShowDossierOfCharacter;
            }
        }
    }

    private void ShowDossierOfCharacter(Character character, Sprite characterPicture)
    {
        dossierWindow.ClosePreviousAndShowThisWindow();

        characterImg.sprite = characterPicture;
        chatTitle.text = $"{character.name.Split(" ")[0]}'s interrogation";
        nameText.text = $"Name: <color=#A1580A>{character.name}</color>";
        sexText.text = $"Sex: <color=#A1580A>{character.sex}</color>";
        ageText.text = $"Age: <color=#A1580A>{character.age}</color>";
        roleText.text = $"Role: <color=#A1580A>{character.role}</color>";
        _ = characterDescriptionText.ShowAnimationText(character.appearance);
    }
}
