using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ShowResults : MonoBehaviour
{
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Labels in the results scene
        var scoreLabel = root.Q<Label>("Score");
        var messageLabel = root.Q<Label>("Heading");

        // Set the score label
        if (scoreLabel != null)
            scoreLabel.text = "Score: " + GameManager.ScorePercentage.ToString("F1") + "%";

        // Set other labels based on whether time ran out
        if (GameManager.DidRunOutOfTime)
        {
            if (messageLabel != null)
                messageLabel.text = "You ran out of time";
        }
        else
        {
            if (messageLabel != null)
                messageLabel.text = "You keyed that car";
        }

        // Main Menu button functionality
        var mainMenuButton = root.Q<Button>("MainMenu");
        if (mainMenuButton != null)
        {
            mainMenuButton.clicked += () =>
            {
                SceneManager.LoadScene("MainMenuTest"); 
            };
        }
    }
}