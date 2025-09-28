using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Scoring
{
    public void ScoreResult(RenderTexture drawing, Texture2D source)
    {
        var drawing_2d = new Texture2D(drawing.width, drawing.height, TextureFormat.ARGB32, false);

        RenderTexture.active = drawing;
        drawing_2d.ReadPixels(new Rect(0, 0, drawing.width, drawing.height), 0, 0);
        drawing_2d.Apply();
        RenderTexture.active = null;

        uint hits = 0;
        uint total = 0;

        // Assume source texture is centered and scaled to render texture

        float xScale = (float) drawing.width / source.width;
        float yScale = (float) drawing.height / source.height;
        Debug.Log($"xScale: {xScale}, yScale: {yScale}");

        for (int i = 0; i < source.height; i++)
        {
            for (int j = 0; j < source.width; j++)
            {
                bool source_has_pixel = source.GetPixel(j, i).a > 0f;

                if (source_has_pixel && drawing_2d.GetPixel(Mathf.FloorToInt(j * xScale), Mathf.FloorToInt(i * yScale)).a > 0f)
                {
                    hits++;
                }
                
                if (source_has_pixel)
                {
                    total++;
                }
            }
        }

        Debug.Log($"HITS {hits} and TOTAL {total}");
    }
}
