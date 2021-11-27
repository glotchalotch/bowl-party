/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(UIElement))]
public class VRSlider : MonoBehaviour
{
    //this is the code that i may be the most proud of because a decent chunk of it was just "will this work? i hope it does" and then it did

    public RectTransform slideRect;
    bool active = false;
    Hand curHand;
    Vector3 initialPos;
    public UnityEvent<float> onValueChange;
    public ValueProvider valueProvider;

    public void Activate(Hand hand)
    {
        curHand = hand;
        active = true;
        initialPos = transform.localToWorldMatrix.MultiplyPoint(new Vector3(slideRect.rect.xMax - (transform.localPosition.x - slideRect.rect.x), 0, 0));
    }

    private void Start()
    {
        GetComponent<UIElement>().onHandClick.AddListener(Activate);
    }

    private void Update()
    {
        if (active && curHand != null && curHand.uiInteractAction.state)
        {
            Vector3 adjustedHandPos = curHand.transform.position + (-transform.forward);
            Vector3 dir = adjustedHandPos - initialPos;
            transform.position = initialPos + (transform.right * Mathf.Cos(Vector3.Angle(transform.right, dir) * Mathf.Deg2Rad));
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, slideRect.rect.x, slideRect.rect.xMax), 0);
            onValueChange.Invoke((transform.localPosition.x - slideRect.rect.x) / (slideRect.rect.xMax - slideRect.rect.x));
        } else
        {
            active = false;
            curHand = null;
            Vector3 local = transform.localPosition;
            if (valueProvider != null) local.x = slideRect.rect.x + (valueProvider.GetSliderValue() * (slideRect.rect.xMax - slideRect.rect.x));
            transform.localPosition = local;
        }
    }

    public abstract class ValueProvider : MonoBehaviour
    {
        public abstract float GetSliderValue();
    }

}*/
