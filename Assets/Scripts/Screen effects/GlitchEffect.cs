using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GlitchEffect : MonoBehaviour
{
    [SerializeField] private Material glitchMaterial;
    
    public static GlitchEffect S_instance { get; private set; }
    
    private bool _isGlitching;
    
    private readonly int k_glitchStrengthProp = Shader.PropertyToID("_Intensity");
    private const float k_transitionGlitchTime = 0.085f;
    private const float k_defaultGlitchStrength = 0.001f;
    private const float k_transitionGlitchStrength = 0.65f;
    
    private void Awake()
    {
        S_instance = this;
        glitchMaterial.SetFloat(k_glitchStrengthProp, k_defaultGlitchStrength);
    }

    public void ShowGlitchOnScreen()
    {
        if(_isGlitching)
            return;

        _isGlitching = true;
        glitchMaterial.DOFloat(k_transitionGlitchStrength, k_glitchStrengthProp, k_transitionGlitchTime).SetEase(Ease.Linear).OnComplete(
            () =>
            {
                glitchMaterial.DOFloat(k_defaultGlitchStrength, k_glitchStrengthProp, k_transitionGlitchTime)
                    .SetEase(Ease.Linear).OnComplete(() => _isGlitching = false); 
            });
    }
}
