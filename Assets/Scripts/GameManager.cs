using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool levelWon = false;
    public static bool gameOver = false;
    public static int levelCnt = 1;

    public static int enemyCountModifier = 1;
    public static float enemySpawnModifier = 2.0f;

    public static int enemyMaxCount = 5;
    public static int enemiesSpawned = 17;

    public GameObject completeLevelUI;
    public GameObject gameOverUI;
    public GameObject hudUI;

    public PlayerMovement movement;

    public MouseLook look;

    void Start(){
        completeLevelUI.SetActive(false);
        if(levelWon == true){
            levelCnt++;
            levelWon = false;
            enemyCountModifier *= 2;
            enemySpawnModifier -= .5f/enemyCountModifier;
            enemyMaxCount += enemyCountModifier;
        }else{
            gameOver = false;
            enemyCountModifier = 1;
            enemySpawnModifier = 2.0f;
            enemyMaxCount = 5;
        }
    }

    void Update(){
        if(enemiesSpawned == 0){
            LevelWin();
        }
    }

    public void GameOver(){
        levelWon = false;
        gameOver = true;
        levelCnt = 1;
        enemyCountModifier = 1;
        enemySpawnModifier = 2.0f;
        enemyMaxCount = 5;
        gameOverUI.SetActive(true);
        hudUI.SetActive(false);
        movement.enabled = false;
        look.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LevelWin(){
        Debug.Log("You win!");
        levelWon = true;
        completeLevelUI.SetActive(true);
        Invoke("NextLevel", 1f);
    }

    void NextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}