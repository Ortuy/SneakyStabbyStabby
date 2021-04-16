using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks
{
    public PhotonView photonView;

    public Health health;
    public Rigidbody2D rigidBody;
    public GameObject playerCamera, playerViewCone, playerViewCone2, rotatingBody,pointLight2d,gel, legs, HUD;
    private Camera usedCameraComponent;
    private Vector2 moveDirection;
    public GameObject boltObject;
    public GameObject spikePitObject;
    public GameObject tripwireObject;
    public GameObject blidingtrapObject;
    public GameObject bombObject;
    public GameObject gelTrapObject;
    public GameObject paintObject;
    public Transform firePos;
    public Transform dropPos;
    public Transform paintPos;
    public bool disableInput = false;
    public bool isBlinking;
    public float timeBlindedRemaining;
    public bool timerBlindedRunning = false;
    public float timeShineRemaining;
    public bool timerShineRunning = false;
    public float timePaintRemaining;
    public bool timerPaintRunning = false;
    public float timePaintRemaining2;
    public bool timerPaintRunning2 = false;
    public float distance;
    public float sprint = 10;
    public float pasiveItemTimeWorking = 20;
    public float timeSprintRemaining = 4;
    public bool timerSprintRunning = false;
    public bool timerSprintRunning2 = true;
    public GameObject stabHitBox;
    public int stabCooldown;
    public bool stabReady = true, isBehindOtherPlayer;
    public bool canUsePotion = true;
    public int camoNum = 0;

    private int selectedTrapID;
    

    public float moveSpeed, shootSpeed;

    public int playerID;

    public SpriteRenderer[] recolorSprites;
    public SpriteRenderer[] nonRecolorSprites;
    public SpriteRenderer[] ghostSprites;

    [SerializeField] private Animator legsAnimator, torsoAnimator;

    public Inventory inventory;
    public Text stabCooldownText, potionCooldownText;

    public ParticleSystem footstep, camoFX, stimFX, visionFX;
    public SpriteRenderer glowingFootstep;

    [SerializeField] private Slider staminaBar;
    private bool sprintPotionActive;

    [SerializeField] public GameObject[] camoObjects;

    public bool waitingForTrap, settingTrap;
    [SerializeField] private GameObject trapMarker;
    [SerializeField] private Sprite[] trapImages;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        usedCameraComponent = playerCamera.GetComponent<Camera>();

        if (photonView.IsMine)
        {
            playerCamera.SetActive(true);
            playerViewCone.SetActive(true);
            playerViewCone2.SetActive(false);
            pointLight2d.SetActive(true);
            inventory.gameObject.SetActive(true);
            inventory.transform.SetParent(null);
            gel.SetActive(false);
            HUD.SetActive(true);

            if (GameManager.localInstance.playerAmount == 0)
            {
                //GameManager.instance.SpawnDecor();
            }

            photonView.RPC("RegisterPlayer", RpcTarget.AllBuffered);
            
        }

        //legsAnimator = GetComponent<Animator>();

        moveSpeed = 5;

        

        //playerCamera.transform.SetParent(null);
    }
    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(photonView.IsMine && !disableInput)
        {
            CheckInput();
        }
        if (photonView.IsMine &&timerBlindedRunning)
        {
            if (timeBlindedRemaining > 0)
            {
                timeBlindedRemaining -= Time.deltaTime;
                
            }
            else
            {
                
                timeBlindedRemaining = 3;
                timerBlindedRunning = false;
                playerViewCone.SetActive(true);
                
            }
        }

        if (photonView.IsMine && timerShineRunning)
        {
            if (timeShineRemaining > 0)
            {
                timeShineRemaining -= Time.deltaTime;

            }
            else
            {

                timeShineRemaining = 3;
                timerShineRunning = false;
                gel.SetActive(false);
                
            }
        }
        if (photonView.IsMine && timerPaintRunning2)
        {
            timerPaintRunning = true;
            if (timePaintRemaining2 > 0)
            {
                
                timePaintRemaining2 -= Time.deltaTime;

            }
            else
            {
                timePaintRemaining2 = 20;
                timerPaintRunning2 = false;

            }
        }
        if (!photonView.IsMine && timerShineRunning)
        {
            if (timeShineRemaining > 0)
            {
                timeShineRemaining -= Time.deltaTime;

            }
            else
            {

                timeShineRemaining = 3;
                timerShineRunning = false;
                gel.SetActive(false);

            }
        }
        if (!photonView.IsMine && timerPaintRunning2)
        {
            timerPaintRunning = true;
            if (timePaintRemaining2 > 0)
            {

                timePaintRemaining2 -= Time.deltaTime;

            }
            else
            {
                timePaintRemaining2 = 20;
                timerPaintRunning2 = false;

            }
            if (timePaintRemaining > 0)
            {
                timePaintRemaining -= Time.deltaTime;


            }
            else
            {
                timePaintRemaining = 3;

                Paint();
            }
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
        if(!settingTrap)
        {
            float moveForward = Input.GetAxisRaw("Vertical");
            float strife = Input.GetAxisRaw("Horizontal");

            moveDirection = new Vector2(strife, moveForward).normalized;
            Vector2 dir = GetDirectionFromMouse();
        }      

        if (Input.GetButtonDown("Fire2") && !settingTrap)
        {
            //Shoot();
            inventory.UseItem();
        }
        
        if(Input.GetButtonUp("Fire2") && waitingForTrap && !settingTrap)
        {
            torsoAnimator.SetBool("Stab", true);
            waitingForTrap = false;
            StartCoroutine(SetTrap());
        }

        if(Input.GetButtonDown("Fire1") && stabReady && !waitingForTrap && !settingTrap)
        {
            Stab();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            inventory.UsePassiveItem();
        }

        var scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll != 0)
        {
            inventory.ChangeSelectedSlot(scroll);
        }

    }

    public void StartTrapPlacement(int trapID)
    {
        trapMarker.SetActive(true);
        trapMarker.GetComponent<SpriteRenderer>().sprite = trapImages[trapID];
        selectedTrapID = trapID;
        torsoAnimator.SetBool("Trap", true);
        waitingForTrap = true;
    }

    IEnumerator SetTrap()
    {
        settingTrap = true;
        trapMarker.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        torsoAnimator.SetBool("Stab", false);
        torsoAnimator.SetBool("Trap", false);

        yield return new WaitForSeconds(0.3f);
        settingTrap = false;
        switch (selectedTrapID)
        {
            case 0:
                Spikepit();
                break;
            case 1:
                Tripwire();
                break;
            case 2:
                Blindingtrap();
                break;
            case 3:
                Bomb();
                break;
            case 4:
                Geltrap();
                break;
        }

    }

    IEnumerator Blink()
    {
        var durationLeft = 0.2f;
        var blinkDirection = GetDirectionFromMouse();
        var blinkSpeed = distance / durationLeft;

        rigidBody.velocity = new Vector2(blinkDirection.x * blinkSpeed, blinkDirection.y * blinkSpeed);
        disableInput = true;
        isBlinking = true;

        while(durationLeft > 0)
        {
            durationLeft -= Time.deltaTime;
            yield return null;
        }

        disableInput = false;
        isBlinking = false;
    }

    IEnumerator LockStabbing()
    {
        //yield return null;
        //animator.SetBool("Stab", false);
        torsoAnimator.SetBool("Loaded", false);

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

        torsoAnimator.SetBool("Loaded", true);
    }

    public void PlayFootstep()
    {
        footstep.Play();

        if(timerPaintRunning2)
        {
            //photonView.RPC("SpawnGlowingFootstep", RpcTarget.AllBuffered);
            SpawnGlowingFootstep();
        }
    }

    //[PunRPC]
    private void SpawnGlowingFootstep()
    {
        GameObject foot = null;

        if(photonView.IsMine)
        {
            foot = PhotonNetwork.Instantiate/*RoomObject*/(glowingFootstep.name, transform.position, legs.transform.rotation);
            //foot.GetComponent<SpriteRenderer>().flipX = legs.GetComponent<SpriteRenderer>().flipX;
            //Destroy(foot, 16);
            float f = 16f;
            foot.GetComponent<PhotonView>().RPC("SetFootstep", RpcTarget.AllBuffered, legs.GetComponent<SpriteRenderer>().flipX, f);
        }
        
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
        if(!isBlinking)
        {
            rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }
        
        if (Input.GetKey(KeyCode.LeftShift)&& timeSprintRemaining !=0 && timerSprintRunning2)
        {
            rigidBody.velocity = new Vector2(moveDirection.x * sprint, moveDirection.y * sprint);
            timerSprintRunning = true;
        }
        else
        {
            timerSprintRunning = false;
        }
        
        if(moveDirection != Vector2.zero)
        {
            legsAnimator.SetBool("Moving", true);
            var newAngle = Mathf.Rad2Deg * Mathf.Atan2(moveDirection.y, moveDirection.x) - 90;
            legs.transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
        }
        else
        {
            legsAnimator.SetBool("Moving", false);
        }
        if (photonView.IsMine && timerSprintRunning)
        {
            if (timeSprintRemaining >= 0)
            {
                timeSprintRemaining -= Time.deltaTime;

                float value;

                if (sprintPotionActive)
                {
                    value = timeSprintRemaining / 8;
                }
                else
                {
                    value = timeSprintRemaining / 4;
                }
                staminaBar.value = value;
            }
            else
            {

                timeSprintRemaining = 0;
                timerSprintRunning = false;


            }
            

        }
        if (photonView.IsMine && timerSprintRunning==false)
        {
            timerSprintRunning2 = false;
            
            timeSprintRemaining += Time.deltaTime;

            float value;

            if(sprintPotionActive)
            {
                value = timeSprintRemaining / 8;
            }
            else
            {
                value = timeSprintRemaining / 4;
            }

            
            if (timeSprintRemaining >=8 && canUsePotion == false)
            {

                timeSprintRemaining = 8;
                timerSprintRunning2 = true;

                value = 1;

            }
            if (timeSprintRemaining >= 4 && canUsePotion == true)
            {

                timeSprintRemaining = 4;
                timerSprintRunning2 = true;

                value = 1;

            }
            staminaBar.value = value;

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
        StartCoroutine(HandleStabAnimation());
        StartCoroutine(WaitAndDeactivateStab());               
    }

    IEnumerator HandleStabAnimation()
    {
        bool crossbow = torsoAnimator.GetBool("Crossbow");

        torsoAnimator.SetBool("Crossbow", false);

        yield return null;

        torsoAnimator.SetBool("Stab", true);

        torsoAnimator.SetBool("Crossbow", crossbow);
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
        
        
        GameManager.localInstance.playerAmount++;
        playerID = GameManager.localInstance.playerAmount;
    }

    [PunRPC]
    private void ActivateStabHitBox()
    {
        torsoAnimator.SetBool("Stab", false);
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
    [PunRPC]
    public void Stop(float amount)
    {
        moveSpeed = amount;
    }
    [PunRPC]
    public void Blinded(bool amount)
    {
        playerViewCone.SetActive(amount);
        timerBlindedRunning = true;
        
    }
    [PunRPC]
    public void Shine(bool amount)
    {
        gel.SetActive(amount);
        timerShineRunning = true;
        
    }
    [PunRPC]
    public void Paint(bool amount)
    {

        
        timerPaintRunning2 = amount;


    }
    public void Blinking()
    {
        StartCoroutine(Blink());
    }

    public void Shoot()
    {
        torsoAnimator.SetBool("Stab", true);
        StartCoroutine(LockStabbing());
        GameObject obj = PhotonNetwork.Instantiate(boltObject.name, new Vector2(firePos.transform.position.x, firePos.transform.position.y), rotatingBody.transform.rotation, 0);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.AddForce(firePos.up * shootSpeed, ForceMode2D.Impulse);
        StartCoroutine(UpdateShotBool());
    }

    IEnumerator UpdateShotBool()
    {
        yield return null;
        torsoAnimator.SetBool("Stab", false);
    }

    public void SetCrossbowAnimation(bool value)
    {
        torsoAnimator.SetBool("Crossbow", value);
    }

    public void Spikepit()
    {
        GameObject obj = PhotonNetwork.Instantiate(spikePitObject.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), rotatingBody.transform.rotation, 0);
        //Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
    }
    public void Tripwire()
    {
        GameObject obj = PhotonNetwork.Instantiate(tripwireObject.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), rotatingBody.transform.rotation, 0);
        //Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
    }
    public void Blindingtrap()
    {
        GameObject obj = PhotonNetwork.Instantiate(blidingtrapObject.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), rotatingBody.transform.rotation, 0);
        //Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
    }
    public void Bomb()
    {
        GameObject obj = PhotonNetwork.Instantiate(bombObject.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), rotatingBody.transform.rotation, 0);
        //Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
    }
    public void Geltrap()
    {
        GameObject obj = PhotonNetwork.Instantiate(gelTrapObject.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), rotatingBody.transform.rotation, 0);
        //Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
    }
    public void Paint()
    {
        //GameObject obj = PhotonNetwork.Instantiate(paintObject.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), rotatingBody.transform.rotation, 0);
        //Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
    }
    public void SeePotion()
    {
        if (photonView.IsMine)
        {
            visionFX.Play();
            playerViewCone.SetActive(false);
            playerViewCone2.SetActive(true);
            StartCoroutine("PotionStopWorking");
        }
            
    }
    IEnumerator PotionStopWorking()
    {
        int timeLeft = Mathf.FloorToInt(pasiveItemTimeWorking);
        potionCooldownText.transform.parent.gameObject.SetActive(true);       

        while (timeLeft > 0)
        {
            potionCooldownText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1);
            timeLeft--;
        }

        potionCooldownText.transform.parent.gameObject.SetActive(false);

        if (photonView.IsMine)
        {
            playerViewCone2.SetActive(false);
            playerViewCone.SetActive(true);
            canUsePotion = true;
        }
    }

    public void SprintPotion()
    {
        if (photonView.IsMine)
        {
            stimFX.Play();
            timeSprintRemaining = 8;
            sprintPotionActive = true;
            StartCoroutine("PotionStopWorking2");
        }

    }
    IEnumerator PotionStopWorking2()
    {
        int timeLeft = Mathf.FloorToInt(pasiveItemTimeWorking);
        potionCooldownText.transform.parent.gameObject.SetActive(true);

        while (timeLeft > 0)
        {
            potionCooldownText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1);
            timeLeft--;
        }

        potionCooldownText.transform.parent.gameObject.SetActive(false);

        if (photonView.IsMine)
        {
            timeSprintRemaining = 4;
            sprintPotionActive = false;
            canUsePotion = true;
        }
    }
    [PunRPC]
    public void CamoSpell(int variant)
    {
        camoFX.Play();

        camoObjects[variant].SetActive(true);

        foreach(SpriteRenderer renderer in recolorSprites)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
        }

        foreach (SpriteRenderer renderer in nonRecolorSprites)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
        }

        StartCoroutine("CamoStopWorking");
    }
    IEnumerator CamoStopWorking()
    {
        int timeLeft = Mathf.FloorToInt(pasiveItemTimeWorking);
        potionCooldownText.transform.parent.gameObject.SetActive(true);

        while (timeLeft > 0)
        {
            potionCooldownText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1);
            timeLeft--;
        }

        potionCooldownText.transform.parent.gameObject.SetActive(false);

        foreach (SpriteRenderer renderer in recolorSprites)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
        }

        foreach (SpriteRenderer renderer in nonRecolorSprites)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
        }

        /*
        dis.SetActive(false);
        dis1.SetActive(false);
        dis2.SetActive(false);
        dis3.SetActive(false);
        */
        foreach(GameObject camo in camoObjects)
        {
            camo.SetActive(false);
        }

        canUsePotion = true;
        camoNum = 0;
        /*
        if (photonView.IsMine)
        {
            
            dis.SetActive(false);
            dis1.SetActive(false);
            dis2.SetActive(false);
            dis3.SetActive(false);

            canUsePotion = true;
            camoNum = 0;
        }
        */
    }
    
    
}
