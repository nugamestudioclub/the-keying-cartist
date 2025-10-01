using UnityEngine;
using UnityEngine.UIElements;

public class InGameButton : MonoBehaviour
{
    private UIDocument _document;
    private Button _endEarlyButton;

    void OnEnable()
    {
        // Get the UIDocument component
        _document = GetComponent<UIDocument>();
        if (_document == null)
        {
            Debug.LogError("No UIDocument found on this GameObject!");
            return;
        }

      
        _endEarlyButton = _document.rootVisualElement.Q<Button>("EndEarly");
        if (_endEarlyButton == null)
        {
            Debug.LogError("EndEarlyButton not found in UIDocument!");
            return;
        }

        // Register the click callback
        _endEarlyButton.RegisterCallback<ClickEvent>(OnEndEarlyClick);
    }

    private void OnEndEarlyClick(ClickEvent evt)
    {
        Debug.Log("End Early button clicked!");

      
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StoreScoreAndEnd(false); 
        }
        else
        {
            Debug.LogError("GameManager.Instance is null!");
        }
    }
}