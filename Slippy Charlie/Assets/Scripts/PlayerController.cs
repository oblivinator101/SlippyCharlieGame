using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public float speed = 150f;
    //public float strafeSpeed;
    public float jumpForce;

    public Rigidbody hips;
    public Rigidbody torso;
    private ConfigurableJoint hipJoint;
    private JointDrive hipJointDrive;

    public bool isGrounded = true;
    public bool isMoving = false;
    private bool playerIsDead = false;

    private float AngDriveYZ_PositionSpring_StartingValue;
    private float AngDriveYZ_PositionSpring_CurrentValue;

    // Start is called before the first frame update
    void Start()
    {
        hipJoint = hips.gameObject.GetComponent<ConfigurableJoint>();
        AngDriveYZ_PositionSpring_StartingValue = hipJoint.angularYZDrive.positionSpring;
        AngDriveYZ_PositionSpring_CurrentValue = AngDriveYZ_PositionSpring_StartingValue;
        hipJointDrive = hipJoint.angularYZDrive;
        isGrounded = true;
    }


    public float breakingDelta = 100f;
    public float recoveryDelta = 50f;


    // Update is called once per frame
    void Update()
    {
        hipJointDrive.positionDamper = hipJoint.angularYZDrive.positionDamper;
        hipJointDrive.maximumForce = hipJoint.angularYZDrive.maximumForce;
        hipJointDrive.useAcceleration = hipJoint.angularYZDrive.useAcceleration;

        if (!playerIsDead)
        {
            if (hipJoint != null)
            {
                // Just keep these values the same as the component in editor.
                
                //What we really want is to change the Angular Drive YZ Position Spring.
                if (isMoving == true && (hips.rotation.x > .03f || hips.rotation.x < -.02f))
                {
                    AngDriveYZ_PositionSpring_CurrentValue = MoveTowards(AngDriveYZ_PositionSpring_CurrentValue, 0, breakingDelta * Time.deltaTime);
                    if (hipJoint.angularYZDrive.positionSpring <= 50f)
                    {
                        playerIsDead = true;
                    }
                }
                else
                {

                    AngDriveYZ_PositionSpring_CurrentValue = MoveTowards(AngDriveYZ_PositionSpring_CurrentValue, AngDriveYZ_PositionSpring_StartingValue, recoveryDelta * Time.deltaTime);
                }

                hipJointDrive.positionSpring = AngDriveYZ_PositionSpring_CurrentValue;
                hipJoint.angularYZDrive = hipJointDrive;
                Debug.Log(AngDriveYZ_PositionSpring_CurrentValue);

            }
        }
        else
        {

            hipJointDrive.positionSpring = 0;
            hipJoint.angularYZDrive = hipJointDrive;
        }
     
    }

    public float MoveTowards(float current, float target, float maxDelta)
    {
        if (Mathf.Abs(target - current) <= maxDelta)
        {
            return target;
        }
        return current + Mathf.Sign(target - current) * maxDelta;
    }

    private void FixedUpdate()
    {
        if (!playerIsDead)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetAxis("Jump") > 0)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    if(hips != null)
                    {
                        hips.AddForce(hips.transform.forward * speed * 1.5f);
                    }
                    if(torso != null)
                    {
                        torso.AddForce(torso.transform.forward * speed * 1f);
                    }
                   
                }
                if (Input.GetKey(KeyCode.S))
                {
                    if (hips != null)
                    {
                        hips.AddForce(-hips.transform.forward * speed * 1.5f);
                    }
                    if (torso != null)
                    {
                        torso.AddForce(-torso.transform.forward * speed * 1f);
                    }
                }
                if (Input.GetAxis("Jump") > 0)
                {
                    if(isGrounded)
                    {
                        hips.AddForce(new Vector3(0, jumpForce, 0));
                        isGrounded = false;
                    }
                }
                    isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }

    }
}
