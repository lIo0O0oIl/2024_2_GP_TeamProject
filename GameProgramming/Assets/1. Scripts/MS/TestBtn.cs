using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBtn : MonoBehaviour
{
    public ParticleSystem particle;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            particle.Play();

            ParticleSystem.MainModule main = particle.main;

            ParticleSystem.MinMaxGradient gradient = main.startColor;

            Gradient newGradient = new Gradient();
            newGradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(Color.red, 0f), 
                    gradient.gradient.colorKeys[1] 
                },
                gradient.gradient.alphaKeys 
            );

            main.startColor = new ParticleSystem.MinMaxGradient(newGradient);
        }
    }
}
