using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform m_camera;
    private Transform m_anchorTarget;

    private bool m_isPulledBack;

    [SerializeField] private GameObject m_focusOverlay;
    private FocusOverlayHandler m_focusHandler;

    [SerializeField] private GameObject peekTarget;
    [SerializeField] private KeyCode peekKeybind = KeyCode.Q;
    private bool peeking = false;
    private Vector3 startRotation;

    [Space(10)]

    [SerializeField] private float m_cameraSpeed = 4f;
    [SerializeField] private float cameraRotationSpeed = 5f;

    [SerializeField] private Transform m_leftSideBound;
    [SerializeField] private Transform m_rightSideBound;

    private Transform m_boundAnchor;

    [Space(10)]

    [SerializeField] private Transform m_pullbackAnchor;

    private void Awake()
    {
        m_camera = Camera.main.transform;

        m_boundAnchor = new GameObject("BoundAnchor").transform;
        m_boundAnchor.position = (m_leftSideBound.position + m_rightSideBound.position) / 2f;
        
        m_anchorTarget = m_boundAnchor;

        m_focusHandler = new FocusOverlayHandler(m_focusOverlay);
    }

    private void Start()
    {
        startRotation = new Vector3(m_camera.rotation.x, m_camera.rotation.y, m_camera.rotation.z);

        peekTarget = GameObject.FindGameObjectWithTag("Driver");
    }

    private void FixedUpdate()
    {
        float x_direction = Input.GetAxis("Horizontal");
        float pullback = Input.GetAxis("Vertical");

        peeking = Input.GetKey(peekKeybind);

        if (peeking)
        {
            x_direction = 0;
            pullback = 0;
        }

        // move the camera anchor for the bounds if horizontal input provided
        if (x_direction != 0) MoveBoundsAnchor(x_direction);
        
        // if we're to pull the camera back and aren't already doing so, swap the anchor we're using
        if (pullback < -0.05f && !m_isPulledBack)
        {
            m_anchorTarget = m_pullbackAnchor;
            m_isPulledBack = true;
        }
        else if (pullback >= -0.05f && m_isPulledBack) // and vice versa
        {
            m_anchorTarget = m_boundAnchor;
            m_isPulledBack = false;
        }

        LerpCamera();
    }

    private void LerpCamera()
    {
        if (peeking && peekTarget != null)
        {
            Vector3 targetDirection = (peekTarget.transform.position - m_camera.transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Lerp(m_camera.transform.rotation, targetRotation, cameraRotationSpeed * Time.deltaTime);
        } else
        {
            Quaternion targetRotation = Quaternion.LookRotation(startRotation);
            transform.rotation = Quaternion.Lerp(m_camera.transform.rotation, targetRotation, cameraRotationSpeed * Time.deltaTime);

            m_focusHandler.ChangeFocus(m_isPulledBack ? 1f : 0f);

            m_camera.position = Vector3.Lerp(m_camera.position, m_anchorTarget.position, m_cameraSpeed * Time.deltaTime);
        }

    }

    private void MoveBoundsAnchor(float x_direction)
    {
        var vec = m_boundAnchor.position;
        vec.x += x_direction * m_cameraSpeed * Time.fixedDeltaTime;

        m_boundAnchor.position = vec;

        if (m_boundAnchor.position.x < m_leftSideBound.position.x)
        {
            m_boundAnchor.position = m_leftSideBound.position;
        }
        else if (m_boundAnchor.position.x > m_rightSideBound.position.x)
        {
            m_boundAnchor.position = m_rightSideBound.position;
        }
    }
}
