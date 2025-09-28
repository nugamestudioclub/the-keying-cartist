using UnityEngine;

public class FocusOverlayHandler
{
    private readonly Material m_focusMaterial;

    private float m_focusAmount = -1;

    public FocusOverlayHandler(GameObject focus_overlay)
    {
        m_focusMaterial = focus_overlay.GetComponent<Renderer>().material;

        SetFocusAmount(0f);
    }

    private void SetFocusAmount(float amount)
    {
        if (m_focusAmount == amount) return;

        m_focusAmount = amount;
        m_focusMaterial.color = Color.Lerp(Color.clear, Color.white, m_focusAmount);
    }

    public void ChangeFocus(float target)
    {
        if (Mathf.Abs(m_focusAmount - target) < 0.005f)
        {
            SetFocusAmount(target);

            return;
        }

        SetFocusAmount(Mathf.Lerp(m_focusAmount, target, Time.fixedDeltaTime / (2f - target)));
    }
}
