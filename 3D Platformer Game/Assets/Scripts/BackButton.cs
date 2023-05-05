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
	public Pause HelpTrueBool;
    void Start () {
		Button btn = backButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		GameObject g = GameObject.FindGameObjectWithTag("TrueBoolKeeper");
		HelpTrueBool = g.GetComponent<Pause>();
	}
	void TaskOnClick(){
		StartUI.SetActive(true);
		HelpPanel.SetActive(false);
		HelpTrueBool.HelpTrue = false;
	}
}