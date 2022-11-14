using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{
    [SerializeField] private AudioSource youWinSound = null;
    void Start()
    {
        youWinSound.Play();
    }
    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
