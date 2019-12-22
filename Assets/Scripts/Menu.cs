using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Canvas quitMenu;
    public Canvas pauseMenu;
    public Canvas quitMenuAfterPause;
    public Button btnPlay;
    public Button btnExit;
    private Canvas mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = (Canvas)GetComponent<Canvas>(); //pobranie menu głównego
        quitMenu = quitMenu.GetComponent<Canvas>(); //pobranie menu wyjścia z gry
        pauseMenu = pauseMenu.GetComponent<Canvas>(); //pobranie menu pauzy
        quitMenuAfterPause = quitMenuAfterPause.GetComponent<Canvas>(); //pobranie menu wyjścia z gry po pauzie
        btnPlay = btnPlay.GetComponent<Button>();
        btnExit = btnExit.GetComponent<Button>();
        quitMenu.enabled = false; //wyłączenie menu wyjścia z gry
        pauseMenu.enabled = false;
        quitMenuAfterPause.enabled = false;
        Time.timeScale = 0; //zatrzymanie czasu
        Cursor.visible = mainMenu.enabled; //odkrycie kursora myszy
        Cursor.lockState = CursorLockMode.Confined; //odblokowanie kursora myszy
        //przetestować z CursorLockMode.None
        //przetestować bez ostatniej linijki
    }

    // Update is called once per frame
    void Update()
    {
        //tutaj będzie kod dotyczący sprawdzania czy przypadkiem gracz nie wcisnął P aby zapauzować grę
        //ewentualnie w KeyboardMouseController ?
    }

    public void btnPlayPressed()
    {
        mainMenu.enabled = false;
        Time.timeScale = 1; //włączenie czasu
        Cursor.visible = false; //ukrycie kursora
        Cursor.lockState = CursorLockMode.Locked; //zablokowanie kursora
        btnPlay.enabled = true; //aktywacja przycisku btnPlay
    }

    public void btnExitPressed()
    {
        quitMenu.enabled = true; //aktywacja menu wyjścia
        mainMenu.enabled = false; //dezaktywacja menu głównego
        btnPlay.enabled = false; //dezaktywacja btnPlay
        btnExit.enabled = false; //dezaktywacja btnExit
    }

    public void btnYesExitPressed()
    {
        Application.Quit(); //wyjście z gry
    }

    public void btnCancelExitPressed()
    {
        mainMenu.enabled = true; //aktywacja menu głównego
        quitMenu.enabled = false; //dezaktywacja menu wyjścia
        btnPlay.enabled = true; //aktywacja btnPlay
        btnExit.enabled = true; //aktywacja btnExit
    }

    public void showPauseMenu()
    {
        pauseMenu.enabled = true; //aktywacja menu pauzy
        Time.timeScale = 0; //zatrzymanie czasu
        Cursor.visible = true; //odkrycie kursora
        Cursor.lockState = CursorLockMode.Confined; //odblokowanie kursora
    }

    public void btnResumeInPauseMenu()
    {
        pauseMenu.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void btnRestartInPauseMenu()
    {
        pauseMenu.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void btnExitInPauseMenu()
    {
        quitMenuAfterPause.enabled = true; //aktywacja menu wyjścia po pauzie
        pauseMenu.enabled = false; //dezaktywacja menu pauzy
    }

    public void btnYesInQuitMenuAfterPause()
    {
        Application.Quit();
    }

    public void btnCancelInQuitMenuAfterPause()
    {
        pauseMenu.enabled = true; //aktywania menu pauzy
        quitMenuAfterPause.enabled = false; //dezaktywacja menu wyjścia po pauzie
    }
}
