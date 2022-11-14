using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWall : MonoBehaviour
{

    private Rigidbody rigidBodyComponent;


    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBodyComponent.velocity = new Vector3(0, 0, 4);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Playa")
        {
            GoToGameOver();
        }
        else if (collision.gameObject.name != "FloorTile")
            Destroy(collision.gameObject);      
    }

    private void GoToGameOver()
    {
        SceneManager.LoadScene(2);
    }
}
