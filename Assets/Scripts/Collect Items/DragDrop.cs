using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        // alpha will make it a little bit tranparent because it represents as we are dragging (called as effect)
        canvasGroup.alpha = .6f;
        // we will take this item from the previous slot
        canvasGroup.blocksRaycasts = false;
        // set the start position
        startPosition = transform.position;
        // check the parent of this item that we drag
        startParent = transform.parent;
        // set this item as a object that it will belong to other objects depend on the position that we drop
        transform.SetParent(transform.root);
        // item that we drag
        itemBeingDragged = gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // change the position of gameobject, and it will follow the mouse speed of users
        rectTransform.anchoredPosition += eventData.delta;
        // delta just means speed, it is used to follow the mouse speed and without canvas.scaleFactor it will move weird
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        // it is used to check whether item fall into these slots or not, if it is not it will return the previous slot (the previous parent)
        if(transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        // feature of drag items and now we set it default
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
