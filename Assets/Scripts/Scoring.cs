using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Scoring
{
    public void ScoreResult(RenderTexture drawing, Texture2D source)
    {
        var drawing_2d = new Texture2D(drawing.width, drawing.height, TextureFormat.ARGB32, false);

        Graphics.CopyTexture(drawing, drawing_2d);

        uint hits = 0;
        uint total = 0;
        for (int i = 0; i < source.height; i++)
        {
            for (int j = 0; j < source.width; j++)
            {
                bool source_has_pixel = source.GetPixel(j, i).a > 0f;

                if (source_has_pixel && drawing_2d.GetPixel(j, i).a > 0f)
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
