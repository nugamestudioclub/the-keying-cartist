using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Texture2D[] m_levelGoalTextures;

    [HideInInspector] public Texture2D CurrentLevelGoalTexture;
    [HideInInspector] public static float ScorePercentage;

    [SerializeField] private int DEBUG_LevelSelect = 0;
    public static bool DidRunOutOfTime;   


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
        //DEBUG
        if (DEBUG_LevelSelect >= 0) ChooseLevel(DEBUG_LevelSelect);
#endif
    }

    public void ChooseLevel(int level_index)
    {
        CurrentLevelGoalTexture = m_levelGoalTextures[level_index];
    }

    public void StoreScoreAndEnd(bool did_run_out_of_time)
    {
        ScorePercentage = GameObject.FindObjectOfType<PaintingManager>().GetScore();
        SceneManager.LoadScene("RanOutOfTime");
       
  
    }
}
