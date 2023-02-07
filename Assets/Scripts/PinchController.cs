using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinchController : MonoBehaviour
{
    [SerializeField]
    private PincherController PincherController;

    private bool followPinch = false;

    public void ToggleFollow()
    {
        followPinch = !followPinch;
    }

    private void FixedUpdate()
    {
        if (followPinch)
        {
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose indexRealityPose) &&
                HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out MixedRealityPose thumbRealityPose))
            {
                float distance = Mathf.Clamp(0.5f - 3 * Vector3.Distance(indexRealityPose.Position, thumbRealityPose.Position), 0.0f, 0.5f);
                PincherController.grip = distance;
            }
        }
    }
}
