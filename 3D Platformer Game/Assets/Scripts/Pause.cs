using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{   
    public GameObject pauseMenu;
    public GameObject ResumeButton;
    public GameObject ExitButton; 
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        Button btn = ResumeButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
        Button extbtn = ExitButton.GetComponent<Button>();
        extbtn.onClick.AddListener(ExitOnClick);
    }
    void ExitOnClick(){
        Application.Quit();
    }
    void TaskOnClick(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (pauseMenu.activeSelf == false){
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
            } else {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
