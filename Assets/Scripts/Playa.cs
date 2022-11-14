using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playa : MonoBehaviour
{
	[SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private Transform swordSlashTransform = null;
	[SerializeField] private GameObject swordSlashObject = null;
    [SerializeField] private AudioSource slashSoundOne = null;
    [SerializeField] private AudioSource objectDestroyedSound = null;
    [SerializeField] private AudioSource jumpSound = null;
    private bool jumpKeyWasPressed;
	private float horizontalInput;
	private Rigidbody rigidBodyComponent;
	private float zedInput;
    private const string doorFinal = "DoorFinal";
    private const string doorFinalSecret = "DoorFinalSecret";
    private string[] destroyableObjects = { 
        "Door",
        "DoorOne",
        "DoorTwo",
        "DoorThree",
        "DoorFour",
        "Box",
        "BoxTwo",
        "BoxThree",
        "BoxFour",
        "BoxFive"
    };
    private string[] gameCompleteObjects = {
    doorFinal,
    doorFinalSecret
    };
    private int frameCount;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
		swordSlashObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;

        if(swordSlashObject.activeInHierarchy && frameCount % 10 == 0)
        {
            swordSlashObject.SetActive(false);
        }
        //if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
		{
			jumpKeyWasPressed = true;		
		}
		//if primary mouse button clicked
		if (Input.GetMouseButtonDown(0))
		{
			SwordSlashPressed();
        }
        horizontalInput = Input.GetAxis("Horizontal") * 4;
		zedInput = Input.GetAxis("Vertical") * 4;		
    }
	
	//FixedUpdate is called once every physic update
	private void FixedUpdate()
	{
        //To check if groundCheckTransform is colliding with only 1 object
        if (Physics.OverlapSphere(groundCheckTransform.position,1f).Length == 1)
		{
			return;
		}

        //jump action physics
        if (jumpKeyWasPressed)
		{
			rigidBodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpSound.Play();
            jumpKeyWasPressed = false;
		}


        rigidBodyComponent.velocity = new Vector3(horizontalInput,rigidBodyComponent.velocity.y,zedInput);
		rigidBodyComponent.velocity = rigidBodyComponent.transform.TransformDirection(rigidBodyComponent.velocity);
    }

    /// <summary>
    /// Increase slash count
    /// Rotate slash
    /// Destroy objects
    /// </summary>
	private void SwordSlashPressed()
	{
        swordSlashObject.SetActive(true);
        slashSoundOne.Play();
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = Random.Range(-90.0f, 90.0f);
        swordSlashObject.transform.rotation = Quaternion.Euler(rotationVector);

        Collider[] colliderArray = Physics.OverlapSphere(swordSlashTransform.position, .1f);
        if (colliderArray != null)
        {
            foreach (Collider c in colliderArray)
            {
                if (destroyableObjects.Contains(c.gameObject.name))
                {
                    objectDestroyedSound.Play();
                    Destroy(c.gameObject);
                }
                else if(gameCompleteObjects.Contains(c.gameObject.name))
                {             
                    if (c.gameObject.name == doorFinal)
                    {
                        SceneManager.LoadScene(3);
                    }
                    else if (c.gameObject.name == doorFinalSecret)
                    {
                        SceneManager.LoadScene(4);
                    }
                }
            }
        }
        colliderArray = null;
    }
}
