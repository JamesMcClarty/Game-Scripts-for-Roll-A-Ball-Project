using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public float speed;

	public AudioClip coinSound;
	public AudioClip spikeSound;
	public AudioClip targetSound;
	public AudioClip rollingSound;
	public AudioClip windSound;
    public LayerMask whatIsSurface;
    public bool onSlope;
    public GameObject particleStars;
    public GameObject particleTargetHit;

    private GameObject sparkleHit;
    private MainCamera mainCamera;
	private Rigidbody rigidBody;
    private LivesCount livesCount;
    private float respawnTimer;
    float moveHorizontal;
    float moveVertical;

    RandomItemGenerator randomItemGenerator;

    public Transform spawnpoint;

	// Use this for initialization
	void Start () {

		rigidBody = GetComponent<Rigidbody> ();
        livesCount = GameObject.FindGameObjectWithTag("LifeCounter").GetComponent<LivesCount>();
        randomItemGenerator = GameObject.FindGameObjectWithTag("RandomItemGenerator").GetComponent<RandomItemGenerator>();
        if (!PlayerPrefs.HasKey("PlayerScore"))
        {
            PlayerPrefs.SetInt("PlayerScore", 0);
        }

        if (!PlayerPrefs.HasKey("PlayerLevel"))
        {
            PlayerPrefs.SetInt("PlayerLevel", 2);
            Debug.Log(PlayerPrefs.GetInt("PlayerLevel"));
        }
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
    }

    void Update()
    {
        //This checks if the ball is on the slope.
        Collider[] colliders;
        colliders = Physics.OverlapSphere(gameObject.transform.position, 1f, whatIsSurface, QueryTriggerInteraction.Ignore);
        //If the collider is on a surface, it plays the roll sound in the RollingSoundScript. Otherwise, it turns off.
        if (colliders.Length != 0)
        {
            onSlope = true;
        }
        else{
            onSlope = false;
        }

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if(respawnTimer > 0f)
        {
            respawnTimer = respawnTimer - Time.deltaTime;
            if (respawnTimer <= 0f)
            {
                StartEverything();
                ResetPosition();
            }
        }
        if (!mainCamera.isCameraShowingStage())
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");


            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rigidBody.AddForce(movement * speed);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Target")
        {
            PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") + 100);
            //rigidBody.isKinematic = true;
            respawnTimer = 2;
			AudioSource audio = GetComponent<AudioSource> ();
            if(AudioLevelSingleton.Instance != null)
            {
                audio.volume = AudioLevelSingleton.Instance.GetSoundVolume();
            }
			audio.PlayOneShot (targetSound);
            mainCamera.StopFollowing();
        }
			
		if (col.gameObject.tag == "Spike")
		{
			AudioSource audio = GetComponent<AudioSource> ();
            if (AudioLevelSingleton.Instance != null)
            {
                audio.volume = AudioLevelSingleton.Instance.GetSoundVolume();
            }
            audio.PlayOneShot (spikeSound);
		}

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") + 10);
            AudioSource audio = GetComponent<AudioSource> ();
            if (AudioLevelSingleton.Instance != null)
            {
                audio.volume = AudioLevelSingleton.Instance.GetSoundVolume();
            }
            audio.PlayOneShot (coinSound);
            sparkleHit = Instantiate(particleStars, gameObject.transform.position, Quaternion.identity);
        }

		if (other.gameObject.tag == "target_100")
		{
            PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") + 100);
			//rigidBody.isKinematic = true;
			respawnTimer = 2;
			AudioSource audio = GetComponent<AudioSource> ();
			if (AudioLevelSingleton.Instance != null)
			{
				audio.volume = AudioLevelSingleton.Instance.GetSoundVolume();
			}
			audio.PlayOneShot (targetSound);
            sparkleHit = Instantiate(particleTargetHit, gameObject.transform.position, Quaternion.identity);
            mainCamera.StopFollowing();
            StopEverything();
        }

		if (other.gameObject.tag == "target_250")
		{
            PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") + 250);
            //rigidBody.isKinematic = true;
            respawnTimer = 2;
			AudioSource audio = GetComponent<AudioSource> ();
			if (AudioLevelSingleton.Instance != null)
			{
				audio.volume = AudioLevelSingleton.Instance.GetSoundVolume();
			}
			audio.PlayOneShot (targetSound);
            sparkleHit = Instantiate(particleTargetHit, gameObject.transform.position, Quaternion.identity);
            mainCamera.StopFollowing();
            StopEverything();
        }

		if (other.gameObject.tag == "target_500")
		{
            PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") + 500);
			//rigidBody.isKinematic = true;
			respawnTimer = 2;
			AudioSource audio = GetComponent<AudioSource> ();
			if (AudioLevelSingleton.Instance != null)
			{
				audio.volume = AudioLevelSingleton.Instance.GetSoundVolume();
			}
			audio.PlayOneShot (targetSound);
            sparkleHit = Instantiate(particleTargetHit, gameObject.transform.position, Quaternion.identity);
            mainCamera.StopFollowing();
            StopEverything();
        }

		if (other.gameObject.tag == "target_1000")
		{
            PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") + 1000);
            //rigidBody.isKinematic = true;
            respawnTimer = 2;
			AudioSource audio = GetComponent<AudioSource> ();
			if (AudioLevelSingleton.Instance != null)
			{
				audio.volume = AudioLevelSingleton.Instance.GetSoundVolume();
			}
			audio.PlayOneShot (targetSound);
            sparkleHit = Instantiate(particleTargetHit, gameObject.transform.position, Quaternion.identity);
            mainCamera.StopFollowing();
            StopEverything();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Border"))
        {
            livesCount.LoseLife();
            ResetPosition();
        }
    }

    void ResetPosition() {
        mainCamera.KeepFollowing();
        gameObject.transform.position = spawnpoint.position;
        gameObject.transform.rotation = Quaternion.identity;
        rigidBody.isKinematic = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        if (livesCount.GetLivesCount() >= 1)
        {
            randomItemGenerator.DeleteItems();
            randomItemGenerator.InstantiateItems();
            if(PlayerPrefs.GetInt("PlayerScore") >= 2000 && PlayerPrefs.GetInt("PlayerLevel") == 2)
            {
                SceneManager.LoadScene(4);
                PlayerPrefs.SetInt("PlayerLevel", 4);
            }

            if (PlayerPrefs.GetInt("PlayerScore") >= 4000 && PlayerPrefs.GetInt("PlayerLevel") == 4)
            {
                SceneManager.LoadScene(3);
                PlayerPrefs.SetInt("PlayerLevel",3);
            }
        }
    }

    public int GetScore { get { return PlayerPrefs.GetInt("PlayerScore"); } }

    public int GetLivesCount { get { return livesCount.GetLivesCount(); } }
    public float GetHorAxis { get { return moveHorizontal; } }
    public float GetVerAxis { get { return moveVertical; } }

    public void StopEverything()
    {
        rigidBody.isKinematic = true;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        if (PlayerPrefs.GetInt("PlayerLevel") == 2)
        {
            GameObject.FindGameObjectWithTag("target_100").GetComponentInParent<Mover>().SwitchMoving();
            GameObject.FindGameObjectWithTag("target_250").GetComponentInParent<Mover>().SwitchMoving();
            GameObject.FindGameObjectWithTag("target_500").GetComponentInParent<Mover>().SwitchMoving();
            GameObject.FindGameObjectWithTag("target_1000").GetComponentInParent<Mover>().SwitchMoving();
        }

        if (PlayerPrefs.GetInt("PlayerLevel") == 3)
        {
            GameObject.FindGameObjectWithTag("target_100").GetComponentInParent<CircleMover>().SwitchMoving();
            GameObject.FindGameObjectWithTag("target_250").GetComponentInParent<CircleMover>().SwitchMoving();
            GameObject.FindGameObjectWithTag("target_500").GetComponentInParent<CircleMover>().SwitchMoving();
            GameObject.FindGameObjectWithTag("target_1000").GetComponentInParent<CircleMover>().SwitchMoving();
        }
    }

    public void StartEverything()
    {
        rigidBody.isKinematic = false;
        if (PlayerPrefs.GetInt("PlayerLevel") == 2)
        {
            GameObject.FindGameObjectWithTag("target_100").GetComponentInParent<Mover>().SwitchMoving();
            GameObject.FindGameObjectWithTag("target_250").GetComponentInParent<Mover>().SwitchMoving();
            GameObject.FindGameObjectWithTag("target_500").GetComponentInParent<Mover>().SwitchMoving();
            GameObject.FindGameObjectWithTag("target_1000").GetComponentInParent<Mover>().SwitchMoving();

        }

        if (PlayerPrefs.GetInt("PlayerLevel") == 3)
        {
            GameObject.FindGameObjectWithTag("target_100").GetComponentInParent<CircleMover>().SwitchMoving();
            GameObject.FindGameObjectWithTag("target_250").GetComponentInParent<CircleMover>().SwitchMoving();
            GameObject.FindGameObjectWithTag("target_500").GetComponentInParent<CircleMover>().SwitchMoving();
            GameObject.FindGameObjectWithTag("target_1000").GetComponentInParent<CircleMover>().SwitchMoving();
        }
    }
}
