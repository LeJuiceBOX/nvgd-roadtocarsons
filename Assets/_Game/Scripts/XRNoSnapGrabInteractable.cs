using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRNoSnapGrabInteractable : XRGrabInteractable {

    private Vector3 interactorPos = Vector3.zero;
    private Quaternion interactorRot = Quaternion.identity;

    protected override void OnSelectEntering(XRBaseInteractor args) {
        base.OnSelectEntering(args);
        StoreInteractor(args);
        MatchAttachmentPoints(args);
    }

    protected override void OnSelectExiting(XRBaseInteractor args) {
        base.OnSelectExiting(args);
        ResetAttachemntPoint(args);
    }


    // Entering
    private void StoreInteractor(XRBaseInteractor interactor) {
        interactorPos = interactor.transform.position;
        interactorRot = interactor.transform.rotation;
    }
    
    private void MatchAttachmentPoints(XRBaseInteractor interactor) {
        bool hasAttach = attachTransform != null;
        interactor.attachTransform.position = hasAttach ? attachTransform.position : transform.position;
        interactor.attachTransform.rotation = hasAttach ? attachTransform.rotation : transform.rotation;
    }

    //Exiting
    private void ResetAttachemntPoint(XRBaseInteractor interactor) {
        interactorPos = Vector3.zero;
        interactorRot = Quaternion.identity;
        interactor.attachTransform.localPosition = interactorPos;
        interactor.attachTransform.localRotation = interactorRot;
    }
}
