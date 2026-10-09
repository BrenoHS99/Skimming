using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOptions : MonoBehaviour
{
    private enum MenuOptions
    {
        start,
        config,
        credits,
        exit
    }

    private MenuOptions menuButton;

    public void StartButton(int menuButtonSelect)
    {
        switch (menuButtonSelect)
        {
            case 0:
                menuButton = MenuOptions.start;
                break;
			case 1:
				menuButton = MenuOptions.config;
				break;
			case 2:
				menuButton = MenuOptions.credits;
                break;
            case 3:
				menuButton = MenuOptions.exit;
				break;
            default:
                menuButton = MenuOptions.exit;
                break;
		}

        if (menuButton == MenuOptions.start)
        {
            SceneManager.LoadScene(0);
        }
		if (menuButton == MenuOptions.exit)
		{
            Application.Quit();
		}
	}
}
