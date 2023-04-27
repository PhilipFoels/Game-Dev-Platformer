using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{	
	public Button backButton;
	public GameObject StartUI;
	public GameObject HelpPanel;
    void Start () {
		Button btn = backButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
	void TaskOnClick(){
		StartUI.SetActive(true);
		HelpPanel.SetActive(false);
	}
}