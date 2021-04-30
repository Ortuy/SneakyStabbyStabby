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
    public GameObject playerCamera, playerViewCone, playerViewCone2, rotatingBody,pointLight2d,gel, legs, HUD, Shop,ColorSelect, Buyblind;
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
    public Text goldText;
    //public int blidingtrapprice = 10;
    //public int blinkPrice = 10;
    //public int bombPrice = 10;
    //public int camoPrice = 10;
    //public int crossbowPrice = 10;
    //public int detectorPrice = 10;
    //public int silentPotionPrice = 10;
    //public int gelTrapPrice = 10;
    //public int spikePitPrice = 10;
    //public int tripWireTrapPrice = 10;
    //public int seePotionPrice = 10;
    //public int speedPotionPrice = 10;



    private int selectedTrapID;
    

    public float moveSpeed, shootSpeed;

    public int playerID;

    public SpriteRenderer[] recolorSprites;
    public SpriteRenderer[] nonRecolorSprites;
    public SpriteRenderer[] ghostSprites;

    [SerializeField] private Animator legsAnimator, torsoAnimator;

    public Inventory inventory;
    public Text stabCooldownText, potionCooldownText;

    public ParticleSystem footstep, footstepStone, camoFX, stimFX, visionFX, blinkFX, crossbowFX;
    public SpriteRenderer glowingFootstep;

    [SerializeField] private Slider staminaBar;
    private bool sprintPotionActive;

    [SerializeField] public GameObject[] camoObjects;

    public bool waitingForTrap, settingTrap, isByDetector;
    [SerializeField] private GameObject trapMarker;
    [SerializeField] private Sprite[] trapImages;

    //public int shopSlotRange = 0;
    //public int shopSlotRange1 = 0;
    //public int shopSlotRange2 = 0;
    //public GameObject shopEnter;
    //public GameObject buyBlinding, buyBlink, buyBomb, buyCamo, buyCrossbow, buyDetector, buySilentPotion, buyGelTrap, buySpikePit, buyTripWire, buySeePotion, buySpeedPotion;
    //public GameObject buyBlinding1, buyBlink1, buyBomb1, buyCamo1, buyCrossbow1, buyDetector1, buySilentPotion1, buyGelTrap1, buySpikePit1, buyTripWire1, buySeePotion1, buySpeedPotion1;
    //public GameObject buyBlinding2, buyBlink2, buyBomb2, buyCamo2, buyCrossbow2, buyDetector2, buySilentPotion2, buyGelTrap2, buySpikePit2, buyTripWire2, buySeePotion2, buySpeedPotion2;

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
            ColorSelect.SetActive(false);
            mapIcon.SetActive(true);

            //Shop.SetActive(false);

            //buyBlinding.SetActive(false);
            //buyBlink.SetActive(false);
            //buyBomb.SetActive(false);
            //buyCamo.SetActive(false);
            //buyCrossbow.SetActive(false);
            //buyDetector.SetActive(false);
            //buySilentPotion.SetActive(false);
            //buyGelTrap.SetActive(false);
            //buySpikePit.SetActive(false);
            //buyTripWire.SetActive(false);
            //buySeePotion.SetActive(false);
            //buySpeedPotion.SetActive(false);

            //buyBlinding1.SetActive(false);
            //buyBlink1.SetActive(false);
            //buyBomb1.SetActive(false);
            //buyCamo1.SetActive(false);
            //buyCrossbow1.SetActive(false);
            //buyDetector1.SetActive(false);
            //buySilentPotion1.SetActive(false);
            //buyGelTrap1.SetActive(false);
            //buySpikePit1.SetActive(false);
            //buyTripWire1.SetActive(false);
            //buySeePotion1.SetActive(false);
            //buySpeedPotion1.SetActive(false);

            //buyBlinding2.SetActive(false);
            //buyBlink2.SetActive(false);
            //buyBomb2.SetActive(false);
            //buyCamo2.SetActive(false);
            //buyCrossbow2.SetActive(false);
            //buyDetector2.SetActive(false);
            //buySilentPotion2.SetActive(false);
            //buyGelTrap2.SetActive(false);
            //buySpikePit2.SetActive(false);
            //buyTripWire2.SetActive(false);
            //buySeePotion2.SetActive(false);
            //buySpeedPotion2.SetActive(false);

            //shopSlotRange = Random.Range(0, 12);
            //shopSlotRange1 = Random.Range(0, 12);
            //shopSlotRange2 = Random.Range(0, 12);
            if (GameManager.localInstance.playerAmount == 0)
            {
                //GameManager.instance.SpawnDecor();
            }

            photonView.RPC("RegisterPlayer", RpcTarget.AllBuffered);
            
        }

        //legsAnimator = GetComponent<Animator>();

        moveSpeed = 5;
        stabLock = true;


        //playerCamera.transform.SetParent(null);
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
        //if(shopSlotRange == 1)
        //{
        //    buyBlinding.SetActive(true);

        //}
        //if (shopSlotRange == 2)
        //{
        //    buyBlink.SetActive(true);


        //}
        //if (shopSlotRange == 3)
        //{
        //    buyBomb.SetActive(true);

        //}
        //if (shopSlotRange == 4)
        //{
        //    buyCamo.SetActive(true);

        //}
        //if (shopSlotRange == 5)
        //{
        //    buyCrossbow.SetActive(true);

        //}
        //if (shopSlotRange == 6)
        //{
        //    buyDetector.SetActive(true);

        //}
        //if (shopSlotRange == 7)
        //{
        //    buySilentPotion.SetActive(true);

        //}
        //if (shopSlotRange == 8)
        //{
        //    buyGelTrap.SetActive(true);

        //}
        //if (shopSlotRange == 9)
        //{
        //    buySpikePit.SetActive(true);

        //}
        //if (shopSlotRange == 10)
        //{
        //    buyTripWire.SetActive(true);

        //}
        //if (shopSlotRange == 11)
        //{
        //    buySeePotion.SetActive(true);

        //}
        //if (shopSlotRange == 12)
        //{
        //    buySpeedPotion.SetActive(true);
        //}
        //if (shopSlotRange1 == 1)
        //{
        //    buyBlinding1.SetActive(true);

        //}
        //if (shopSlotRange1 == 2)
        //{
        //    buyBlink1.SetActive(true);


        //}
        //if (shopSlotRange1 == 3)
        //{
        //    buyBomb1.SetActive(true);

        //}
        //if (shopSlotRange1 == 4)
        //{
        //    buyCamo1.SetActive(true);

        //}
        //if (shopSlotRange1 == 5)
        //{
        //    buyCrossbow1.SetActive(true);

        //}
        //if (shopSlotRange1 == 6)
        //{
        //    buyDetector1.SetActive(true);

        //}
        //if (shopSlotRange1 == 7)
        //{
        //    buySilentPotion1.SetActive(true);

        //}
        //if (shopSlotRange1 == 8)
        //{
        //    buyGelTrap1.SetActive(true);

        //}
        //if (shopSlotRange1 == 9)
        //{
        //    buySpikePit1.SetActive(true);

        //}
        //if (shopSlotRange1 == 10)
        //{
        //    buyTripWire1.SetActive(true);

        //}
        //if (shopSlotRange1 == 11)
        //{
        //    buySeePotion1.SetActive(true);

        //}
        //if (shopSlotRange1 == 12)
        //{
        //    buySpeedPotion1.SetActive(true);
        //}
        //if (shopSlotRange2 == 1)
        //{
        //    buyBlinding2.SetActive(true);

        //}
        //if (shopSlotRange2 == 2)
        //{
        //    buyBlink2.SetActive(true);


        //}
        //if (shopSlotRange2 == 3)
        //{
        //    buyBomb2.SetActive(true);

        //}
        //if (shopSlotRange2 == 4)
        //{
        //    buyCamo2.SetActive(true);

        //}
        //if (shopSlotRange2 == 5)
        //{
        //    buyCrossbow2.SetActive(true);

        //}
        //if (shopSlotRange2 == 6)
        //{
        //    buyDetector2.SetActive(true);

        //}
        //if (shopSlotRange2 == 7)
        //{
        //    buySilentPotion2.SetActive(true);

        //}
        //if (shopSlotRange2 == 8)
        //{
        //    buyGelTrap2.SetActive(true);

        //}
        //if (shopSlotRange2 == 9)
        //{
        //    buySpikePit2.SetActive(true);

        //}
        //if (shopSlotRange2 == 10)
        //{
        //    buyTripWire2.SetActive(true);

        //}
        //if (shopSlotRange2 == 11)
        //{
        //    buySeePotion2.SetActive(true);

        //}
        //if (shopSlotRange2== 12)
        //{
        //    buySpeedPotion2.SetActive(true);
        //}
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameManager.localInstance.ToggleMap();
            if(GameManager.localInstance.mapPanel.activeInHierarchy)
            {
                stabLock = true;
                torsoAnimator.SetBool("Map", true);
            }
            else
            {
                stabLock = false;
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
        if(GameManager.localInstance.stoneMask.GetTile(intPosition))
        {
            footstepStone.Play();
        }
        else
        {
            footstep.Play();
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
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        
        torsoAnimator.SetBool("Stab", true);
        yield return new WaitForSeconds(0.08f);
        crossbowFX.Play();
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
    public void Detector()
    {
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

        //foreach(SpriteRenderer renderer in recolorSprites)
        //{
        //    renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
        //}
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

        //foreach (SpriteRenderer renderer in recolorSprites)
        //{
        //    renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1);
        //}
        recolorSprites[0].color = new Color(recolorSprites[0].color.r, recolorSprites[0].color.g, recolorSprites[0].color.b, 1);
        recolorSprites[1].color = new Color(recolorSprites[0].color.r, recolorSprites[0].color.g, recolorSprites[0].color.b, 1);

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
    
    public void ColourSet()
    {
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet.r, colorToSet.g, colorToSet.b);
        //SetColor(colorToSet.r, colorToSet.g, colorToSet.b);
    }
    public void ColourSet1()
    {
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet1.r, colorToSet1.g, colorToSet1.b);
        //SetColor(colorToSet1.r, colorToSet1.g, colorToSet1.b);
    }
    public void ColourSet2()
    {
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet2.r, colorToSet2.g, colorToSet2.b);
        //SetColor(colorToSet2.r, colorToSet2.g, colorToSet2.b);
    }
    public void ColourSet3()
    {
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet3.r, colorToSet3.g, colorToSet3.b);
        //SetColor(colorToSet3.r, colorToSet3.g, colorToSet3.b);
    }
    public void ColourSet4()
    {
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet4.r, colorToSet4.g, colorToSet4.b);
        //SetColor(colorToSet4.r, colorToSet4.g, colorToSet4.b);
    }
    public void ColourSet5()
    {
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet5.r, colorToSet5.g, colorToSet5.b);
        //SetColor(colorToSet5.r, colorToSet5.g, colorToSet5.b);
    }
    public void ColourSet6()
    {
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet6.r, colorToSet6.g, colorToSet6.b);
        //SetColor(colorToSet6.r, colorToSet6.g, colorToSet6.b);
    }
    public void ColourSet7()
    {
        photonView.RPC("SetColor", RpcTarget.AllBuffered, colorToSet7.r, colorToSet7.g, colorToSet7.b);
        //SetColor(colorToSet7.r, colorToSet7.g, colorToSet7.b);
    }
    //public void BuyBlinding()
    //{
    //    if (gold >= blidingtrapprice)
    //    {
    //        PhotonNetwork.Instantiate(pickup1.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= blidingtrapprice;
    //        buyBlinding.SetActive(false);
    //        shopSlotRange = Random.Range(0, 12);
    //    }

    //}
    //public void BuyBlink()
    //{
    //    if (gold >= blinkPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup2.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= blinkPrice;
    //        buyBlink.SetActive(false);
    //        shopSlotRange = Random.Range(0, 12);
    //    }

    //}
    //public void BuyBomb()
    //{
    //    if (gold >= bombPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup3.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= bombPrice;
    //        buyBomb.SetActive(false);
    //        shopSlotRange = Random.Range(0, 12);
    //    }

    //}
    //public void BuyCamo()
    //{
    //    if (gold >= camoPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup4.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= camoPrice;
    //        buyCamo.SetActive(false);
    //        shopSlotRange = Random.Range(0, 12);
    //    }

    //}
    //public void BuyCrossbow()
    //{
    //    if (gold >= crossbowPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup5.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= crossbowPrice;
    //        buyCrossbow.SetActive(false);
    //        shopSlotRange = Random.Range(0, 12);
    //    }

    //}
    //public void BuyDetector()
    //{
    //    if (gold >= detectorPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup6.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= detectorPrice;
    //        buyDetector.SetActive(false);
    //        shopSlotRange = Random.Range(0, 12);
    //    }

    //}
    //public void BuySilentPotion()
    //{
    //    if (gold >= silentPotionPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup7.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= silentPotionPrice;
    //        buySilentPotion.SetActive(false);
    //        shopSlotRange = Random.Range(0, 12);
    //    }

    //}
    //public void BuyGelTrap()
    //{
    //    if (gold >= gelTrapPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup8.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= gelTrapPrice;
    //        buyGelTrap.SetActive(false);
    //        shopSlotRange = Random.Range(0, 12);
    //    }

    //}
    //public void BuySpikePit()
    //{
    //    if (gold >= spikePitPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup9.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= spikePitPrice;
    //        buySpikePit.SetActive(false);
    //        shopSlotRange = Random.Range(0, 12);
    //    }

    //}
    //public void BuyTripWire()
    //{
    //    if (gold >= tripWireTrapPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup10.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= tripWireTrapPrice;
    //        buyTripWire.SetActive(false);
    //        shopSlotRange = Random.Range(0, 12);
    //    }

    //}
    //public void BuySeePotion()
    //{
    //    if (gold >= seePotionPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup11.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= seePotionPrice;
    //        buySeePotion.SetActive(false);
    //        shopSlotRange = Random.Range(0, 12);
    //    }

    //}
    //public void BuySpeedPotion()
    //{
    //    if (gold >= speedPotionPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup12.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= speedPotionPrice;
    //        buySpeedPotion.SetActive(false);
    //        shopSlotRange = Random.Range(0, 12);
    //    }

    //}
    //public void BuyBlinding1()
    //{
    //    if (gold >= blidingtrapprice)
    //    {
    //        PhotonNetwork.Instantiate(pickup1.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= blidingtrapprice;
    //        buyBlinding1.SetActive(false);
    //        shopSlotRange1 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyBlink1()
    //{
    //    if (gold >= blinkPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup2.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= blinkPrice;
    //        buyBlink1.SetActive(false);
    //        shopSlotRange1 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyBomb1()
    //{
    //    if (gold >= bombPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup3.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= bombPrice;
    //        buyBomb1.SetActive(false);
    //        shopSlotRange1 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyCamo1()
    //{
    //    if (gold >= camoPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup4.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= camoPrice;
    //        buyCamo1.SetActive(false);
    //        shopSlotRange1 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyCrossbow1()
    //{
    //    if (gold >= crossbowPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup5.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= crossbowPrice;
    //        buyCrossbow1.SetActive(false);
    //        shopSlotRange1 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyDetector1()
    //{
    //    if (gold >= detectorPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup6.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= detectorPrice;
    //        buyDetector1.SetActive(false);
    //        shopSlotRange1 = Random.Range(0, 12);
    //    }

    //}
    //public void BuySilentPotion1()
    //{
    //    if (gold >= silentPotionPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup7.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= silentPotionPrice;
    //        buySilentPotion1.SetActive(false);
    //        shopSlotRange1 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyGelTrap1()
    //{
    //    if (gold >= gelTrapPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup8.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= gelTrapPrice;
    //        buyGelTrap1.SetActive(false);
    //        shopSlotRange1 = Random.Range(0, 12);
    //    }

    //}
    //public void BuySpikePit1()
    //{
    //    if (gold >= spikePitPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup9.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= spikePitPrice;
    //        buySpikePit1.SetActive(false);
    //        shopSlotRange1 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyTripWire1()
    //{
    //    if (gold >= tripWireTrapPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup10.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= tripWireTrapPrice;
    //        buyTripWire1.SetActive(false);
    //        shopSlotRange1 = Random.Range(0, 12);
    //    }

    //}
    //public void BuySeePotion1()
    //{
    //    if (gold >= seePotionPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup11.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= seePotionPrice;
    //        buySeePotion1.SetActive(false);
    //        shopSlotRange1 = Random.Range(0, 12);
    //    }

    //}
    //public void BuySpeedPotion1()
    //{
    //    if (gold >= speedPotionPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup12.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= speedPotionPrice;
    //        buySpeedPotion1.SetActive(false);
    //        shopSlotRange1 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyBlinding2()
    //{
    //    if (gold >= blidingtrapprice)
    //    {
    //        PhotonNetwork.Instantiate(pickup1.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= blidingtrapprice;
    //        buyBlinding2.SetActive(false);
    //        shopSlotRange2 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyBlink2()
    //{
    //    if (gold >= blinkPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup2.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= blinkPrice;
    //        buyBlink2.SetActive(false);
    //        shopSlotRange2 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyBomb2()
    //{
    //    if (gold >= bombPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup3.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= bombPrice;
    //        buyBomb2.SetActive(false);
    //        shopSlotRange2 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyCamo2()
    //{
    //    if (gold >= camoPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup4.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= camoPrice;
    //        buyCamo2.SetActive(false);
    //        shopSlotRange2 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyCrossbow2()
    //{
    //    if (gold >= crossbowPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup5.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= crossbowPrice;
    //        buyCrossbow2.SetActive(false);
    //        shopSlotRange2 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyDetector2()
    //{
    //    if (gold >= detectorPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup6.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= detectorPrice;
    //        buyDetector2.SetActive(false);
    //        shopSlotRange2 = Random.Range(0, 12);
    //    }

    //}
    //public void BuySilentPotion2()
    //{
    //    if (gold >= silentPotionPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup7.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= silentPotionPrice;
    //        buySilentPotion2.SetActive(false);
    //        shopSlotRange2 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyGelTrap2()
    //{
    //    if (gold >= gelTrapPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup8.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= gelTrapPrice;
    //        buyGelTrap2.SetActive(false);
    //        shopSlotRange2 = Random.Range(0, 12);
    //    }

    //}
    //public void BuySpikePit2()
    //{
    //    if (gold >= spikePitPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup9.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= spikePitPrice;
    //        buySpikePit2.SetActive(false);
    //        shopSlotRange2 = Random.Range(0, 12);
    //    }

    //}
    //public void BuyTripWire2()
    //{
    //    if (gold >= tripWireTrapPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup10.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= tripWireTrapPrice;
    //        buyTripWire2.SetActive(false);
    //        shopSlotRange2 = Random.Range(0, 12);
    //    }

    //}
    //public void BuySeePotion2()
    //{
    //    if (gold >= seePotionPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup11.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= seePotionPrice;
    //        buySeePotion2.SetActive(false);
    //        shopSlotRange2 = Random.Range(0, 12);
    //    }

    //}
    //public void BuySpeedPotion2()
    //{
    //    if (gold >= speedPotionPrice)
    //    {
    //        PhotonNetwork.Instantiate(pickup12.name, new Vector2(paintPos.transform.position.x, paintPos.transform.position.y), Quaternion.identity, 0);
    //        gold -= speedPotionPrice;
    //        buySpeedPotion2.SetActive(false);
    //        shopSlotRange2 = Random.Range(0, 12);
    //    }

    //}



}
