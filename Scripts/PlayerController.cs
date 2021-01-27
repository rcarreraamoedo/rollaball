using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject looseTextObject;
    public GameObject restartButton;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private Vector3 startPos;


    void Main()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        looseTextObject.SetActive(false);
        restartButton.SetActive(false);
        startPos = this.transform.position;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    
    void SetCountText() 
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12) 
        {
            winTextObject.SetActive(true);
            restartButton.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            rb.AddForce(movement * speed);
        }
        else
        {
            Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);
            rb.AddForce(movement * speed);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
        if (other.gameObject.CompareTag("Enemy")){
            this.transform.position=startPos;
            count = count -1;
            if (count<0){
                looseTextObject.SetActive(true);
                speed=0;
                movementX=0;
                movementY=0;
                restartButton.SetActive(true);
            }
            SetCountText();
        }
    }
}