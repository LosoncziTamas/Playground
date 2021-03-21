using System;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Sprite Background;
    public float Speed;
    
    private void Update()
    {
        var horizontalMove = Input.GetAxis("Horizontal");
        if (horizontalMove > 0.0f)
        {
            
        }
        else if (horizontalMove < 0.0f)
        {
            
        }
    }
}
