using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverHandler : MonoBehaviour
{
    [SerializeField] Transform snapPosition;
    [SerializeField] float rotationMultiplier = 5;

    float min = 30f;
    float max = 150f;

    [SerializeField] XRController rightHandController;
    [SerializeField] XRController leftHandController;

    public bool leftGrabPressed;
    public bool rightGrabPressed;

    GameObject rightHand;
    private Transform rightHandOriginalParent;
    bool rightHandOnLever = false;

    GameObject leftHand;
    private Transform leftHandOriginalParent;
    bool leftHandOnLever = false;

    float baseHandY;
    float difference;

    [SerializeField] Transform v1;
    [SerializeField] Transform v2;

    private void Start()
    {
        transform.localRotation.eulerAngles.Set(0, 0, 60);

        snapPosition = transform.GetChild(0);

        leftHand = leftHandController.transform.Find("Hands").gameObject;
        rightHand = rightHandController.transform.Find("Hands").gameObject;

        rightHandOriginalParent = rightHandController.transform;
        leftHandOriginalParent = leftHandController.transform;
    }


    private void Update()
    {

        leftHandController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out leftGrabPressed);
        rightHandController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out rightGrabPressed);

        TransformHandPositionToRotation();
        ClampRotation();
        ReleasehandsFromLever();
    }

    float returnAngle()
    {
        var direction = v2.position - v1.position;
        direction.Normalize();

        var angle = Vector3.Angle(direction, v1.forward);

        return angle;
    }

    private void ClampRotation()
    {
        if (returnAngle() >= max)
        {

            Quaternion newRot = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, -60);
            transform.localRotation = newRot;
        }
        else if (returnAngle() <= min)
        {
            Quaternion newRot = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 60);
            transform.localRotation = newRot;
        }
        else
        {
            transform.localRotation = transform.localRotation;
        }
    }

    void PlacehandOnLever(ref GameObject hand, ref Transform originalParent, ref bool handOnLever)
    {
        originalParent = hand.transform.parent;

        hand.transform.parent = snapPosition.transform;
        hand.transform.position = snapPosition.transform.position;

        handOnLever = true;
    }

    void TransformHandPositionToRotation()
    {
        if (leftHandOnLever)
        {
            difference = leftHandController.transform.localPosition.y - baseHandY;

            transform.Rotate(new Vector3(0, 0, difference * rotationMultiplier), Space.Self);
        }

        if (rightHandOnLever)
        {
            difference = rightHandController.transform.localPosition.y - baseHandY;

            transform.Rotate(new Vector3(0, 0, difference * rotationMultiplier), Space.Self);
        }
    }

    void ReleasehandsFromLever()
    {
        if (rightHandOnLever == true && !rightGrabPressed)
        {
            rightHand.transform.parent = rightHandOriginalParent;
            rightHand.transform.position = rightHandOriginalParent.position;
            rightHand.transform.rotation = rightHandOriginalParent.rotation;
            rightHandOnLever = false;
        }
        if (leftHandOnLever == true && !leftGrabPressed)
        {
            leftHand.transform.parent = leftHandOriginalParent;
            leftHand.transform.position = leftHandOriginalParent.position;
            leftHand.transform.rotation = leftHandOriginalParent.rotation;
            leftHandOnLever = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LeftHand")
        {
            if (rightHandOnLever == false && leftHandOnLever == false && leftGrabPressed)
            {
                baseHandY = leftHandController.transform.localPosition.y;
                PlacehandOnLever(ref leftHand, ref leftHandOriginalParent, ref leftHandOnLever);
            }
        }

        if (other.tag == "RightHand")
        {
            if (rightHandOnLever == false && leftHandOnLever == false && rightGrabPressed)
            {
                baseHandY = rightHandController.transform.localPosition.y;
                PlacehandOnLever(ref rightHand, ref rightHandOriginalParent, ref rightHandOnLever);
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        var direction = v2.position - v1.position;
        direction.Normalize();

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, direction * 10f);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, v1.forward * 10f);
    }
}
