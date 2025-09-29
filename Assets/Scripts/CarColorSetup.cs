using UnityEngine;

public class CarColorSetup : MonoBehaviour
{
    [SerializeField] private Renderer m_carRenderer;
    [SerializeField] private Texture2D[] m_carTextures;

    void Awake()
    {
        m_carRenderer.material.mainTexture = m_carTextures[Random.Range(0, m_carTextures.Length)];
    }
}
