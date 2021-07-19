using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

[SelectionBase]
public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem engineParticlesCloud;
    [SerializeField] ParticleSystem engineParticlesFlame;
    public ParticleSystem FX_Win_Launchpad;
    public GameObject vfx;

    private GameObject Landpad;
    private int value = 1;
   // public Transform vfx;
    Vector3 destination = new Vector3(-8.63f, 12.3f, -32.33f);

    Rigidbody rigidBody;
    AudioSource audioSource;
    [Header("Fractured Ship")]
   // public GameObject[] fracturedShips;

    bool isTransitioning = false;
    bool collisionsDisabled = false;

    string LevelNo; //For Flurry
    // Use this for initialization

    private void Awake()
    {
        SoundManager.Initialize();
    }
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        Landpad = GameObject.FindGameObjectWithTag("Finish");
        //Instantiate(FX_Win_Launchpad, Landpad.transform.localPosition, Quaternion.identity);

        //Flurry Event
        LevelNo = SceneManager.GetActiveScene().name;
        EventLogExample.Instance.LevelStart(LevelNo);
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

        if (Input.GetKeyDown(KeyCode.L)) ;
        //Initiate.Fade(SceneManager.GetActiveScene().buildIndex + 1, GameAssets.i.color, 1f); //  LoadNextLevel();
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
              //  GetComponent<GameScene>().CompleteLevel();
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
       Instantiate(FX_Win_Launchpad, Landpad.transform.localPosition, Quaternion.identity);
        Instantiate(vfx, destination, Quaternion.identity);
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        StartCoroutine(LoopAudio());
        
        SoundManager.PlaySound(SoundManager.Sound.WinSound);
         SaveManager.Instance.CompleteLevel(Manager.Insatance.currentLevel);
       // SaveManager.Instance.CompleteLevel(SceneManager.GetActiveScene().buildIndex+2);

        Invoke("CompleteDialogueBoxEnable",3f); // parameterise time
        //Flurry Event
        EventLogExample.Instance.LevelComplete(LevelNo);
    }

    public void StartDeathSequence()
    {
        
        isTransitioning = true;
        value = PlayerPrefs.GetInt("Haptic", 1);
        if (value == 1)
        {
            Handheld.Vibrate();
           // Debug.Log("Vibraitng");
        }
        this.transform.localScale = new Vector3(0f, 0f, 0f);
        this.GetComponent<CapsuleCollider>().enabled = false;
        engineParticlesCloud.Stop();
        engineParticlesFlame.Stop();
        audioSource.Stop();
        // audioSource.PlayOneShot(death);
        SoundManager.PlaySound(SoundManager.Sound.Explode);
        //Instantiate Fractured Ship
        Instantiate(ShipsAssets.Instance.fracturedShip, transform.position, transform.rotation);
        
        //deathParticles.Play();
        Invoke("FailedDialogueBoxEnable", 2f); // parameterise time

        //Flurry Event
        EventLogExample.Instance.LevelLoss(LevelNo);
    }

    public void CompleteDialogueBoxEnable()
    {
        InGamePanels.instance.ToggleLevelComplete();
    }

    public void FailedDialogueBoxEnable()
    {
         InGamePanels.instance.ToggleLevelFailed();

    }

    private void RespondToThrustInput()
    {
        
    bool thrust = CrossPlatformInputManager.GetButton("Jump");
       
        if ( thrust || Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
            StopApplyingThrust();
        
    }

    private void StopApplyingThrust()
    {
        audioSource.Stop();
        //SoundManager.PlaySound(SoundManager.Sound.LoseSound);
        engineParticlesCloud.Stop();
        engineParticlesFlame.Stop();
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust*Time.deltaTime);
        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.outputAudioMixerGroup = GameAssets.i.masterAudioMIxer;
            audioSource.PlayOneShot(mainEngine);

            //SoundManager.PlaySound(SoundManager.Sound.Rocket_Thrust);
            
        }
        engineParticlesCloud.Play();
        engineParticlesFlame.Play();
        
    }

private void RespondToRotateInput ()
    { 
        float xRotate = CrossPlatformInputManager.GetAxis("Horizontal") ;
        if (xRotate < -0.2f || Input.GetKey(KeyCode.A))
        {
            //xRotate = -1f;
            RotateManually(rcsThrust * Time.deltaTime * 1);
            //Debug.Log("Rotate left");
        }
        else if (xRotate > 0.2f || Input.GetKey(KeyCode.D))
        {
            //xRotate = 1f;
            RotateManually(-rcsThrust * Time.deltaTime * 1);
           // Debug.Log("Rotate Right");
        }
    }

    private void RotateManually(float rotationThisFrame)
    {
        rigidBody.freezeRotation = true; // take manual control of rotation
        transform.Rotate(Vector3.forward * rotationThisFrame);
        rigidBody.freezeRotation = false; // resume physics control of rotation
    }

    IEnumerator LoopAudio ()
    {
//audioSource = GetComponent<AudioSource>();
        float length = 4.5f;
        
        while(true)
        {
            SoundManager.PlaySound(SoundManager.Sound.Firework_Sound);
            yield return new WaitForSeconds(length);
        }
    }
}