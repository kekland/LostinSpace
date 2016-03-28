using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour {
    Vector3 lastClickedPlace;
    NavMeshAgent meshAgent;
    public Animator anim;
    public PlayerStats stats;
    public bool CanMove = true;

    [Header("Camera targets : ")]
    public Transform cameraAction;
    void Awake()
    {
        Application.targetFrameRate = 60;
        currentTileMov = Instantiate(tileMov);
        meshAgent = GetComponent<NavMeshAgent>();
        tileMovText = currentTileMov.transform.FindChild("Canvas").FindChild("Text").GetComponent<Text>();
    }

    public GameObject tileMov;
    public GameObject currentTileMov;
    public Text tileMovText;
    public bool doingAction;

    void FixedUpdate()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && !doingAction)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.tag == "Walkable" && stats.Energy >= Mathf.RoundToInt(Vector3.Distance(hit.point, transform.position) / 4))
                {
                    lastClickedPlace = hit.point;
                    lastClickedPlace = new Vector3(Mathf.RoundToInt(lastClickedPlace.x), 0.05f, Mathf.RoundToInt(lastClickedPlace.z));
                    currentTileMov.transform.position = lastClickedPlace;
                    string text = Mathf.RoundToInt(Vector3.Distance(lastClickedPlace, transform.position) / 4).ToString();
                    tileMovText.text = text;
                }
            }
            if (Input.GetMouseButtonDown(0) && CanMove)
            {
                anim.SetBool("isRunning", true);
                Vector3 startPosition = transform.position;
                meshAgent.destination = lastClickedPlace;
                int distance = Mathf.RoundToInt(Vector3.Distance(lastClickedPlace, startPosition) / 4);
                stats.Energy -= distance;
            }
        }
        if(doingAction)
        {
            currentTileMov.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            currentTileMov.GetComponent<MeshRenderer>().enabled = true;
        }
        anim.SetBool("isRunning", (meshAgent.remainingDistance > meshAgent.radius));
    }
    void Update()
    {
        if (!doingAction)
        {
            CameraMove(true);
        }
        else
        {
            CameraMove(false);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            cameraMoveState -= 1;
            if(cameraMoveState < 0)
            {
                cameraMoveState = 3;
            }
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            cameraMoveState += 1;
            if(cameraMoveState > 3)
            {
                cameraMoveState = 0;
            }
        }
    }
    int cameraMoveState = 0;

    public bool isMoving
    {
        get
        {
            return (meshAgent.remainingDistance > meshAgent.radius);
        }
    }
    void CameraMove(bool defaultState)
    {
        if (defaultState)
        {
            if (cameraMoveState == 0)
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(transform.position.x + 6f, 14f, transform.position.z - 6f), 3f * Time.deltaTime);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(new Vector3(45f, 315f, 0f)), Time.deltaTime * 5f);
            }
            else if (cameraMoveState == 1)
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(transform.position.x + 6f, 14f, transform.position.z + 6f), 3f * Time.deltaTime);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(new Vector3(45f, 225f, 0f)), Time.deltaTime * 5f);
            }
            else if (cameraMoveState == 2)
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(transform.position.x - 6f, 14f, transform.position.z + 6f), 3f * Time.deltaTime);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(new Vector3(45f, 135f, 0f)), Time.deltaTime * 5f);
            }
            else if (cameraMoveState == 3)
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(transform.position.x - 6f, 14f, transform.position.z - 6f), 3f * Time.deltaTime);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(new Vector3(45f, 45f, 0f)), Time.deltaTime * 5f);
            }
        }
        else
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraAction.position, 4f * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, cameraAction.rotation, Time.deltaTime * 5f);
        }
    }
}
