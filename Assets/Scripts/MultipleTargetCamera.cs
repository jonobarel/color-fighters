﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private List<Transform> targets;
    [SerializeField] private float smoothTime = .5f;
    [SerializeField] private float minZoom = 40f;
    [SerializeField] private float maxZoom = 10f;
    [SerializeField] private float limitZoom = 50f;

    public List<Transform> Targets {
        get {return targets;}
    }
    private Vector3 velocity;
    private Camera camera;

    void Start() {
        camera = GetComponent<Camera>();
        targets = new List<Transform>();
    }
    void LateUpdate() {
        MoveCamera();
        ZoomCamera();
    }

    void MoveCamera() {
        if (targets.Count == 0) {
            return;
        }
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + cameraOffset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void ZoomCamera() {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / limitZoom);
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, newZoom, Time.deltaTime);
    }

    private Bounds EncapsulateBounds() {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++) {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds;
    }
    private Vector3 GetCenterPoint() {
        if (targets.Count == 1) {
            return targets[0].position;
        }

        var bounds = EncapsulateBounds();

        return bounds.center;
    }

    private float GetGreatestDistance() {

        var bounds = EncapsulateBounds();

        return bounds.size.x;

    }
}
