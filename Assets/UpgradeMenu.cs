using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public UpgradeMenu upgradeMenu;
    public static bool GameIsPaused = false;
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        gameObject.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1;
    }

}
