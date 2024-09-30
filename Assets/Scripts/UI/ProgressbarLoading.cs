using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ProgressbarLoading : MonoBehaviour
{
    [SerializeField] private TMP_Text progressText;

    private readonly int k_percentageProgressPerSegment = 100 / k_segmentsForFullLoadingAmount;
    private const int k_segmentsForFullLoadingAmount = 20;
    private const int k_secondsPerSegment = 2;
    
    [ContextMenu("Play")]
    public async void ShowProgressbarAnimation()
    {
        for (int i = 0; i <= k_segmentsForFullLoadingAmount; i++)
        {
            var progress = "";
            for (int j = 1; j <= i; j++)
                progress += "#";

            for (int j = 1; j <= k_segmentsForFullLoadingAmount - i; j++)
                progress += "_";
            
            progressText.text = $">> Loading [{progress}] {k_percentageProgressPerSegment * i} %";
            await UniTask.WaitForSeconds(k_secondsPerSegment);
        }
    }
}
