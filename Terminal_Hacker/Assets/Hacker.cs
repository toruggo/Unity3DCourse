using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

	void Start ()
	{
		ShowMainMenu();
	}

	void ShowMainMenu()
	{
		Terminal.ClearScreen();
		Terminal.WriteLine("What would you like to hack into?\n");
		Terminal.WriteLine("Press 1 for the local library");
		Terminal.WriteLine("Press 2 for the police station");
		Terminal.WriteLine("Press 3 for the NASA\n");
		Terminal.WriteLine("Enter your selection: ");
	}

	void OnUserInput(string input)
	{
		if(input == "menu")
		{
			ShowMainMenu();
		}
		if(input == "1")
		{
			print("You chose the library");
		}
		else if(input == "2")
		{
			print("You chose the police station");
		}
		else if(input == "3")
		{
			print("You chose NASA");
		}
		else if(input == "007")
		{
			print("Please select a level, Mr Bond!");
		}
		else
		{
			print("There's no level " + input + ". Please chose a valid level!");
		}
	}
}
