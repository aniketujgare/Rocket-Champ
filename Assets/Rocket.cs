using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionsDisabled = false;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("LoadNextLevel"))
        {
            // do nothing
        }else
            PlayerPrefs.SetInt("LoadNextLevel",0);
        if (PlayerPrefs.HasKey("LoadNextLevelCount"))
        {
            //do nothing
        }
        else
            PlayerPrefs.SetInt("LoadNextLevelCount", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTransitioning)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
        if(Debug.isDebugBuild)
            RespondToDebugKeys(); 
    }

    private void RespondToDebugKeys()
    {

        if (Input.GetKeyDown(KeyCode.L))
            LoadNextLevel();
        else if (Input.GetKeyDown(KeyCode.C))
            collisionsDisabled = !collisionsDisabled;   // toggle
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionsDisabled) { return; } // ignore collisions when dead
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", 1f); // parameterise time
    }

    private void StartDeathSequence()
    {
        isTransitioning = true;
        audioSource.Stop(); 
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", 1f); // parameterise time
    }

    public void LoadNextLevel()
    {
        int previousSceneIndex = PlayerPrefs.GetInt("LoadNextLevel");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int previousLevelCount = PlayerPrefs.GetInt("LoadNextLevelCount");
        if (currentSceneIndex > previousSceneIndex)
        {
            PlayerPrefs.SetInt("LoadNextLevel",SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt("LoadNextLevelCount", previousLevelCount + 1);
            print("level clered count" + PlayerPrefs.GetInt("LoadNextLevelCount"));
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); // todo allow for more than 2 levels
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void RespondToThrustInput()
    {
        
    bool thrust = CrossPlatformInputManager.GetButton("Jump");
       
        print("thrust");
        if ( thrust || Input.GetKey(KeyCode.Space))
        {
            print("thrusting");
            ApplyThrust();
        }
        else
            StopApplyingThrust();
        
    }

    private void StopApplyingThrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust*Time.deltaTime);
        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    /*private void RespondToRotateInput()
    {
        // todo rcsTRhrust set to 500 for this
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotateManually(rcsThrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RotateManually(-rcsThrust * Time.deltaTime );
        }


    }*/

private void RespondToRotateInput ()
    { 
        float xRotate = CrossPlatformInputManager.GetAxis("Horizontal") ;
        if (xRotate < -0.2f || Input.GetKey(KeyCode.A))
        {
            //xRotate = -1f;
            RotateManually(rcsThrust * Time.deltaTime * 1);
        }
        else if (xRotate > 0.2f || Input.GetKey(KeyCode.D))
        {
            //xRotate = 1f;
            RotateManually(-rcsThrust * Time.deltaTime * 1);
        }
    }

    private void RotateManually(float rotationThisFrame)
    {
        rigidBody.freezeRotation = true; // take manual control of rotation
        transform.Rotate(Vector3.forward * rotationThisFrame);
        rigidBody.freezeRotation = false; // resume physics control of rotation
    }
}