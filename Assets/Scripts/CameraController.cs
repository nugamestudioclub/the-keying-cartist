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
    private Vector3 forwardDirection;

    [Space(10)]

    [SerializeField] private float m_cameraSpeed = 4f;
    [SerializeField] private float cameraRotationSpeed = 5f;

    [SerializeField] private Transform m_leftSideBound;
    [SerializeField] private Transform m_rightSideBound;
    [SerializeField] private Transform m_peekAnchor;

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

    public bool IsInFocusMode() => m_isPulledBack;

    private void Start()
    {
        forwardDirection = m_camera.forward;

        peekTarget = GameObject.FindGameObjectWithTag("Driver");
    }

    private void FixedUpdate()
    {
        float x_direction = Input.GetAxis("Horizontal");
        float pullback = Input.GetAxis("Vertical");

        // while peeking, you cant Focus or Pan the camera
        peeking = Input.GetKey(peekKeybind);

        if (peeking && m_anchorTarget != m_peekAnchor)
        {
            m_anchorTarget = m_peekAnchor;
            m_isPulledBack = false;
        }
        else if (!peeking && m_anchorTarget == m_peekAnchor)
        {
            m_anchorTarget = m_boundAnchor;
        }

        // move the camera anchor for the bounds if horizontal input provided
        if (x_direction != 0) MoveBoundsAnchor(x_direction);
        
        // if we're to pull the camera back and aren't already doing so, swap the anchor we're using
        if (!peeking && pullback < -0.05f && !m_isPulledBack)
        {
            m_anchorTarget = m_pullbackAnchor;
            m_isPulledBack = true;
        }
        else if (!peeking && pullback >= -0.05f && m_isPulledBack) // and vice versa
        {
            m_anchorTarget = m_boundAnchor;
            m_isPulledBack = false;
        }

        LerpCamera();
    }

    private void LerpCamera()
    {
        Quaternion targetRotation;
        if (peeking && peekTarget != null)
        {
            targetRotation = Quaternion.LookRotation((peekTarget.transform.position - m_camera.position).normalized);
        } 
        else
        {
            targetRotation = m_isPulledBack ? m_pullbackAnchor.rotation : Quaternion.LookRotation(forwardDirection);
        }

        m_camera.rotation = Quaternion.Lerp(m_camera.rotation, targetRotation, cameraRotationSpeed * Time.fixedDeltaTime);

        m_focusHandler.ChangeFocus(m_isPulledBack);

        m_camera.position = Vector3.Lerp(m_camera.position, m_anchorTarget.position, m_cameraSpeed * Time.fixedDeltaTime);
    }

    private void MoveBoundsAnchor(float z_dir)
    {
        // everything here is flipped (</>, +/-) to account for being rotated 180 degrees in world.
        var vec = m_boundAnchor.position;
        vec.z += z_dir * m_cameraSpeed * Time.fixedDeltaTime;

        m_boundAnchor.position = vec;

        if (m_boundAnchor.position.z < m_leftSideBound.position.z)
        {
            m_boundAnchor.position = m_leftSideBound.position;
        }
        else if (m_boundAnchor.position.z > m_rightSideBound.position.z)
        {
            m_boundAnchor.position = m_rightSideBound.position;
        }
    }
}
