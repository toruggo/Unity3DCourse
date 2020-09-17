using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 100f;
	[SerializeField] float levelLoadDelay = 2f;

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

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	void Update ()
	{
		if (!isTransitioning)
		{
			RespondToThrustInput();
			RespondToRotateInput();
		}
		if(Debug.isDebugBuild)
		{
			RespondToDebugKeys();
		}
	}

	private void RespondToDebugKeys()
	{
		if(Input.GetKeyDown(KeyCode.L))
		{
			LoadNextLevel();
		}
		else if(Input.GetKeyDown(KeyCode.C))
		{
			collisionsDisabled = !collisionsDisabled;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (isTransitioning || collisionsDisabled){ return; } // ignore collisions when dead

		switch (collision.gameObject.tag)
		{
			case "Friendly":
				// do nothing
				break;
			case "Finish":
				StartSucessSequence();
				break;
			default:
				StartDeathSequence();
				break;
		}
	}

	void StartSucessSequence()
	{
		isTransitioning = true;
		audioSource.Stop();
		audioSource.PlayOneShot(success);
		successParticles.Play();
		Invoke("LoadNextLevel", levelLoadDelay);
	}

	void StartDeathSequence()
	{
		isTransitioning = true;
		audioSource.Stop();
		audioSource.PlayOneShot(death);
		deathParticles.Play();
		Invoke("LoadFirstLevel", levelLoadDelay);
	}

	private void LoadNextLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
		SceneManager.LoadScene(nextSceneIndex);
	}

	private void LoadFirstLevel()
	{
		SceneManager.LoadScene(0);
	}

	private void RespondToThrustInput()
	{
		if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
		{
			ApplyThrust();
		}
		else
		{
			StopApplyingThrust();
		}
	}

	private void StopApplyingThrust()
	{
		audioSource.Stop();
		mainEngineParticles.Stop();
	}

	private void ApplyThrust()
	{
		rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
		if (!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(mainEngine);
		}
		mainEngineParticles.Play();
	}

	private void RespondToRotateInput()
	{
		rigidBody.angularVelocity = Vector3.zero;

		float rotationThisFrame = rcsThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * rotationThisFrame);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(-Vector3.forward * rotationThisFrame);
		}
	}
}
