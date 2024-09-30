using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
//using UnityEngine.Networking.we

public class ModelCore : MonoBehaviour
{
    public static ModelCore s_Instance;
    
    private readonly HttpClient _httpClient = new HttpClient();
    private string _baseUrl = "https://verdictum-f8gt3.ondigitalocean.app"; // Replace with your actual host URL

    private void Awake()
    {
        s_Instance = this;
    }

    // Method to set the base URL
    public void SetBaseUrl(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    // Method to call generate_plot
    public async UniTask<string> GeneratePlotAsync(string token, string prompt)
    {
        var requestBody = new
        {
            token = token,
            prompt = prompt
        };

        return await PostRequestAsync("/v1/generate_plot", requestBody);
    }

    // Method to call say
    public async UniTask<string> SpeakWithModelAsync(string modelId, string prompt)
    {
        var requestBody = new
        {
            model_id = modelId,
            prompt = prompt
        };

        return await PostRequestAsync("/v1/say", requestBody);
    }

    // Helper method to send POST requests
    private async UniTask<string> PostRequestAsync(string endpoint, object requestBody)
    {
        var jsonContent = JsonConvert.SerializeObject(requestBody);
        //var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        //var response = await _httpClient.PostAsync(_baseUrl + endpoint, content);
        
        using (UnityWebRequest www = UnityWebRequest.Post(_baseUrl + endpoint, jsonContent, "application/json"))
        {
            await www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                throw new Exception($"API Request Failed: {www.result}");
            }
            else
            {
                return www.downloadHandler.text;
            }
        }
    }
}
