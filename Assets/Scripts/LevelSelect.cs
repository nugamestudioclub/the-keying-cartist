using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement; 

public class LevelSelect : MonoBehaviour
{
    private Label _label;
    private string _originalText;

    private Button _playButton;
    private Button _settingsButton;
    private Button _quitButton;
    private Button _mainMenuButton; 

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // The shared label
        _label = root.Q<Label>("ChangingLabel");
        if (_label == null) return;

        _originalText = _label.text;


        _label.style.whiteSpace = WhiteSpace.Normal;
        _label.style.overflow = Overflow.Visible;
        _label.style.height = StyleKeyword.Auto;

     
        _playButton = root.Q<Button>("Level1");
        _settingsButton = root.Q<Button>("Level2");
        _quitButton = root.Q<Button>("Level3");
        _mainMenuButton = root.Q<Button>("MainMenu"); 

        // Level 1
        if (_playButton != null)
        {
            _playButton.RegisterCallback<PointerEnterEvent>(evt => SetLabel("Draw the numbers six seven and candy"));
            _playButton.RegisterCallback<PointerLeaveEvent>(evt => ResetLabel());
            _playButton.clicked += () => LoadScene("");
        }

        // Level 2
        if (_settingsButton != null)
        {
            _settingsButton.RegisterCallback<PointerEnterEvent>(evt => SetLabel("Draw a bird and someone flipping the bird"));
            _settingsButton.RegisterCallback<PointerLeaveEvent>(evt => ResetLabel());
            _settingsButton.clicked += () => LoadScene("");
        }

        // Level 3
        if (_quitButton != null)
        {
            _quitButton.RegisterCallback<PointerEnterEvent>(evt => SetLabel("Draw hit game Hollow Knight and Silksong Hornet with sneakers and flowers around"));
            _quitButton.RegisterCallback<PointerLeaveEvent>(evt => ResetLabel());
            _quitButton.clicked += () => LoadScene("");
        }

        // Main Menu
        if (_mainMenuButton != null)
        {
            _mainMenuButton.RegisterCallback<PointerEnterEvent>(evt => SetLabel("Go back to the Main Menu"));
            _mainMenuButton.RegisterCallback<PointerLeaveEvent>(evt => ResetLabel());
            _mainMenuButton.clicked += () => LoadScene("MainMenuTest");
        }
    }

    private void SetLabel(string text)
    {
        _label.text = text;
    }

    private void ResetLabel()
    {
        _label.text = _originalText;
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
