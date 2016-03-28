using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerAction : MonoBehaviour {

    PlayerAbilities abi;
    PlayerMovement mov;
    Camera mainCamera;
    public GameObject grenadeArea;
    void Awake()
    {
        abi = GetComponent<PlayerAbilities>();
        mov = GetComponent<PlayerMovement>();
        mainCamera = Camera.main;
        anim = transform.FindChild("Graphics").GetComponent<Animator>();
    }
	public void DoAction(int action)
    {
        switch(action)
        {
            case 0: Shoot(); break;
            case 1: HEGrenade(); break;
            case 2: SmokeGrenade(); break;
            case 3: Overwatch(); break;
            case 4: Reload(); break;
            case 5: Assault_DoubleShoot(); break;
            case 6: Assault_Flashbang(); break;
            case 7: Assault_Healing(); break;
            case 8: Assault_Suppression(); break;
            case 9: Scout_2in1(); break;
            case 10: Scout_FreeReload(); break;
            case 11: Scout_Invisibility(); break;
            case 12: Scout_ShowLoot(); break;
            case 13: Sniper_DefPosition(); break;
            case 14: Sniper_Series(); break;
            case 15: Sniper_UpgOverwatch(); break;
        }
    }

    void Update()
    {
        if(isShooting)
        {
            HandleShooting(1);
        }
        else if(isThrowingHE)
        {
            HandleGrenade(0);
        }
        else if(isThrowingSmoke)
        {
            HandleGrenade(1);
        }
        else if(isThrowingFB)
        {
            HandleGrenade(2);
        }
        else if(isOnOverwatch)
        {
            HandleOverwatch(0);
        }
        else if(isOnUpgOverwatch)
        {
            HandleOverwatch(1);
        }
        else if(isDoubleShooting)
        {
            HandleShooting(2);
        }
        else if(isUsingSuppresion)
        {
            HandleSuppression();
        }
        else if(isShootingTwo)
        {
            HandleTwoInOne();
        }
        else if(isShootingWithSeries)
        {
            HandleSeries();
        }
        else if(isOnDefensivePosition)
        {
            HandleDefPosition();
        }
    }

    void HandleShooting(int count) //How much times to shoot
    {
        mov.doingAction = true;
        int PickedSoliderId = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PickedSoliderId--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PickedSoliderId++;
        }
        PickedSoliderId = Mathf.Clamp(PickedSoliderId, 0, targetsToShoot.Count - 1);
        mov.CanMove = false;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int i = 0; i < count; i++)
            {
                transform.LookAt(targetsToShoot[PickedSoliderId].transform.position);
                StartCoroutine(ShootEnum());
                Debug.Log("Shooted to : " + targetsToShoot[PickedSoliderId].name);
            }
        }
    }
    Animator anim;
    public ParticleSystem gunSmoke;
    public ParticleSystem[] gunExplosion;
    public Light gunLight;
    IEnumerator ShootEnum()
    {
        yield return new WaitForSeconds(1.2f);
        gunSmoke.transform.parent.GetComponent<AudioSource>().Play();
        gunLight.enabled = true;
        yield return new WaitForSeconds(1f);
        anim.SetBool("Shooting", true);
        foreach(ParticleSystem ps in gunExplosion)
        {
            ps.Play();
        }
        yield return new WaitForSeconds(0.5f);
        gunSmoke.Play();
        gunLight.enabled = false;
        anim.SetBool("Shooting", false);
        yield return new WaitForSeconds(1f);
        mov.doingAction = false;
        mov.CanMove = true;
        isShooting = false;
        isShootingTwo = false;
    }

    public GameObject GrArea;
    void HandleGrenade(int type) // 0 - HE, 1 - Smoke, 2 - FB
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            Debug.Log(hit.point);
            GrArea.transform.position = hit.point;
        }
        mov.CanMove = false;
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(GrArea);
            mov.CanMove = true;
            isThrowingFB = false;
            isThrowingHE = false;
            isThrowingSmoke = false;
        }
    }

    public bool doingAction;
    void HandleOverwatch(int type) //0 - Standard, 1 - Upgraded (Sniper)
    {
        GetVisibleSoliders();
        Debug.Log("Standing on overwatch!");
        
        if(targetsToShoot.Count > 0)
        {
            if (isOnOverwatch)
            {
                HandleShooting(1);
                isOnOverwatch = false;
            }
            else if (isOnUpgOverwatch)
            {
                HandleShooting(2);
                isOnUpgOverwatch = false;
            }
            Debug.Log("Overwatch breaked!");
        }
    }

    void HandleSuppression()
    {

    }

    void HandleTwoInOne()
    {

    }

    void HandleInvisibility()
    {

    }

    void HandleDefPosition()
    {

    }

    void HandleSeries()
    {

    }

    void GetVisibleSoliders()
    {
        targetsToShoot.Clear();
        GameObject[] targets = GameObject.FindGameObjectsWithTag("RemotePlayer");
        foreach (GameObject target in targets)
        {
            if (target.GetComponent<MeshRenderer>().isVisible)
            {
                targetsToShoot.Add(target);
            }
        }
    }
    public List<GameObject> targetsToShoot = new List<GameObject>();
    public bool isShooting;
    void Shoot() //0
    {
        GetVisibleSoliders();
        isShooting = true;
        doingAction = true;
    }

    public bool isThrowingHE;
    void HEGrenade() //1
    {
        GrArea = Instantiate(grenadeArea);
        isThrowingHE = true;
        doingAction = true;
    }

    public bool isThrowingSmoke;
    void SmokeGrenade() //2
    {
        GrArea = Instantiate(grenadeArea);
        isThrowingSmoke = true;
        doingAction = true;
    }

    public bool isOnOverwatch;
    void Overwatch() //3
    {
        isOnOverwatch = true;
    }

    void Reload() //4
    {
        doingAction = true;
    }

    public bool isDoubleShooting;
    void Assault_DoubleShoot() //5
    {
        GetVisibleSoliders();
        isDoubleShooting = true;
        doingAction = true;
    }

    public bool isThrowingFB;
    void Assault_Flashbang() //6
    {
        GrArea = Instantiate(grenadeArea);
        isThrowingFB = true;
        doingAction = true;
    }

    void Assault_Healing() //7
    {
        doingAction = true;
    }

    public bool isUsingSuppresion;
    void Assault_Suppression() //8
    {
        GetVisibleSoliders();
        isUsingSuppresion = true;
        doingAction = true;
    }

    public bool isShootingTwo;
    void Scout_2in1() //9
    {
        GetVisibleSoliders();
        isShootingTwo = true;
        doingAction = true;
    }

    void Scout_FreeReload() //10
    {
        doingAction = true;
    }

    public bool isInvisible;
    void Scout_Invisibility() //11
    {
        isInvisible = true;
    }

    void Scout_ShowLoot() //12
    {
        doingAction = true;
    }

    public bool isOnDefensivePosition;
    void Sniper_DefPosition() //13
    {
        isOnDefensivePosition = true;
        doingAction = true;
    }

    public bool isShootingWithSeries;
    void Sniper_Series() //14
    {
        GetVisibleSoliders();
        isShootingWithSeries = true;
        doingAction = true;
    }

    public bool isOnUpgOverwatch;
    void Sniper_UpgOverwatch() //15
    {
        isOnUpgOverwatch = true;
    }
}
