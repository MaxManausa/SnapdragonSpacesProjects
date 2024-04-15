using UnityEngine;

public class GameModeManager : MonoBehaviour
{

    [SerializeField] private PinchHandMenu pinchHandMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject storyMenu;
    [SerializeField] private GameObject freePlayMenu;

    // Enumeration for Game Modes
    public enum GameMode
    {
        MainMenu,
        StoryMode,
        FreePlay
    }

    // Private field to hold the current game mode
    private GameMode currentMode;

    // Public property to safely expose the current game mode
    public GameMode CurrentMode
    {
        get { return currentMode; }
        private set { currentMode = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Default to Main Menu at start
        SetGameMode(GameMode.MainMenu);
    }

    // Update the game mode
    public void SetGameMode(GameMode mode)
    {
        CurrentMode = mode;  // Using the property here for setting the value
        switch (mode)
        {
            case GameMode.MainMenu:
                EnterMainMenu();
                break;
            case GameMode.StoryMode:
                EnterStoryMode();
                break;
            case GameMode.FreePlay:
                EnterFreePlay();
                break;
            default:
                Debug.LogError("Undefined game mode");
                break;
        }
    }

    // Method to activate Main Menu
    public void EnterMainMenu()
    {
        Debug.Log("Entering Main Menu");
        // Add your Main Menu logic here
        pinchHandMenu.handMenu = mainMenu;
    }

    // Method to activate Story Mode
    public void EnterStoryMode()
    {
        Debug.Log("Entering Story Mode");
        // Add your Story Mode logic here
        pinchHandMenu.handMenu = storyMenu;
    }

    // Method to activate Free Play
    public void EnterFreePlay()
    {
        Debug.Log("Entering Free Play");
        // Add your Free Play logic here
        pinchHandMenu.handMenu = freePlayMenu;
    }
}