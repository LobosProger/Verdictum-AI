using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class FrontendModelCore : MonoBehaviour
{
    private string apiKey = "AIzaSyDcFqCG41G63Rjl8o5HaDDacW0C-9GULp0"; 
    private string apiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent"; // Edit it and choose your prefer model

    private async void Start()
    {
        var body = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = "Explain how AI works" }
                    }
                }
            }
        };
        var res = await PostRequestAsync(body);
        
        Debug.Log(res);
    }

    private async UniTask<string> PostRequestAsync(object requestBody)
    {
        var url = $"{apiEndpoint}?key={apiKey}";
        var jsonContent = JsonConvert.SerializeObject(requestBody);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonContent);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            throw new Exception($"API Request Failed: {request.result}");
        }
        else
        {
            return request.downloadHandler.text;
        }
    }
}