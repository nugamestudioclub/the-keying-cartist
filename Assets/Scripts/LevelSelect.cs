using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelect : MonoBehaviour
{
    private Label _label;
    private string _originalText;

    private Button _levelOne;
    private Button _levelTwo;
    private Button _levelThree;
    private Button _mainMenuButton;

    
    public GameManager GameManager;

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

        _levelOne = root.Q<Button>("Level1");
        _levelTwo = root.Q<Button>("Level2");
        _levelThree = root.Q<Button>("Level3");
        _mainMenuButton = root.Q<Button>("MainMenu");

        // Level 1
        if (_levelOne != null)
        {
            _levelOne.RegisterCallback<PointerEnterEvent>(evt => SetLabel("Draw the numbers six seven and candy"));
            _levelOne.RegisterCallback<PointerLeaveEvent>(evt => ResetLabel());
            _levelOne.clicked += () => GameManager.ChooseLevel(0); 
        }

        // Level 2
        if (_levelTwo != null)
        {
            _levelTwo.RegisterCallback<PointerEnterEvent>(evt => SetLabel("Draw a bird and someone flipping the bird"));
            _levelTwo.RegisterCallback<PointerLeaveEvent>(evt => ResetLabel());
            _levelTwo.clicked += () => GameManager.ChooseLevel(1); 
        }

        // Level 3
        if (_levelThree != null)
        {
            _levelThree.RegisterCallback<PointerEnterEvent>(evt => SetLabel("Draw hit game Hollow Knight and Silksong Hornet with sneakers and flowers around"));
            _levelThree.RegisterCallback<PointerLeaveEvent>(evt => ResetLabel());
            _levelThree.clicked += () => GameManager.ChooseLevel(2); 
        }

        // Main Menu
        if (_mainMenuButton != null)
        {
            _mainMenuButton.RegisterCallback<PointerEnterEvent>(evt => SetLabel("Go back to the Main Menu"));
            _mainMenuButton.RegisterCallback<PointerLeaveEvent>(evt => ResetLabel());
            _mainMenuButton.clicked += () => UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuTest");
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
}
