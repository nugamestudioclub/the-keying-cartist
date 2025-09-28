using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OwnerWalkingScript : MonoBehaviour
{
    [SerializeField]
    Vector3 initial_position = new Vector3(1, 1, 2);

    // the lengths of these three arrays need to be the same!
    [SerializeField]
    private Vector3[] positions;

    [SerializeField]
    private float[] timeAtEachPosition;

    [SerializeField]
    private float[] lerpSpeedToEachPosition;

    [Space(10)]

    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] walkingFrames;

    [SerializeField]
    private Sprite idleFrame;

    private int locationToMoveTo = 0;
    private bool isAnimating = false;
    private int walkingFrameIndex = 0;

    private Coroutine animatingCoroutine;

    [Space(15)]

    [SerializeField]
    private UnityEvent OnGameEnd;
    


    // Start is called before the first frame update
    void Start()
    {
        transform.position = positions[0];
        StartCoroutine(MoveOwnerAround());
    }

    void FixedUpdate()
    {
        Vector3 newLocation = positions[locationToMoveTo];
        float lerpRate = lerpSpeedToEachPosition[locationToMoveTo];

        var target_pos = Vector3.MoveTowards(transform.position, newLocation, Time.fixedDeltaTime * lerpRate);

        if (target_pos == transform.position)
        {
            SetAnimationState(false);
        }
        else
        {
            SetAnimationState(true);
        }

        transform.position = target_pos;
    }

    private void SetAnimationState(bool state)
    {
        if (state == isAnimating) return;

        isAnimating = state;

        if (state)
        {
            animatingCoroutine = StartCoroutine(IEAnimateWalk());
        }
        else
        {
            StopCoroutine(animatingCoroutine);
            spriteRenderer.sprite = idleFrame;
        }
    }

    private IEnumerator IEAnimateWalk()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.25f / lerpSpeedToEachPosition[locationToMoveTo]);

            walkingFrameIndex = (walkingFrameIndex + 1) % walkingFrames.Length;
            spriteRenderer.sprite = walkingFrames[walkingFrameIndex];
        }
    }

    private IEnumerator MoveOwnerAround()
    {
        for (int i = 0; i < positions.Length; i += 1)
        {
            locationToMoveTo = i;
            yield return new WaitForSeconds(timeAtEachPosition[i]);
        }
        yield return null;

        OnGameEnd?.Invoke();
    }
}
