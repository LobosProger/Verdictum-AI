using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;

[Serializable]
public class Character
{
    public string name;
    public string sex;
    public string age;
    public string role;
    public string appearance;
    public string personality;
    public string evidences;
    public bool is_criminal;
    public string model;
}

[Serializable]
public class Location
{
    public string name;
    public string description;
    public List<Character> characters;
}

[Serializable]
public class GameData
{
    public string title;
    public string introduction;
    public List<Location> locations;
    public string truth;
}

public class IdeaCreator : MonoBehaviour
{
    [SerializeField] private TMP_InputField promptInput;
    [SerializeField] private ConsoleButton generate;
    [Space] 
    [SerializeField] private WindowView mainGameContent;
    [SerializeField] private WindowView caseFileContent;
    [Space]
    [SerializeField] private TextAsset gameDataJson;
    [SerializeField] private bool testMode;
    
    public static Action<GameData> s_OnLoadedGameWorld;
    
    private void Start()
    {
        generate.OnConsoleButtonPressed.AddListener(OnGenerateButtonClicked);

        if (testMode)
        {
            var gameData = JsonConvert.DeserializeObject<GameData>(gameDataJson.text);
            s_OnLoadedGameWorld?.Invoke(gameData);
            
            mainGameContent.ClosePreviousAndShowThisWindow();
            caseFileContent.ShowWindowUponAnotherWindows(true);
        }
    }

    private async void OnGenerateButtonClicked()
    {
        if(string.IsNullOrEmpty(promptInput.text))
            return;
        
        var writtenPrompt = promptInput.text;
        
        promptInput.gameObject.SetActive(false);
        generate.gameObject.SetActive(false);

        var gameWorld = await ModelCore.s_Instance.GeneratePlotAsync((SystemInfo.deviceName + DateTime.Now.Ticks.ToString()).GetHashCode().ToString(),
            writtenPrompt);
        
        var gameData = JsonConvert.DeserializeObject<GameData>(gameWorld);
        s_OnLoadedGameWorld?.Invoke(gameData);
        
        mainGameContent.ClosePreviousAndShowThisWindow();
        caseFileContent.ShowWindowUponAnotherWindows(true);
    }
}
