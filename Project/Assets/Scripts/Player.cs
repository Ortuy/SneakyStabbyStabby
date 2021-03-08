using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks
{
    public PhotonView photonView;

    public Rigidbody2D rigidBody;
    public GameObject playerCamera, playerViewCone, rotatingBody, legs;
    private Camera usedCameraComponent;
    private Vector2 moveDirection;
    public GameObject boltObject;
    public Transform firePos;
    public bool disableInput = false;

    public GameObject stabHitBox;
    public int stabCooldown;
    public bool stabReady = true, isBehindOtherPlayer;

    public float moveSpeed, shootSpeed;

    public int playerID;

    public SpriteRenderer[] recolorSprites;

    private Animator animator;

    public Inventory inventory;
    public Text stabCooldownText;

    public ParticleSystem footstep;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        usedCameraComponent = playerCamera.GetComponent<Camera>();

        if (photonView.IsMine)
        {
            playerCamera.SetActive(true);
            playerViewCone.SetActive(true);
            inventory.gameObject.SetActive(true);
            inventory.transform.SetParent(null);
            photonView.RPC("RegisterPlayer", RpcTarget.AllBuffered);
            
        }

        animator = GetComponent<Animator>();

        //playerCamera.transform.SetParent(null);
    }

    // Update is called once per frame
    private void Update()
    {
        if(photonView.IsMine && !disableInput)
        {
            CheckInput();
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine)
            return;
        if (collision.CompareTag("Backside"))
        {
            isBehindOtherPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!photonView.IsMine)
            return;
        if (collision.CompareTag("Backside"))
        {
            isBehindOtherPlayer = false;
        }
    }

    private void CheckInput()
    {
        float moveForward = Input.GetAxisRaw("Vertical");
        float strife = Input.GetAxisRaw("Horizontal");

        moveDirection = new Vector2(strife, moveForward).normalized;

        Vector2 dir = GetDirectionFromMouse();

        if (Input.GetButtonDown("Fire2") && stabReady)
        {
            Shoot();
        }
        
        if(Input.GetButtonDown("Fire1") && stabReady)
        {
            Stab();
        }

        var scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll != 0)
        {
            inventory.ChangeSelectedSlot(scroll);
        }
    }

    IEnumerator LockStabbing()
    {
        //yield return null;
        //animator.SetBool("Stab", false);
        stabCooldownText.transform.parent.gameObject.SetActive(true);
        stabCooldownText.text = stabCooldown.ToString();
        stabReady = false;
        for(int i = 0; i < stabCooldown; i++)
        {
            yield return new WaitForSeconds(1f);
            stabCooldownText.text = (stabCooldown - i - 1).ToString();
        }
        stabCooldownText.transform.parent.gameObject.SetActive(false);
        stabReady = true;
    }

    public void PlayFootstep()
    {
        footstep.Play();
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
        
        if(moveDirection != Vector2.zero)
        {
            animator.SetBool("Moving", true);
            var newAngle = Mathf.Rad2Deg * Mathf.Atan2(moveDirection.y, moveDirection.x) - 90;
            legs.transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    [PunRPC]
    public void SetColor(float newR, float newG, float newB)
    {
        foreach(SpriteRenderer renderer in recolorSprites)
        {
            renderer.color = new Color(newR, newG, newB);
        }
    }

    private void Stab()
    {
        StartCoroutine(LockStabbing());
        StartCoroutine(WaitAndDeactivateStab());
        animator.SetBool("Stab", true);
        
    }

    IEnumerator WaitAndDeactivateStab()
    {
        yield return new WaitForSeconds(0.3f);
        photonView.RPC("ActivateStabHitBox", RpcTarget.AllBuffered);
        yield return new WaitForSeconds(0.15f);
        photonView.RPC("DeactivateStab", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void DeactivateStab()
    {
        stabHitBox.SetActive(false);
    }

    [PunRPC]
    private void RegisterPlayer()
    {
        GameManager.instance.playerAmount++;
        playerID = GameManager.instance.playerAmount;
    }

    [PunRPC]
    private void ActivateStabHitBox()
    {
        animator.SetBool("Stab", false);
        stabHitBox.SetActive(true);

        var hBox = stabHitBox.GetComponent<StabHitBox>();

        if (isBehindOtherPlayer)
        {
            hBox.valid = true;
        }
        else
        {
            hBox.valid = false;
        }
    }

    private void Shoot()
    {
        StartCoroutine(LockStabbing());
        GameObject obj = PhotonNetwork.Instantiate(boltObject.name, new Vector2(firePos.transform.position.x, firePos.transform.position.y), rotatingBody.transform.rotation, 0);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.AddForce(firePos.up * shootSpeed, ForceMode2D.Impulse);
    }
}
