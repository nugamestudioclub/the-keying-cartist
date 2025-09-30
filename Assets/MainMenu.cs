using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{

    private UIDocument _document;
    private Button _button;


    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _button = _document.rootVisualElement.Q<Button>("Start") as Button;
        _button.RegisterCallback<ClickEvent>(OnPlayGameClick);
        _button = _document.rootVisualElement.Q<Button>("Settings") as Button;
        _button.RegisterCallback<ClickEvent>(OnSettingsClick);
        _button = _document.rootVisualElement.Q<Button>("Credits") as Button;
        _button.RegisterCallback<ClickEvent>(OnCreditsClick);
        _button = _document.rootVisualElement.Q<Button>("Exit") as Button;
        _button.RegisterCallback<ClickEvent>(OnExitClick);

    }

    private void OnDisable()
    {
      _button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
    }

    private void OnPlayGameClick(ClickEvent eve)
    {
         Debug.Log("You clicked the start button");
    }

    private void OnSettingsClick(ClickEvent eve)
    {
        Debug.Log("You clicked the settings button");
    }

    private void OnCreditsClick(ClickEvent eve)
    {
        Debug.Log("You clicked the credits button");
    }

    private void OnExitClick(ClickEvent eve)
    {
        Application.Quit();
    }
}
