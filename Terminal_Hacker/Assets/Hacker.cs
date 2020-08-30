﻿using UnityEngine;

public class Hacker : MonoBehaviour {

	// Game configuration data
	string[] level1Passwords = { "books", "aisle", 	"shelf", "password", "font", "borrow" };
	string[] level2Passwords = { "handcuffs", "guns", "officer", "prison", "jailbreak", "arrest" };
	string[] level3Passwords = { "mars", "moon", "stars", "calipso", "curiosity", "satellite" };

	// Game state
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
		bool isValidLevelNumber = (input == "1" || input == "2");
		if(isValidLevelNumber)
		{
			level = int.Parse(input);
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
		Terminal.ClearScreen();
		switch(level)
		{
			case 1:
				password = level1Passwords[Random.Range(0, level1Passwords.Length)];
				break;
			case 2:
				password = level2Passwords[Random.Range(0, level2Passwords.Length)];
				break;
			case 3:
				password = level3Passwords[Random.Range(0, level3Passwords.Length)];
				break;
			default:
				Debug.LogError("Invalid level number");
				break;
		}
		Terminal.WriteLine("Please enter your password: ");
	}

	void CheckPassword(string input)
	{
		if(input == password)
		{
			DisplayWinScreen();
		}
		else
		{
			Terminal.WriteLine("Incorrect password! Please try again.");
		}
	}

	void DisplayWinScreen()
	{
		currentScreen = Screen.Win;
		Terminal.ClearScreen();
		ShowLevelReward();
	}

	void ShowLevelReward()
	{
		switch(level)
		{
			case 1:
				Terminal.WriteLine("Have a book...");
				Terminal.WriteLine(@"
	_______
   /      //
  /      //
 /_____ //
(______(/           
"				);
				break;
			case 2:
				Terminal.WriteLine("You got the prison key!");
				Terminal.WriteLine(@"
 __
/0 \_______
\__/-=' = '
"				);
				break;
			default:
				Debug.LogError("Invalid level reached");
				break;
		}
	}
}
