/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

/**
 * Spawns a <see cref="CarBehaviour"/> when a plane is tapped.
 */
[Serializable]

public class InteractionController : ARBaseGestureInteractable
{
    private Button btnDelete;
    static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();
    [SerializeField] public GameObject Reticle;
    [SerializeField]
    private Pose pose;
    [SerializeField] TMP_Text HintText;


    private Touch touch;
    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (gesture.targetObject == null || gesture.targetObject.layer == 9) 
        {
            return true;
        }
        return false;
    }
    protected override void OnEndManipulation(TapGesture gesture)
    {
        HintText.text = "Tap to place";

        if (gesture.isCanceled)
            return;
        if (gesture.targetObject != null)
            return;
        if (IsPointerOverUI(gesture))
            return;
        if (GestureTransformationUtility.Raycast(gesture.startPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            HintText.text = "Select menu object";
            var hit = hits[0];
            var dataHandler = DataHandler.Instance;
            if (dataHandler.isSelectFurniture())
            {
                HintText.text = "Object placed";
                GameObject placementObject = Instantiate(dataHandler.GetFurniture(), pose.position, pose.rotation);
                var anchorObject = new GameObject("PlacementAnchor");
                anchorObject.transform.position = hit.pose.position;
                anchorObject.transform.rotation = hit.pose.rotation;
                placementObject.transform.parent = anchorObject.transform;
                dataHandler.CleanFurniture();
            }

        }
    }
    void CrosshairCalculation()
    {
        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));

        if (GestureTransformationUtility.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            HintText.text = "";
            pose = hits[0].pose;
            Reticle.transform.position = pose.position;
            Reticle.transform.rotation = pose.rotation;
        }
        HintText.text = "Plane detecting...";

    }
    private void FixedUpdate()
    {
        CrosshairCalculation();
        //btnDelete.onClick.AddListener(Destroy);
    }
    bool IsPointerOverUI(TapGesture touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.startPosition.x, touch.startPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
    private void Destroy()
    {
           // Destroy(Car.gameObject);
    }
}
