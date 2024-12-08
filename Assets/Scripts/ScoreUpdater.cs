using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    [Header("Score")]
    //get from player script
    
    [SerializeField] int score = 0;
    [SerializeField] PlayerClassManager player;
    
    [SerializeField] TextMeshProUGUI ScoreText;


    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = player.Gold;
        ScoreText.text = "Gold: " + score;
        
    }
    
}
