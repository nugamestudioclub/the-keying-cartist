using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : MonoBehaviour
{
    [SerializeField] private Camera m_mainCamera;
    [SerializeField] private CameraController m_cameraController;
    [SerializeField] private Renderer m_targetRenderer;
    [SerializeField] private Renderer m_focusRenderer;
    [SerializeField] private LayerMask m_paintObjLayer;

    [Space(10)]

    [SerializeField] private int m_drawTextureWidth;
    [SerializeField] private int m_drawTextureHeight;

    [Space(10)]

    [SerializeField] private Texture2D m_brushTexture;
    [SerializeField] private AnimationCurve m_closeEnoughCurve;
    [SerializeField] private AnimationCurve m_wastedPixelPenaltyCurve;

    private TextureDrawer m_drawer;

    private void Start()
    {
        m_focusRenderer.material.mainTexture = GameManager.Instance.CurrentLevelGoalTexture;

        var tex = new RenderTexture(m_drawTextureWidth, m_drawTextureHeight, 0, RenderTextureFormat.ARGB32);
        tex.Create();

        var material = m_targetRenderer.material;
        material.mainTexture = tex;

        RenderTexture.active = tex;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = null;

        m_drawer = new TextureDrawer(tex, new Brush(m_brushTexture));
    }

    private void Update()
    {
        if (m_cameraController.IsInFocusMode()) return;

        var mouse_pos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            var draw_pos = GetDrawPosition(mouse_pos);
            if (draw_pos.x == -1 && draw_pos.y == -1) return;

            m_drawer.Draw(draw_pos);
        }
        else if (Input.GetMouseButton(0))
        {
            var draw_pos = GetDrawPosition(mouse_pos);
            if (draw_pos.x == -1 && draw_pos.y == -1) return;

            m_drawer.DrawTo(draw_pos);
        }
    }

    public float GetScore() => new Scoring(m_drawer.RenderTexture, GameManager.Instance.CurrentLevelGoalTexture, m_wastedPixelPenaltyCurve, m_closeEnoughCurve).ScoreResult();

    private Vector2 GetDrawPosition(Vector2 mouse_pos)
    {
        var ray = m_mainCamera.ScreenPointToRay(mouse_pos);

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, m_paintObjLayer))
        {
            if (hit.collider.gameObject.CompareTag("Block")) return Vector2.one * -1;

            var hit_coord = hit.textureCoord;

            float w_scalar = (float)m_drawTextureHeight / m_drawTextureWidth;
            float h_scalar = (float)m_drawTextureWidth / m_drawTextureHeight;

            var pixel_coord = new Vector2(hit_coord.x * w_scalar * m_drawTextureWidth, (1 - hit_coord.y) * h_scalar * m_drawTextureHeight);

            return pixel_coord;
        }

        return Vector2.one * -1;
    }
}
