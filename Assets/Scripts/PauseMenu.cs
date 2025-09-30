using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _pauseMenu;
    private Button _continueButton;
    private Button _mainMenuButton;
    private Slider _volumeSlider;

    private bool _isPaused;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        VisualElement root = _document.rootVisualElement;

        // Pause menu panel
        _pauseMenu = root.Q<VisualElement>("PauseMenu");
        _pauseMenu.style.display = DisplayStyle.None; // start hidden
        _volumeSlider = _document.rootVisualElement.Q<Slider>("VolumeSlider");
        // Buttons
        _continueButton = _pauseMenu.Q<Button>("Continue");
        _mainMenuButton = _pauseMenu.Q<Button>("MainMenu");

        if (_continueButton != null)
            _continueButton.RegisterCallback<ClickEvent>(evt => ResumeGame());
        if (_mainMenuButton != null)
            _mainMenuButton.RegisterCallback<ClickEvent>(evt => BackToMainMenu());

        // Slider (must be inside PauseMenu)
       
        if (_volumeSlider != null)
        {
            _volumeSlider.focusable = true; // ensure it can receive pointer input
            _volumeSlider.value = AudioListener.volume;

            // Ensure slider receives focus on pointer down
            _volumeSlider.RegisterCallback<PointerDownEvent>(evt => _volumeSlider.Focus());

            _volumeSlider.RegisterValueChangedCallback(evt =>
            {
                AudioListener.volume = evt.newValue;
                Debug.Log("Volume set to: " + evt.newValue);
            });
        }
        else
        {
            Debug.LogWarning("VolumeSlider not found in PauseMenu.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        _isPaused = true;
        _pauseMenu.style.display = DisplayStyle.Flex;
    }

    private void ResumeGame()
    {
        _isPaused = false;
        _pauseMenu.style.display = DisplayStyle.None;
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuTest");
    }
}
