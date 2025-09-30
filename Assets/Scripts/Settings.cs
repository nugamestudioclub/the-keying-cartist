using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Settings : MonoBehaviour
{
    private UIDocument _document;
    private Button _button;
    private Slider _volumeSlider;


    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _button = _document.rootVisualElement.Q<Button>("MainMenu") as Button;
        _button.RegisterCallback<ClickEvent>(OnMainClick);
        _button = _document.rootVisualElement.Q<Button>("Continue") as Button;
        _volumeSlider = _document.rootVisualElement.Q<Slider>("VolumeSlider");

        if (_volumeSlider != null)
        {
         
            _volumeSlider.value = AudioListener.volume;

  
            _volumeSlider.RegisterValueChangedCallback(evt =>
            {
                AudioListener.volume = evt.newValue;
                Debug.Log("Volume set to: " + evt.newValue);
            });
        }
        else
        {
            Debug.LogWarning("VolumeSlider not found in UI Document.");
        }
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
