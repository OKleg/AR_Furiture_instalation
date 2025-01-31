

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/**
 * Spawns a <see cref="CarBehaviour"/> when a plane is tapped.
 */
public class CarManager : MonoBehaviour
{
    private Button btnDelete;
    //[SerializeField] public GameObject CarPrefab;
    [SerializeField] public ReticleBehaviour Reticle;
    [SerializeField] public DrivingSurfaceManager DrivingSurfaceManager;

    public CarBehaviour Car;


    private Touch touch;

    private void Update()
    {
#if UNITY_EDITOR
        return;
#endif
        touch = Input.GetTouch(0);
        if (Input.touchCount <= 0 && touch.phase != TouchPhase.Began) 
            return;

        if (IsPointerOverUI(touch)) return;


        // Spawn our Object at the reticle location.
        // var obj = GameObject.Instantiate(CarPrefab);
        /*var obj = GameObject.Instantiate(DataHandler.Instance.GetFurniture());
           Car = obj.GetComponent<CarBehaviour>();
           Car.transform.position = touch.position;
           DrivingSurfaceManager.LockPlane(Reticle.CurrentPlane);*/
        var dataHandler = DataHandler.Instance;
        if (dataHandler.isSelectFurniture())
        {
            Instantiate(dataHandler.GetFurniture(), Reticle.pose.position, Reticle.pose.rotation);
            dataHandler.CleanFurniture();
        }


        //btnDelete.onClick.AddListener(Destroy);
    }
    bool IsPointerOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
    private void Destroy()
    {
            Destroy(Car.gameObject);
    }
}
