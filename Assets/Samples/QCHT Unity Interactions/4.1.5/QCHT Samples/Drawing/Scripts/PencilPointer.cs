// /******************************************************************************
//  * File: PencilPointer.cs
//  * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
//  *
//  *
//  ******************************************************************************/

using System;
using QCHT.Interactions;
using QCHT.Interactions.Distal;
using QCHT.Interactions.Core;
using QCHT.Interactions.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace QCHT.Samples.Drawing
{
    [DefaultExecutionOrder(XRRayInteractorManager.k_BeforeRayInteractorManager)] // Before all interactions
    public class PencilPointer : MonoBehaviour, IXRRayInteractorFilter
    {
        [SerializeField] private XrHandedness _handedness;

        [SerializeField]
        private InputActionProperty penSelectAction;

        [SerializeField]
        private InputActionProperty penSelectActionValue;

        [Space]
        [SerializeField, Tooltip("Pen down threshold")]
        private float _penDownValue;

        [SerializeField, Tooltip("Pen up threshold")]
        private float _penUpValue;

        [Space]
        [SerializeField] private float minSizeMultiplier = 1f;
        // [SerializeField] private float maxSizeMultiplier = 2f;

        [Space]
        [SerializeField] private ParticleSystem pointerParticles;
        [SerializeField] private ParticleSystem lineParticles;

        private float _referenceScale = 1f;
        private bool _penDown;

        public event Action<XrHandedness> onPenDown;
        public event Action<XrHandedness> onPenUp;

        private XRHandTrackingSubsystem _subsystem;

        public bool CanShowRay => !_penDown;

        private XRSwitchHandToControllerManager _controllerManager;

        private void OnEnable()
        {
            penSelectAction.EnableDirectAction();
            penSelectActionValue.EnableDirectAction();
            XRRayInteractorManager.AddFilterToViewer(this, _handedness);

            _controllerManager = FindObjectOfType<XRSwitchHandToControllerManager>();

            // Check if select value is already down. Preventing to draw directly on start.
            _penDown = IsPenDown();
        }

        private void OnDisable()
        {
            penSelectAction.DisableDirectAction();
            penSelectActionValue.DisableDirectAction();

            XRRayInteractorManager.RemoveFilterToViewer(this);
        }

        private void Update()
        {
            if (WasPressedThisFrame())
            {
                StartLineParticles();
                onPenDown?.Invoke(_handedness);
            }
            else if (WasReleasedThisFrame())
            {
                StopLineParticles();
                onPenUp?.Invoke(_handedness);
            }
        }
        
        private bool WasPressedThisFrame()
        {
            if (_penDown) return false;

            _penDown = IsPenDown();

            return _penDown;
        }

        private bool WasReleasedThisFrame()
        {
            if (!_penDown) return false;

            _penDown = !IsPenUp();

            return !_penDown;
        }

        private bool IsPenDown()
        {
#if !UNITY_EDITOR
            if (_controllerManager != null && _controllerManager.CurrentMode ==
                XRSwitchHandToControllerManager.ControllerMode.HandTracking)
            {
                return HandTrackingPinchDown();
            }

#endif
            // Prefer pen down action value if it has any control
            if (penSelectActionValue.action != null && penSelectActionValue.action.controls.Count > 0)
            {
                return penSelectActionValue.action.ReadValue<float>() > _penDownValue;
            }

            // Otherwise check penDownAction value
            if (penSelectAction.action != null && penSelectAction.action.controls.Count > 0)
            {
                return penSelectAction.action.IsPressed();
            }

            return false;
        }

        private bool IsPenUp()
        {
#if !UNITY_EDITOR
            if (_controllerManager != null && _controllerManager.CurrentMode ==
                XRSwitchHandToControllerManager.ControllerMode.HandTracking)
            {
                return HandTrackingPinchUp();
            }
#endif
            // Prefer pen down action value if it has any control
            if (penSelectActionValue.action != null && penSelectActionValue.action.controls.Count > 0)
            {
                return penSelectActionValue.action.ReadValue<float>() < _penUpValue;
            }

            // Otherwise check penDownAction value
            if (penSelectAction.action != null && penSelectAction.action.controls.Count > 0)
            {
                return !penSelectAction.action.IsPressed();
            }

            return true;
        }

        private bool HandTrackingPinchDown()
        {
            _subsystem ??= XRHandTrackingSubsystem.GetSubsystemInManager();
            if (_subsystem == null)
                return false;

            var hand = _handedness == XrHandedness.XR_HAND_LEFT ? _subsystem.LeftHand : _subsystem.RightHand;
            var pinchStrength = hand.GetFingerPinching(XrFinger.XR_HAND_FINGER_INDEX);
            return pinchStrength > _penDownValue;
        }

        private bool HandTrackingPinchUp()
        {
            _subsystem ??= XRHandTrackingSubsystem.GetSubsystemInManager();
            if (_subsystem == null)
                return false;

            var hand = _handedness == XrHandedness.XR_HAND_LEFT ? _subsystem.LeftHand : _subsystem.RightHand;
            var pinchStrength = hand.GetFingerPinching(XrFinger.XR_HAND_FINGER_INDEX);
            return pinchStrength < _penUpValue;
        }

        public void SetColor(Color color)
        {
            var main = pointerParticles.main;
            main.startColor = color;
        }

        public void SetScale(float scale)
        {
            _referenceScale = scale;
        }

        public void SetLineParticles(ParticleSystem particles)
        {
            lineParticles = particles;
            var particleSystemTransform = particles.transform;
            particleSystemTransform.SetParent(transform);
            particleSystemTransform.localPosition = Vector3.zero;
        }

        public void DestroyLineParticles()
        {
            if (lineParticles == null)
                return;

            Destroy(lineParticles.gameObject);
            lineParticles = null;
        }

        public void StartLineParticles()
        {
            if (lineParticles != null)
                lineParticles.Play();
        }

        public void StopLineParticles()
        {
            if (lineParticles != null)
                lineParticles.Stop();
        }

        public void UpdateScale()
        {
            if (pointerParticles == null) return;
            var main = pointerParticles.main;
            main.startSize = minSizeMultiplier * _referenceScale;
        }

        public void Show()
        {
            if (pointerParticles != null)
                pointerParticles.Play();
        }

        public void Hide()
        {
            if (pointerParticles != null)
                pointerParticles.Stop();
        }
    }
}