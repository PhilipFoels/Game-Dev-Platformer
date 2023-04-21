using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{

    public GameObject Player;
    public Text scoreText;
    public int _score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _score = Player.GetComponent<Movement>().score;
        scoreText.text = "Score: " + _score.ToString();
    }
}
