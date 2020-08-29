using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

	int level;
	enum Screen { MainMenu, Password, Win };
	Screen currentScreen;
	string password;

	void Start ()
	{
		ShowMainMenu();
	}

	void ShowMainMenu()
	{
		currentScreen = Screen.MainMenu;
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
		else if(currentScreen == Screen.MainMenu)
		{
			RunMainMenu(input);
		}
		else if(currentScreen == Screen.Password)
		{
			CheckPassword(input);
		}
	}

	void RunMainMenu(string input)
	{
		if(input == "1")
		{
			level = 1;
			password = "bookshelf";
			StartGame();
		}
		else if(input == "2")
		{
			level = 2;
			password = "jailbreak";
			StartGame();
		}
		else if(input == "3")
		{
			level = 3;
			password = "curiosity";
			StartGame();
		}
		else if(input == "007")
		{
			Terminal.WriteLine("Please select a level, Mr Bond!");
		}
		else
		{
			Terminal.WriteLine("There's no level " + input + ". Please chose a valid level!");
		}
	}

	void StartGame()
	{
		currentScreen = Screen.Password;
		Terminal.WriteLine("You have chosen level " + level);
		Terminal.WriteLine("Please enter your password: ");
	}

	void CheckPassword(string input)
	{
		if(input == password)
		{
			Terminal.WriteLine("Correct password!");
		}
		else
		{
			Terminal.WriteLine("Incorrect password! Please try again.");
		}
	}
}
