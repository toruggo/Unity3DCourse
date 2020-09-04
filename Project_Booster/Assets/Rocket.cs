using UnityEngine;
using UnityEngine.SceneManagement;

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
	enum State { Alive, Dying, Transcending }
	State state = State.Alive;

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	void Update ()
	{
		if (state == State.Alive)
		{
			RespondToThrustInput();
			RespondToRotateInput();
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (state != State.Alive){ return; } // ignore collisions when dead

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
		state = State.Transcending;
		audioSource.Stop();
		audioSource.PlayOneShot(success);
		successParticles.Play();
		Invoke("LoadNextLevel", 1f);
	}

	void StartDeathSequence()
	{
		state = State.Dying;
		audioSource.Stop();
		audioSource.PlayOneShot(death);
		deathParticles.Play();
		Invoke("LoadFirstLevel", 1f);
	}

	private void LoadNextLevel()
	{
		SceneManager.LoadScene(1); // allow for more than 2 scenes
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
			audioSource.Stop();
			mainEngineParticles.Stop();
		}
	}

	private void ApplyThrust()
	{
		rigidBody.AddRelativeForce(Vector3.up * mainThrust);
		if (!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(mainEngine);
		}
		mainEngineParticles.Play();
	}

	private void RespondToRotateInput()
	{
		rigidBody.freezeRotation = true; // take manual control of rotation

		float rotationThisFrame = rcsThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * rotationThisFrame);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(-Vector3.forward * rotationThisFrame);
		}

		rigidBody.freezeRotation = false; //resume physics control of rotation
	}
}
