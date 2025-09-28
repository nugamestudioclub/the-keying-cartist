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
        float scaled_x = m_activeBrush.Size.x * ((float)m_targetTexture.height / m_targetTexture.width);
        float scaled_y = m_activeBrush.Size.y * ((float)m_targetTexture.width / m_targetTexture.height);
        var rect = new Rect(
            position.x - scaled_x / 2,
            position.y - scaled_y / 2,
            scaled_x,
            scaled_y
        );

        Graphics.DrawTexture(rect, m_activeBrush.BrushTexture, source_rect, 0, 0, 0, 0, m_activeBrush.BrushMaterial);

        GL.PopMatrix();

        RenderTexture.active = null;

        m_previousPosition = position;
    }

    // draws from the last position to this one by stamping repeatedly
    public void DrawTo(Vector2 position, float smoothness = 0.1f, bool do_jitter = true)
    {
        // cache the previous position because draw overwrites it
        var previous = m_previousPosition;

        float avg_dim = (m_activeBrush.Size.x + m_activeBrush.Size.y) / 2;

        float distance = Vector2.Distance(previous, position);
        int step_count = Mathf.CeilToInt(distance / (avg_dim * smoothness));

        for (int i = 0; i < step_count; i++)
        {
            var target_draw_pos = Vector2.Lerp(previous, position, (float)i / step_count);

            if (do_jitter)
            {
                target_draw_pos += Random.insideUnitCircle;
            }

            Draw(target_draw_pos);
        }

        // draw updates the previous position from the mouse, so we dont need to update the cache.
    }

    public RenderTexture RenderTexture => m_targetTexture;
}
