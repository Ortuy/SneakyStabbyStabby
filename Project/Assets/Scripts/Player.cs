using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.MonoBehaviour
{
    public PhotonView photonView;

    public Rigidbody2D rigidBody;
    public GameObject playerCamera, playerViewCone, rotatingBody;
    private Camera usedCameraComponent;
    private Vector2 moveDirection;
    public GameObject boltObject;
    public Transform firePos;
    public bool disableInput = false;
    

    public float moveSpeed, shootSpeed;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        usedCameraComponent = playerCamera.GetComponent<Camera>();

        if (photonView.isMine)
        {
            playerCamera.SetActive(true);
            playerViewCone.SetActive(true);
        }

        //playerCamera.transform.SetParent(null);
    }

    // Update is called once per frame
    private void Update()
    {
        if(photonView.isMine && !disableInput)
        {
            CheckInput();
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void CheckInput()
    {
        float moveForward = Input.GetAxisRaw("Vertical");
        float strife = Input.GetAxisRaw("Horizontal");

        moveDirection = new Vector2(strife, moveForward).normalized;

        Vector2 dir = GetDirectionFromMouse();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        //rigidBody.velocity = (moveForward * moveSpeed * dir) + (-strife * moveSpeed * Vector2.Perpendicular(dir));

        /**
        if (moveForward != 0)
        {
            rigidBody.velocity = moveForward * moveSpeed * GetDirectionFromMouse();
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
        }**/
    }

    private Vector2 GetDirectionFromMouse()
    {
        Vector2 temp = Vector2.zero;

        Vector2 mousePos = usedCameraComponent.ScreenToWorldPoint(Input.mousePosition);

        Vector2 pos2D = transform.position;

        temp = mousePos - pos2D;

        var newAngle = Mathf.Rad2Deg * Mathf.Atan2(temp.y, temp.x) - 90;

        //float newAngle = Vector2.Angle(Vector2.left + pos2D, mousePos);
        rotatingBody.transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
        //playerCamera.transform.rotation = Quaternion.AngleAxis(-newAngle, Vector3.forward);

        return temp.normalized;
    }

    private void Move()
    {
        rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void Shoot()
    {
        GameObject obj = PhotonNetwork.Instantiate(boltObject.name, new Vector2(firePos.transform.position.x, firePos.transform.position.y), rotatingBody.transform.rotation, 0);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.AddForce(firePos.up * shootSpeed, ForceMode2D.Impulse);

    }
}
