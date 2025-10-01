using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private Sprite[] m_keySprites;
    [SerializeField] private Sprite m_drawingSprite;

    [SerializeField] private LayerMask m_paintObjLayer;

    [SerializeField] private SpriteRenderer m_renderer;
    [SerializeField] private AudioSource m_source;
    private Camera m_perspective;

    private void Awake()
    {
        m_perspective = Camera.main;
    }

    private void Start()
    {
        StartCoroutine(IEAnimateKey());
    }

    private void Update()
    {
        var mouse_pos = Input.mousePosition;

        var ray = m_perspective.ScreenPointToRay(mouse_pos);

        if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, m_paintObjLayer)) return;

        transform.position = hit.point;

        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            m_renderer.sprite = m_drawingSprite;

            m_source.Play();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(IEAnimateKey());
            m_source.Stop();
        }
    }

    private IEnumerator IEAnimateKey()
    {
        int index = 0;

        while (true)
        {
            m_renderer.sprite = m_keySprites[index];
            index = (index + 1) % m_keySprites.Length;

            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
