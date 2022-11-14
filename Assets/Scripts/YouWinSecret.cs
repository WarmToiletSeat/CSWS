using UnityEngine;

public class YouWinSecret : MonoBehaviour
{
    [SerializeField] private AudioSource youWinSecretSound = null;
    void Start()
    {
        youWinSecretSound.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
