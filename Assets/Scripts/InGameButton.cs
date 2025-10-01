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

        // Query the End Early button (recursive search by name)
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
        // Optional: debug log
        Debug.Log("End Early button clicked!");

        // Call GameManager's function
        GameManager gm = GameObject.FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.StoreScoreAndEnd(false);
        }
        else
        {
            Debug.LogError("GameManager not found in scene!");
        }
    }
}