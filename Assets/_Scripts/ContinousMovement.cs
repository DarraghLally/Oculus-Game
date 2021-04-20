using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinousMovement : MonoBehaviour
{
    public XRNode inputSource;
    public float speed = 1;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;

    private XRRig rig;
    private Vector2 inputAxis;
    private CharacterController character;
    private float fallingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource); // Find relevent device
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis); // Get users actions from analog controller
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        // Get access to head game object, use to rotate
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y); // get the direction
        // set direction to view direction
        character.Move(direction * Time.fixedDeltaTime * speed); // move

        // Deal with gravity
        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
            fallingSpeed = 0; // dont fall
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime; // increment fall speed, for realism
        }
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight; // set height of capsule to height of rig
        Vector3 capsuleCentre = transform.InverseTransformPoint(rig.cameraGameObject.transform.position); // get capsule center
        character.center = new Vector3(capsuleCentre.x, character.height / 2 + character.skinWidth, capsuleCentre.z); // vertical positioning
    }

    bool CheckIfGrounded()
    {
        // Let us know if on the ground, so not to apply falling mechanic
        // Using sphere ray cast
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
}
