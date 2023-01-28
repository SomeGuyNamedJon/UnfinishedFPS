using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene("MainScene");
    }

    public void Quit(){
        Application.Quit();
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Quit();
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            Play();
        }
    }
}
