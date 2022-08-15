using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ProgramManager : MonoBehaviour
{
    [Header("Put your planeMarker here")]
    [SerializeField] private GameObject PlaneMarkerPrefab;

    private ARRaycastManager ARRaycastManagerScript;
    private Vector2 TouchPosition;
    public GameObject ObjectToSpawn;

    public bool ChooseObject = false;

    [Header("Put ScrolView here")]
    public GameObject ScrolView;

    [SerializeField] private Camera ARCamera;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject SelectedObject;

    public bool Rotation;
    private Quaternion YRotation;
    public bool Recharging;
    [SerializeField] GameObject MaketShell;

    void Start()
    {
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();
        PlaneMarkerPrefab.SetActive(false);
        ScrolView.SetActive(false);

    }

    void Update()
    {
        if (ChooseObject)
        {
            ShowMarkerAndSetObject();
        }

        MoveObjectAndRotation();
        //SpawnAsATouch();
        if (Recharging)
        {
            MaketShell.SetActive(false);
        }
        else
        {
            MaketShell.SetActive(true);
        }

    }
    void ShowMarkerAndSetObject()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        // Показываем маркер
        if (hits.Count > 0)
        {
            PlaneMarkerPrefab.transform.position = hits[0].pose.position;
            PlaneMarkerPrefab.SetActive(true);
        }
        // Устанавливаем объект
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Instantiate(ObjectToSpawn, hits[0].pose.position, ObjectToSpawn.transform.rotation);
            MaketShell = GameObject.Find("MaketShell");
            ChooseObject = false;
            PlaneMarkerPrefab.SetActive(false);
        }
    }
    void SpawnAsATouch()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            TouchPosition = touch.position;
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            ARRaycastManagerScript.Raycast(TouchPosition, hits, TrackableType.Planes);

            Instantiate(ObjectToSpawn, hits[0].pose.position, ObjectToSpawn.transform.rotation);
        }
    }
    void MoveObjectAndRotation()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            TouchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = ARCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.collider.CompareTag("UnSelected"))
                    {
                        hitObject.collider.gameObject.tag = "Selected";
                    }
                }
            }

            SelectedObject = GameObject.FindWithTag("Selected");

            if (touch.phase == TouchPhase.Moved && Input.touchCount == 1)
            {
                if (Rotation)
                {
                    YRotation = Quaternion.Euler(0f, -touch.deltaPosition.x * 0.1f, 0f);
                    SelectedObject.transform.rotation = YRotation * SelectedObject.transform.rotation;
                }
                else
                {
                    ARRaycastManagerScript.Raycast(TouchPosition, hits, TrackableType.Planes);
                    SelectedObject.transform.position = hits[0].pose.position;
                }
            }
            
            if(Input.touchCount == 2)
            {
                Touch touch1 = Input.touches[0];
                Touch touch2 = Input.touches[1];

                if(touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
                {
                    float DistanceBetweenTouches = Vector2.Distance(touch1.position, touch2.position);
                    float prevDistanceBeteenTouches = Vector2.Distance(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
                    float Delta = DistanceBetweenTouches - prevDistanceBeteenTouches;
                    if (Mathf.Abs(Delta) > 0)
                    {
                        Delta *= 0.1f;
                    }
                    else
                    {
                        DistanceBetweenTouches = Delta = 0;
                    }
                    YRotation = Quaternion.Euler(0f, -touch1.deltaPosition.x * Delta, 0f);
                    SelectedObject.transform.rotation = YRotation * SelectedObject.transform.rotation;
                }
            }

            if(touch.phase == TouchPhase.Ended)
            {
                if (SelectedObject.CompareTag("Selected"))
                {
                    SelectedObject.tag = "UnSelected";
                }
            }
        }
    }
}
