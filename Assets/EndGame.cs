using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void RestartGame (){
        Time.timeScale = 1f; //Velocidad del juego, cuando se reinicia vuelve de 0 a 1
        SceneManager.LoadScene("Game");
    }
    public void PlayGame (){
        Time.timeScale = 1f; //Velocidad del juego, cuando se reinicia vuelve de 0 a 1
        SceneManager.LoadScene("StartMenu");
    }
}
