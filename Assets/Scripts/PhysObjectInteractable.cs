using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PhysObjectInteractable : XRGrabInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool IsHoverableBy(XRBaseInteractor interactor)
    {
        return Vector3.Distance(interactor.attachTransform.position, transform.position) <= 0.3f;
    }
}
