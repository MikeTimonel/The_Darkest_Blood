using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionCount : MonoBehaviour
{
    public int potions = 0;
    private TextMeshProUGUI potionIndicator;
    
    


    
    void Start()
    {
        potionIndicator = FindObjectOfType<TextMeshProUGUI>();
        
    }

    
    void Update()
    {
        potionIndicator.text = "x" + potions;
        
    }
}
