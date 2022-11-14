using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private AudioSource deathSound = null;
    [SerializeField] private AudioSource evilLaugh = null;
    void Start()
    {
        deathSound.Play();
        evilLaugh.Play();
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
