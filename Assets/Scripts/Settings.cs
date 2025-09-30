using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Settings : MonoBehaviour
{
    private UIDocument _document;
    private Button _button;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _button = _document.rootVisualElement.Q<Button>("MainMenu") as Button;
        _button.RegisterCallback<ClickEvent>(OnMainClick);
        
    }
    
    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnMainClick);
    }
    
    private void OnMainClick(ClickEvent evt)
    {
        SceneManager.LoadScene("MainMenuTest"); 
        Debug.Log("You clicked the Main Menu button");
       
    }
    
    
    
}
