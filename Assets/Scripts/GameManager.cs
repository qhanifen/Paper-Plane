using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoSingleton<GameManager>
{
    public bool gameOver = false;

    PlayerController player;

    MenuPanel menuPanel;

    public enum SceneMode
    {
        MainMenu,
        InGame
    }

    public SceneMode sceneMode;

    void Start()
    {
        if (sceneMode == SceneMode.InGame)
        {
            instance.StartCoroutine(PlayerGravityCountdown());
        }
    }

    public static void StartGame()
    {
        instance.sceneMode = SceneMode.InGame;
        ScoreManager.instance.sceneMode = SceneMode.InGame;
        SceneManager.LoadScene("Test Scene");    
    }

    public static void GameOver()
    {
        Debug.Log("Game Over");
        instance.gameOver = true;

        instance.StartCoroutine(OpenMenu());
    }

    static IEnumerator PlayerGravityCountdown()
    {
        Debug.Log("Starting Countdown");
        yield return new WaitForSeconds(3.0f);
        instance.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        instance.player.GetComponent<Rigidbody>().useGravity = true;
    }

    public static IEnumerator OpenMenu()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(instance.menuPanel);
    }
}
