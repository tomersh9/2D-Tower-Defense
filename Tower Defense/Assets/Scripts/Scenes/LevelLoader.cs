using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
   public void LoadGameScene() => SceneManager.LoadScene("Level 1");

   public void LoadCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

   public void LoadOptionsScene() => SceneManager.LoadScene("Options");

   public void LoadMainMenuScene() => SceneManager.LoadScene("Menu");
}
