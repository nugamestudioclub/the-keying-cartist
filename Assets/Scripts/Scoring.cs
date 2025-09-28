using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Scoring
{
    private readonly Texture2D m_drawingTexture;
    private readonly Texture2D m_sourceTexture;
    private readonly AnimationCurve m_wastedCurve;
    private readonly AnimationCurve m_closeEnoughCurve;

    public Scoring(RenderTexture drawing_texture, Texture2D source_texture, AnimationCurve wasted_curve, AnimationCurve close_enough_curve)
    {
        m_drawingTexture = new Texture2D(drawing_texture.width, drawing_texture.height, TextureFormat.ARGB32, false);

        RenderTexture.active = drawing_texture;
        m_drawingTexture.ReadPixels(new Rect(0, 0, drawing_texture.width, drawing_texture.height), 0, 0);
        m_drawingTexture.Apply();
        RenderTexture.active = null;

        m_sourceTexture = source_texture;
        m_wastedCurve = wasted_curve;
        m_closeEnoughCurve = close_enough_curve;
    }


    public float ScoreResult()
    {
        uint hits = 0;
        uint total_in_source = 0;
        uint total_in_drawing = 0;
        uint wasted = 0;

        // Assume source texture is centered and scaled to render texture

        // texture imports snap to powers of 2 by default, so correct the scale diff between the source and drawing
        float xScale = (float)m_drawingTexture.width / m_sourceTexture.width;
        float yScale = (float)m_drawingTexture.height / m_sourceTexture.height;

        for (int i = 0; i < m_sourceTexture.height; i++)
        {
            for (int j = 0; j < m_sourceTexture.width; j++)
            {
                bool source_has_pixel = m_sourceTexture.GetPixel(j, i).a > 0.1f;
                bool drawing_has_pixel = m_drawingTexture.GetPixel(Mathf.FloorToInt(j * xScale), Mathf.FloorToInt(i * yScale)).a > 0.1f;

                if (source_has_pixel && drawing_has_pixel)
                {
                    hits++;
                    total_in_drawing++;
                    total_in_source++;
                }
                else if (drawing_has_pixel) // if wasted pixel
                {
                    wasted++;
                    total_in_drawing++;
                }
                else if (source_has_pixel)
                {
                    total_in_source++;
                }
            }
        }

        if (total_in_drawing == 0 || total_in_source == 0) return 0;

        float penalty = m_wastedCurve.Evaluate((float)wasted / total_in_drawing);
        float percentage = m_closeEnoughCurve.Evaluate((float)hits / total_in_source);

        return percentage * (1 - penalty);
    }
}
