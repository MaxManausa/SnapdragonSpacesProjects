// /******************************************************************************
//  * File: XRRayInteractorManager.cs
//  * Copyright (c) 2023 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
//  *
//  *
//  ******************************************************************************/

using System.Collections.Generic;
using System.Linq;
using QCHT.Interactions.Core;
using QCHT.Interactions.Hands;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Object = UnityEngine.Object;

namespace QCHT.Interactions.Distal
{
    [DefaultExecutionOrder(k_RayInteractorManager)]
    public class XRRayInteractorManager : MonoBehaviour, IHandedness
    {
        // Before all interactions
        public const int k_RayInteractorManager = XRInteractionUpdateOrder.k_Interactors - 1;
        public const int k_BeforeRayInteractorManager = k_RayInteractorManager - 2;

        [field: SerializeField] public XrHandedness Handedness { get; private set; }

        [SerializeField] protected XRRayInteractor rayInteractor;

        [Interface(typeof(IXRRayInteractorFilter))]
        [SerializeField] protected List<Object> startFilters = new List<Object>();

        private readonly List<IXRRayInteractorFilter> _filters = new List<IXRRayInteractorFilter>();

        internal static List<XRRayInteractorManager> activeViewers = new List<XRRayInteractorManager>();

        protected void Awake()
        {
            foreach (var filter in startFilters)
            {
                if (filter == null || !(filter is IXRRayInteractorFilter visualFilter))
                    return;

                _filters.Add(visualFilter);
            }

            activeViewers.Add(this);
        }

        protected void OnDestroy()
        {
            activeViewers.Remove(this);
        }

        public static void AddFilterToViewer(IXRRayInteractorFilter filter, XrHandedness handedness)
        {
            foreach (var viewer in activeViewers)
            {
                if (viewer.Handedness == handedness)
                {
                    viewer.AddFilter(filter);
                }
            }
        }

        public static void RemoveFilterToViewer(IXRRayInteractorFilter filter)
        {
            foreach (var viewer in activeViewers)
            {
                viewer.RemoveFilter(filter);
            }
        }

        public void AddFilter(IXRRayInteractorFilter filter)
        {
            _filters.Add(filter);
        }

        public void RemoveFilter(IXRRayInteractorFilter filter)
        {
            _filters.Remove(filter);
        }

        protected virtual void Update()
        {
            if (rayInteractor != null)
            {
                rayInteractor.enabled = _filters.Aggregate(true, (current, filter) => current & filter.CanShowRay);
            }
        }
    }
}