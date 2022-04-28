using System.Collections;
using System.Collections.Generic;
using UnityEngine;//allows us to connect to unity
using UnityEngine.SceneManagement;//allows us to change and manipulate scenes

public class MenuHandler : MonoBehaviour
{
    //Change Scene based off the scene index value
    public void ChangeScene(int sceneIndex)
    {
        //using Scene Manager load the scene that corresponds to the scene index int
        SceneManager.LoadScene(sceneIndex);
    }
    //Quit our game
    public void QuitGame()
    {
        //#if is developer code...if does not get built into the released product
        //#if UNITY_EDITOR means if we are developing in unity we can use this code
#if UNITY_EDITOR
        //If unity is playing when we activate this function, then get our of playmode
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        //Quits the application
        Application.Quit();
    }
}
