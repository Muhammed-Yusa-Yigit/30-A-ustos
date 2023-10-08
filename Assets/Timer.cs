using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{

    public UnityEngine.UI.Button btn;
    public Text zaman, can, Durum;
    float zamanSayaci = 120;
    float canSayaci = 3;
    bool oyunDevam = true;
    bool oyunTamam = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(oyunDevam && !oyunTamam)
        {
            zamanSayaci -= Time.deltaTime;
            zaman.text = (int)zamanSayaci + "";
        }
        else if(!oyunTamam)
        {
            Durum.text = "Hedefe varılamadı";
            btn.gameObject.SetActive(true);
     
        }
        if (zamanSayaci < 0)
        {
            oyunDevam = false;
        }
    }

    private void OnCollisionEnter (Collision other)
    {
        string objIsmi = other.gameObject.name;
        if (objIsmi.Equals("End"))
        {
            //print("Oyunu kazandınız");
            oyunTamam = true;
            Durum.text = "Hedefe Vardınz,Tebrikler";
            btn.gameObject.SetActive(true) ;
        }
        else if (other.gameObject.CompareTag("Block"))
        {

            canSayaci -= 1;
            can.text = canSayaci + "";
            if (canSayaci == 0)
            {
                oyunDevam = false;
            }
        }
    }
    //=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*//


    
  
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";

        private float horizontalInput;
        private float verticalInput;
        private float currentSteerAngle;
        private float currentbreakForce;
        private bool isBreaking;

        [SerializeField] private float motorForce;
        [SerializeField] private float breakForce;
        [SerializeField] private float maxSteerAngle;

        [SerializeField] private WheelCollider frontLeftWheelCollider;
        [SerializeField] private WheelCollider frontRightWheelCollider;
        [SerializeField] private WheelCollider rearLeftWheelCollider;
        [SerializeField] private WheelCollider rearRightWheelCollider;

        [SerializeField] private Transform frontLeftWheelTransform;
        [SerializeField] private Transform frontRightWheelTransform;
        [SerializeField] private Transform rearLeftWheelTransform;
        [SerializeField] private Transform rearRightWheelTransform;





        private void FixedUpdate()
        {
        if (oyunDevam && !oyunTamam)
        {
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();

            // frontLeftWheelCollider.motorTorque = 0.17f * motorForce;
        }
        else
        {
            motorForce = 0;
        }

    }
   

        private void GetInput()
        {
            horizontalInput = Input.GetAxis(HORIZONTAL);
            verticalInput = Input.GetAxis(VERTICAL);
            isBreaking = Input.GetKey(KeyCode.Space);
        }

        private void HandleMotor()
        {
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
            currentbreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();
        }

        private void ApplyBreaking()
        {
            frontRightWheelCollider.brakeTorque = currentbreakForce;
            frontLeftWheelCollider.brakeTorque = currentbreakForce;
            rearLeftWheelCollider.brakeTorque = currentbreakForce;
            rearRightWheelCollider.brakeTorque = currentbreakForce;
        }

        private void HandleSteering()
        {
            currentSteerAngle = maxSteerAngle * horizontalInput;
            frontLeftWheelCollider.steerAngle = currentSteerAngle;
            frontRightWheelCollider.steerAngle = currentSteerAngle;
        }

        private void UpdateWheels()
        {
            UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
            UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
            UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
            UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        }

        private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Vector3 pos;
            Quaternion rot;
            wheelCollider.GetWorldPose(out pos, out rot);
            wheelTransform.rotation = rot;
            wheelTransform.position = pos;
        }

    }



