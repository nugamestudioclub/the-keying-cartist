using UnityEngine;

public class Brush
{
    private readonly Vector2 m_size;
    private readonly Texture2D m_brushTexture;
    private readonly Material m_brushMaterial;

    public Brush(Texture2D brush_texture = null)
    {
        m_brushTexture = brush_texture == null ? MakeSquareTexture(32) : brush_texture;
        m_brushMaterial = new Material(Shader.Find("Sprites/Default")); // support alpha on brushes
        m_brushMaterial.color = Color.white;

        m_size = new Vector2(brush_texture.width, brush_texture.height);
    }

    public Texture2D BrushTexture => m_brushTexture;
    public Material BrushMaterial => m_brushMaterial;
    public Vector2 Size => m_size;

    public static Texture2D MakeSquareTexture(int size)
    {
        var tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        Color[] colors = new Color[size * size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                colors[i * size + j] = Color.white;
            }
        }

        tex.SetPixels(colors);
        tex.Apply();

        return tex;
    }
}