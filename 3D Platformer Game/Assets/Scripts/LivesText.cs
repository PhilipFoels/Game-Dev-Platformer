using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesText : MonoBehaviour
{
    public GameObject Player;
    public Text livesText;
    public int _livesLeft;

    public void Start()
    {
        
    }
   

    // Update is called once per frame
    void Update()
    {
        _livesLeft = Player.GetComponent<Movement>().livesLeft;
        livesText.text = "Lives left: " + _livesLeft.ToString();
    }
}
