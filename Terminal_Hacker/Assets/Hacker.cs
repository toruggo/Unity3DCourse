using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

	void Start () {
		ShowMainMenu("Hugo");
	}

	void ShowMainMenu(string greetingName) {
		Terminal.ClearScreen();
		Terminal.WriteLine("Hello " + greetingName + "!");
		Terminal.WriteLine("What would you like to hack into?\n");
		Terminal.WriteLine("Press 1 for the local library");
		Terminal.WriteLine("Press 2 for the police station");
		Terminal.WriteLine("Press 3 for the NASA\n");
		Terminal.WriteLine("Enter your selection: ");
	}

	void OnUserInput(string input) {
		print(input);
	}
}
