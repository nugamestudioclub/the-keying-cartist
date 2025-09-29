using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Texture2D[] m_levelGoalTextures;

    [HideInInspector] public Texture2D CurrentLevelGoalTexture;

    [SerializeField] private int DEBUG_LevelSelect = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

#if UNITY_EDITOR
        //DEBUG
        if (DEBUG_LevelSelect >= 0) ChooseLevel(DEBUG_LevelSelect);
#endif
    }

    public void ChooseLevel(int level_index)
    {
        CurrentLevelGoalTexture = m_levelGoalTextures[level_index];
    }

    public void DEBUG_End()
    {
        Debug.Log("UR DONE");
    }
}
