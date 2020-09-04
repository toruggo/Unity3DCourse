﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	// todo finish lighting bug
	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 100f;
	[SerializeField] AudioClip mainEngine;
	[SerializeField] AudioClip success;
	[SerializeField] AudioClip death;

	Rigidbody rigidBody;
	AudioSource audioSource;
	enum State { Alive, Dying, Transcending }
	State state = State.Alive;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		// somewhere stop sound on death
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
		Invoke("LoadNextLevel", 1f);
	}

	void StartDeathSequence()
	{
		state = State.Dying;
		audioSource.Stop();
		audioSource.PlayOneShot(death);
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
		}
	}

	private void ApplyThrust()
	{
		rigidBody.AddRelativeForce(Vector3.up * mainThrust);
		if (!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(mainEngine);
		}
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
