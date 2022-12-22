using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class MainMenu : MonoBehaviour
{
    private int GAME_SCENE_INDEX = 1;

    public void PlayGame() 
    {
        SceneManager.LoadScene(GAME_SCENE_INDEX); 
    }
}
