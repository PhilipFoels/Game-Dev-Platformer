using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpButton : MonoBehaviour
{	
	public Button helpButton;

    void Start () {
		Button btn = helpButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
	void TaskOnClick(){
		SceneManager.LoadScene(1);
	}
}
