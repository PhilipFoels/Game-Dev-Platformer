using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpButton : MonoBehaviour
{	
	public Button helpButton;
	public GameObject OriginalUI;
	public GameObject HelpPanel;
	public Pause HelpTrueBool;
    void Start () {
		HelpPanel.SetActive(false);
		Button btn = helpButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		GameObject g = GameObject.FindGameObjectWithTag("TrueBoolKeeper");
		HelpTrueBool = g.GetComponent<Pause>();
	}
	void TaskOnClick(){
		OriginalUI.SetActive(false);
		HelpPanel.SetActive(true);
		HelpTrueBool.HelpTrue = true;
	}
}
