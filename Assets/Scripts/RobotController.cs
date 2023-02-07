using Assets.Joystick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotController : MonoBehaviour
{
    [System.Serializable]
    public struct Joint
    {
        public string inputAxis;
        public GameObject robotPart;
        public JoystickController JoystickController;
    }
    public Joint[] joints;


    // CONTROL

    public void StopAllJointRotations()
    {
        for (int i = 0; i < joints.Length; i++)
        {
            GameObject robotPart = joints[i].robotPart;
            UpdateRotationState(RotationDirection.None, robotPart);
        }
    }

    public void RotateJoint(int jointIndex, RotationDirection direction)
    {
        StopAllJointRotations();
        Joint joint = joints[jointIndex];
        UpdateRotationState(direction, joint.robotPart);
    }

    // HELPERS

    static void UpdateRotationState(RotationDirection direction, GameObject robotPart)
    {
        ArticulationJointController jointController = robotPart.GetComponent<ArticulationJointController>();
        jointController.rotationState = direction;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < joints.Length; i++)
        {
            GameObject robotPart = joints[i].robotPart;
            JoystickController joystickController = joints[i].JoystickController;
            ArticulationJointController articulationJointController = robotPart.GetComponent<ArticulationJointController>();
            if (joystickController != null)
            {
                Transform joystick = joystickController.transform.Find("Joystick");
                if (joystick != null)
                {
                    articulationJointController.speed = Mathf.Abs(100 * joystick.localRotation.x);
                    if (joystick.localRotation.x > 0.0)
                    {
                        articulationJointController.rotationState = RotationDirection.Positive;
                    } else if (joystick.localRotation.x < 0.0)
                    {
                        articulationJointController.rotationState = RotationDirection.Negative;
                    } else
                    {
                        articulationJointController.rotationState = RotationDirection.None;
                        articulationJointController.speed = 0;
                    }
                }
            }
        }
    }

}
