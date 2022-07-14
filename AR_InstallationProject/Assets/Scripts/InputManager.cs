using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.XR.ARSubsystems;
public class InputManager : ARBaseGestureInteractable
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject crosshair;

    List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private Touch touch;

    private Pose pose;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (gesture.targetObject == null)
            return true;
        return false;
    }

    protected override void OnEndManipulation(TapGesture gesture)
    {
        if (gesture.isCanceled)
            return;

        if(gesture.targetObject != null || IsPointerOverUI(gesture))
        {
            return;
        }

        if (GestureTransformationUtility.Raycast(gesture.startPosition, _hits, TrackableType.PlaneWithinPolygon))
        {
            GameObject placeObj = Instantiate(DataHandler.Instance.GetBoiler(), pose.position, pose.rotation);

            var anchorObject = new GameObject("PlacementAnchor");
            anchorObject.transform.position = pose.position;
            anchorObject.transform.rotation = pose.rotation;
            placeObj.transform.parent = anchorObject.transform;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CrHCalculate();
    }

    bool IsPointerOverUI(TapGesture touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.startPosition.x, touch.startPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    void CrHCalculate()
    {
        Vector3 origin = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0.5f));
        if (GestureTransformationUtility.Raycast(origin, _hits, TrackableType.PlaneWithinPolygon))
        {
            pose = _hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90, 0, 0);
        }
    }
}
