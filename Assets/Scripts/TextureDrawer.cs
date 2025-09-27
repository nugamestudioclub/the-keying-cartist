using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureDrawer
{
    private readonly RenderTexture m_targetTexture;
    private readonly Brush m_activeBrush;

    private Vector2 m_previousPosition;

    public TextureDrawer(RenderTexture target_texture, Brush brush)
    {
        m_targetTexture = target_texture;
        m_activeBrush = brush;
    }

    public void Draw(Vector2 position)
    {
        RenderTexture.active = m_targetTexture;

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, m_targetTexture.height, m_targetTexture.width, 0);

        var source_rect = new Rect(0, 1, 1, 1); //REMOVED: flip x and y
        var rect = new Rect(
            position.x - m_activeBrush.Size.x / 2,
            position.y - m_activeBrush.Size.y / 2,
            m_activeBrush.Size.x * ((float)m_targetTexture.height / m_targetTexture.width),
            m_activeBrush.Size.y * ((float)m_targetTexture.width / m_targetTexture.height)
        );

        Graphics.DrawTexture(rect, m_activeBrush.BrushTexture, source_rect, 0, 0, 0, 0, m_activeBrush.BrushMaterial);

        GL.PopMatrix();

        RenderTexture.active = null;

        m_previousPosition = position;
    }

    // draws from the last position to this one by stamping repeatedly
    public void DrawTo(Vector2 position, float smoothness = 0.1f)
    {
        // cache the previous position because draw overwrites it
        var previous = m_previousPosition;

        float avg_dim = (m_activeBrush.Size.x + m_activeBrush.Size.y) / 2;

        float distance = Vector2.Distance(previous, position);
        int step_count = Mathf.CeilToInt(distance / (avg_dim * smoothness));

        for (int i = 0; i < step_count; i++)
        {
            Draw(Vector2.Lerp(previous, position, (float)i / step_count));
        }

        // draw updates the previous position from the mouse, so we dont need to update the cache.
    }
}
