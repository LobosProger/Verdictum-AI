using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CaseFileWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text nameOfWorld;
    [SerializeField] private TMP_Text descriptionOfWorld;
    [SerializeField] private ConsoleButton locationBtnTemplate;
    [SerializeField] private Transform locationBtnsContainer;
    [Space]
    [SerializeField] private WindowView locationWindowView;
    [SerializeField] private LayoutGroupFix fixLocationsContent;
    [SerializeField] private TMP_Text nameOfLocation;
    [SerializeField] private TMP_Text descriptionOfLocation;
    
    private void Awake()
    {
        IdeaCreator.s_OnLoadedGameWorld += ShowWorldDataAndLocationsButtons;
    }

    private void ShowWorldDataAndLocationsButtons(GameData gameData)
    {
        nameOfWorld.text = gameData.title;
        descriptionOfWorld.text = gameData.introduction;

        foreach (var eachLocation in gameData.locations)
        {
            var createdBtn = Instantiate(locationBtnTemplate, locationBtnsContainer);
            var selectedLocationByThisBtn = eachLocation;
            
            createdBtn.gameObject.SetActive(true);
            createdBtn.SetButtonText(eachLocation.name);
            createdBtn.OnConsoleButtonPressed.AddListener(() => ShowThisLocation(selectedLocationByThisBtn));
        }
    }

    private void ShowThisLocation(Location location)
    {
        locationWindowView.ClosePreviousAndShowThisWindow();
        nameOfLocation.text = location.name;
        descriptionOfLocation.text = location.description;

        _ = fixLocationsContent.FixLayout();
    }
}