using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class VerdictumController : MonoBehaviour
{
    [SerializeField] private CanvasGroup modalWindow;
    [SerializeField] private ConsoleButton personButton;
    [SerializeField] private Transform personasContainer;
    [Space] 
    [SerializeField] private TMP_Text statusOfEnding;
    [SerializeField] private TextAnimation descriptionOfEnding;

    private GameData _loadedGameWorld;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        IdeaCreator.s_OnLoadedGameWorld += ShowListOfPersonasToProsecute;
    }

    private void ShowListOfPersonasToProsecute(GameData gameData)
    {
        _loadedGameWorld = gameData;
        
        foreach (var eachLocation in gameData.locations)
        {
            foreach (var character in eachLocation.characters)
            {
                var createdButton = Instantiate(personButton, personasContainer);
                
                createdButton.gameObject.SetActive(true);
                createdButton.SetButtonText(character.name);
                createdButton.OnConsoleButtonPressed.AddListener(() => ProsecuteCharacter(character));
            }
        }
    }

    private async void ProsecuteCharacter(Character character)
    {
        modalWindow.blocksRaycasts = false;
        await UniTask.WaitForSeconds(0.3f);
        _audioSource.Play();
        
        for (int i = 1; i <= 5; i++)
        {
            modalWindow.alpha -= 0.2f;
            await UniTask.WaitForSeconds(0.3f);
        }
        
        modalWindow.alpha = 0;
        await UniTask.WaitForSeconds(0.3f);
        
        statusOfEnding.text = character.is_criminal ? "Success!" : "Failure";
        descriptionOfEnding.ShowAnimationText(_loadedGameWorld.truth);
    }
}
