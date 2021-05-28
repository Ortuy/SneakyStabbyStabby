using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviourPunCallbacks
{
    public PhotonView photonView;

    public Health health;
    public Rigidbody2D rigidBody;
    public GameObject mapIcon;
    public GameObject mapIconRadar;
    public GameObject playerCamera, playerViewCone, playerViewCone2, rotatingBody,pointLight2d,gel, legs, HUD, Shop,ColorSelect, Buyblind, BlindVignette, audioObject;
    private Camera usedCameraComponent;
    public Camera mapCamera;
    private Vector2 moveDirection;
    public GameObject boltObject;
    public GameObject spikePitObject;
    public GameObject tripwireObject;
    public GameObject blidingtrapObject;
    public GameObject bombObject;
    public GameObject gelTrapObject;
    public GameObject paintObject;
    public GameObject pickup1;
    public GameObject pickup2;
    public GameObject pickup3;
    public GameObject pickup4;
    public GameObject pickup5;
    public GameObject pickup6;
    public GameObject pickup7;
    public GameObject pickup8;
    public GameObject pickup9;
    public GameObject pickup10;
    public GameObject pickup11;
    public GameObject pickup12;
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
    public float R;
    public float G;
    public float B;
    public bool colourChange = false;
    public GameObject Colour;
    public bool stabLock = true;
    public Color colorToSet;
    public Color colorToSet1;
    public Color colorToSet2;
    public Color colorToSet3;
    public Color colorToSet4;
    public Color colorToSet5;
    public Color colorToSet6;
    public Color colorToSet7;
    public int gold;
    public Text goldText, mapNameText, mapNameTextRadar;
    public bool destroyWeb = false;
    public bool isTrapped, isSprinting;




    private int selectedTrapID;
    

    public float moveSpeed, shootSpeed;

    public int playerID;

    public SpriteRenderer[] recolorSprites;
    public SpriteRenderer[] nonRecolorSprites;
    public SpriteRenderer[] ghostSprites;

    [SerializeField] public Animator legsAnimator, torsoAnimator;

    public Inventory inventory;
    public Text stabCooldownText, potionCooldownText;

    public ParticleSystem footstep, footstepStone, footstepWood, camoFX, stimFX, visionFX, blinkFX, crossbowFX, trapFX;
    public SpriteRenderer glowingFootstep;

    [SerializeField] private Slider staminaBar;
    private bool sprintPotionActive;

    [SerializeField] public GameObject[] camoObjects;

    public bool waitingForTrap, settingTrap, isByDetector, isInteracting;
    [SerializeField] private GameObject trapMarker;
    [SerializeField] private Sprite[] trapImages;

    private CameraFollow cFollow;

    public bool visionPotionActive;

    private void Awake()
    {
        health = GetComponent<Health>();

        photonView = GetComponent<PhotonView>();

        usedCameraComponent = playerCamera.GetComponentInChildren<Camera>();

        if (photonView.IsMine)
        {

            StartCoroutine(WaitAFrameAndSetName());
            playerCamera.SetActive(true);
            playerViewCone.SetActive(true);
            playerViewCone2.SetActive(false);
            pointLight2d.SetActive(true);
            inventory.gameObject.SetActive(true);
            inventory.transform.SetParent(null);
            gel.SetActive(false);
            HUD.SetActive(true);
            ColorSelect.SetActive(false);
            mapIcon.SetActive(true);
            mapIconRadar.SetActive(false);
            BlindVignette.SetActive(false);
            audioObject.SetActive(true);

            if (GameManager.localInstance.playerAmount == 0)
            {
                //GameManager.instance.SpawnDecor();
            }

            photonView.RPC("RegisterPlayer", RpcTarget.AllBuffered);
            
        }

        //legsAnimator = GetComponent<Animator>();

        moveSpeed = 5;
        stabLock = true;
        destroyWeb = false;


        //playerCamera.transform.SetParent(null);
    }

    IEnumerator WaitAFrameAndSetName()
    {
        yield return null;
        photonView.RPC("SetMapName", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void SetMapName()
    {
        mapNameText.text = health.playerName;
        mapNameTextRadar.text = health.playerName;
    }

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;


    }

    // Update is called once per frame
    private void Update()
    {
        goldText.text = gold.ToString();

        if (photonView.IsMine && !disableInput)
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
                BlindVignette.SetActive(false);

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
        //if(!settingTrap)
        //{
        //    float moveForward = Input.GetAxisRaw("Vertical");
        //    float strife = Input.GetAxisRaw("Horizontal");

        //    moveDirection = new Vector2(strife, moveForward).normalized;
        //    Vector2 dir = GetDirectionFromMouse();
        //}
        if (!isTrapped && !settingTrap)
        {
            float moveForward = Input.GetAxisRaw("Vertical");
            float strife = Input.GetAxisRaw("Horizontal");

            moveDirection = new Vector2(strife, moveForward).normalized;
            Vector2 dir = GetDirectionFromMouse();
        }


        if (Input.GetButtonDown("Fire2") && !settingTrap && !health.isGhost)
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

        if (Input.GetKeyDown(KeyCode.Q) && !health.isGhost)
        {
            inventory.UsePassiveItem();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameManager.localInstance.ToggleMap();
            if(GameManager.localInstance.mapOut)
            {
                AkSoundEngine.PostEvent("ui_open_map", gameObject, gameObject);
                stabLock = true;
                torsoAnimator.SetBool("Map", true);
            }
            else
            {
                stabLock = false;
                AkSoundEngine.PostEvent("ui_close_map", gameObject, gameObject);
                torsoAnimator.SetBool("Map", false);
            }
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
        trapFX.Play();
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
            case 7:
                Detector();
                break;
        }

    }

    IEnumerator Blink()
    {
        AkSoundEngine.PostEvent("char_footstep_dash", gameObject, gameObject);
        var durationLeft = 0.2f;
        var blinkDirection = GetDirectionFromMouse();
        var blinkSpeed = distance / durationLeft;

        rigidBody.velocity = new Vector2(blinkDirection.x * blinkSpeed, blinkDirection.y * blinkSpeed);
        disableInput = true;
        isBlinking = true;


        photonView.RPC("SetBlinkVisuals", RpcTarget.AllBuffered, true);

        while (durationLeft > 0)
        {
            durationLeft -= Time.deltaTime;
            yield return null;
        }

        photonView.RPC("SetBlinkVisuals", RpcTarget.AllBuffered, false);

        disableInput = false;
        isBlinking = false;
    }

    [PunRPC]
    public void SetBlinkVisuals(bool isBlinkOn)
    {
        if(isBlinkOn)
        {
            Color playerColor = new Color(recolorSprites[0].color.r, recolorSprites[0].color.g, recolorSprites[0].color.b, 0);

            ghostSprites[0].color = new Color(1, 1, 1, 1);
            ghostSprites[1].color = new Color(1, 1, 1, 1);
            nonRecolorSprites[0].color = new Color(1, 1, 1, 0);
            nonRecolorSprites[1].color = new Color(1, 1, 1, 0);
            nonRecolorSprites[2].color = new Color(1, 1, 1, 0);
            recolorSprites[0].color = playerColor;
            recolorSprites[1].color = playerColor;

            blinkFX.Play();
        }
        else
        {
            Color playerColor = new Color(recolorSprites[0].color.r, recolorSprites[0].color.g, recolorSprites[0].color.b, 1);

            ghostSprites[0].color = new Color(1, 1, 1, 0);
            ghostSprites[1].color = new Color(1, 1, 1, 0);
            nonRecolorSprites[0].color = new Color(1, 1, 1, 1);
            nonRecolorSprites[1].color = new Color(1, 1, 1, 1);
            nonRecolorSprites[2].color = new Color(1, 1, 1, 1);
            recolorSprites[0].color = playerColor;
            recolorSprites[1].color = playerColor;

            blinkFX.Stop();
        }
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
        
        var intPosition = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), 0);

        if(timerSprintRunning)
        {

            if (GameManager.localInstance.stoneMask.GetTile(intPosition))
            {
                //Play stone sprint footstep here
                footstepStone.Play();
            }
            else if (GameManager.localInstance.woodMask.GetTile(intPosition))
            {
                //Play wood sprint footsteps here
                footstepWood.Play();
            }
            else
            {
                //Play sprint footsteps here
                footstep.Play();
            }
        }
        else
        {
            if (GameManager.localInstance.stoneMask.GetTile(intPosition))
            {
                AkSoundEngine.SetState("footstep", "normal");
				AkSoundEngine.SetSwitch("surface", "stone", gameObject);
				AkSoundEngine.PostEvent("char_footsteps", gameObject, gameObject);
                Debug.Log("Stone Footstep!");
                footstepStone.Play();
            }
            else if (GameManager.localInstance.woodMask.GetTile(intPosition))
            {
                //Play wood footsteps here
                Debug.Log("Wood Footstep");
                footstepWood.Play();
            }
            else
            {
				AkSoundEngine.SetState("footstep", "normal");
                AkSoundEngine.SetSwitch("surface", "grass", gameObject);
                AkSoundEngine.PostEvent("char_footsteps", gameObject, gameObject);
				
                footstep.Play();
            }

        }
        
        
        

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
            AkSoundEngine.SetState("footstep", "inaudible");
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

        //playerCamera.transform.rotation = Quaternion.AngleAxis(-newAngle, Vector3.forward);

        rotatingBody.transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);


        return temp.normalized;
    }

    private void Move()
    {
        if(!isBlinking)
        {
            rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
            this.gameObject.layer = 0;
        }
        if(isBlinking)
        {
            this.gameObject.layer = 15;
        }
        
        if (Input.GetKey(KeyCode.LeftShift)&& timeSprintRemaining !=0 && timerSprintRunning2 && isTrapped == false)
        {
            AkSoundEngine.SetState("footstep", "speed");
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
    //tutej//
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
        if (stabLock == false)
        {
            AkSoundEngine.PostEvent("char_knife_swinging", gameObject, gameObject);
            StartCoroutine(LockStabbing());
            StartCoroutine(HandleStabAnimation());
            StartCoroutine(WaitAndDeactivateStab());
        }
             
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
        yield return new WaitForSeconds(0.4f);
        photonView.RPC("ActivateStabHitBox", RpcTarget.AllBuffered);
        yield return new WaitForSeconds(0.15f);
        photonView.RPC("DeactivateStab", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void DeactivateStab()
    {
        stabHitBox.SetActive(false);
        destroyWeb = false;
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
        destroyWeb = true;

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
        BlindVignette.SetActive(amount);
        timerBlindedRunning = true;
        
    }
    [PunRPC]
    public void Shine(bool amount)
    {
        AkSoundEngine.PostEvent("sfx_teleport", gameObject, gameObject);
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
        if (cFollow == null)
        {
            cFollow = playerCamera.GetComponent<CameraFollow>();
        }

        cFollow.ShakeCamera(2);

        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        
        torsoAnimator.SetBool("Stab", true);
        yield return new WaitForSeconds(0.08f);
        crossbowFX.Play();
        AkSoundEngine.PostEvent("sfx_crossbow_shoot", gameObject, gameObject);
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
        AkSoundEngine.PostEvent("sfx_obj_spikepit", gameObject, gameObject);
        GameObject obj = PhotonNetwork.Instantiate(spikePitObject.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), rotatingBody.transform.rotation, 0);
        //Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
    }
    public void Tripwire()
    {
        AkSoundEngine.PostEvent("sfx_obj_tripwire", gameObject, gameObject);
        GameObject obj = PhotonNetwork.Instantiate(tripwireObject.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), rotatingBody.transform.rotation, 0);
        //Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
    }
    public void Blindingtrap()
    {
        AkSoundEngine.PostEvent("sfx_obj_throw", gameObject, gameObject);
        GameObject obj = PhotonNetwork.Instantiate(blidingtrapObject.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), rotatingBody.transform.rotation, 0);
        //Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
    }
    public void Bomb()
    {
        AkSoundEngine.PostEvent("sfx_obj_throw", gameObject, gameObject);
        GameObject obj = PhotonNetwork.Instantiate(bombObject.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), rotatingBody.transform.rotation, 0);
        //Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
    }
    public void Geltrap()
    {
        GameObject obj = PhotonNetwork.Instantiate(gelTrapObject.name, new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), rotatingBody.transform.rotation, 0);
        //Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
    }
    public void Detector()
    {
        AkSoundEngine.PostEvent("sfx_obj_detector", gameObject, gameObject);
        GameObject obj = PhotonNetwork.Instantiate("Detector", new Vector2(dropPos.transform.position.x, dropPos.transform.position.y), rotatingBody.transform.rotation, 0);
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
            AkSoundEngine.PostEvent("sfx_obj_visionpotion", gameObject, gameObject);
            playerViewCone.SetActive(false);
            playerViewCone2.SetActive(true);
            visionPotionActive = true;
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
            visionPotionActive = false;

            inventory.currentPassive = null;
            
            canUsePotion = true;
        }
    }

    public void SprintPotion()
    {
        if (photonView.IsMine)
        {
            stimFX.Play();
            AkSoundEngine.PostEvent("sfx_obj_speedpotion", gameObject, gameObject);
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

            inventory.currentPassive = null;

            canUsePotion = true;
        }
    }
    [PunRPC]
    public void CamoSpell(int variant)
    {

        camoFX.Play();
        AkSoundEngine.PostEvent("sfx_obj_camo", gameObject, gameObject);

        camoObjects[variant].SetActive(true);


        recolorSprites[0].color = new Color(recolorSprites[0].color.r, recolorSprites[0].color.g, recolorSprites[0].color.b, 0);
        recolorSprites[1].color = new Color(recolorSprites[0].color.r, recolorSprites[0].color.g, recolorSprites[0].color.b, 0);

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


        recolorSprites[0].color = new Color(recolorSprites[0].color.r, recolorSprites[0].color.g, recolorSprites[0].color.b, 1);
        recolorSprites[1].color = new Color(recolorSprites[0].color.r, recolorSprites[0].color.g, recolorSprites[0].color.b, 1);

        foreach (SpriteRenderer renderer in nonRecolorSprites)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
        }


        foreach(GameObject camo in camoObjects)
        {
            camo.SetActive(false);
        }

        inventory.currentPassive = null;
        canUsePotion = true;
        camoNum = 0;
  
    }
    
    public void ColourSet()
    {
        AkSoundEngine.PostEvent("sfx_change_color", gameObject, gameObject);
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet.r, colorToSet.g, colorToSet.b);
        //SetColor(colorToSet.r, colorToSet.g, colorToSet.b);
    }
    public void ColourSet1()
    {
        AkSoundEngine.PostEvent("sfx_change_color", gameObject, gameObject);
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet1.r, colorToSet1.g, colorToSet1.b);
        //SetColor(colorToSet1.r, colorToSet1.g, colorToSet1.b);
    }
    public void ColourSet2()
    {
        AkSoundEngine.PostEvent("sfx_change_color", gameObject, gameObject);
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet2.r, colorToSet2.g, colorToSet2.b);
        //SetColor(colorToSet2.r, colorToSet2.g, colorToSet2.b);
    }
    public void ColourSet3()
    {
        AkSoundEngine.PostEvent("sfx_change_color", gameObject, gameObject);
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet3.r, colorToSet3.g, colorToSet3.b);
        //SetColor(colorToSet3.r, colorToSet3.g, colorToSet3.b);
    }
    public void ColourSet4()
    {
        AkSoundEngine.PostEvent("sfx_change_color", gameObject, gameObject);
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet4.r, colorToSet4.g, colorToSet4.b);
        //SetColor(colorToSet4.r, colorToSet4.g, colorToSet4.b);
    }
    public void ColourSet5()
    {
        AkSoundEngine.PostEvent("sfx_change_color", gameObject, gameObject);
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet5.r, colorToSet5.g, colorToSet5.b);
        //SetColor(colorToSet5.r, colorToSet5.g, colorToSet5.b);
    }
    public void ColourSet6()
    {
        AkSoundEngine.PostEvent("sfx_change_color", gameObject, gameObject);
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet6.r, colorToSet6.g, colorToSet6.b);
        //SetColor(colorToSet6.r, colorToSet6.g, colorToSet6.b);
    }
    public void ColourSet7()
    {
        AkSoundEngine.PostEvent("sfx_change_color", gameObject, gameObject);
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet7.r, colorToSet7.g, colorToSet7.b);
        //SetColor(colorToSet7.r, colorToSet7.g, colorToSet7.b);
    }
    public void ChangeTagGhost()
    {
        this.gameObject.tag = "Ghost";
    }
    public void ChangeTagPlayer()
    {
        this.gameObject.tag = "Player";
    }



}
