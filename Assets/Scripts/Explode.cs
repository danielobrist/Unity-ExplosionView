using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;


[Serializable]
public class ExplodyComponent
{
    public Transform transformObject;
    public Vector3 originalPosition;
    public Vector3 explodedPosition;
}

public class Explode : MonoBehaviour
{
    public GameObject explosionCenter;
    public string explodableTag = "";
    public float explosionFactor = 5F;
    public float explosionSpeed = 1f;


    private List<Transform> taggedObjects;
    List<ExplodyComponent> explodyItems;
    bool isInExplodedView = false;
    bool isMoving = false;


    void Awake()
    {
        taggedObjects = new List<Transform>();
        AddChildrenByTag(this.transform, explodableTag, taggedObjects);

        explodyItems = new List<ExplodyComponent>();
        foreach (Transform taggedObject in taggedObjects)
        {
            ExplodyComponent explodyComponent = new ExplodyComponent();
            explodyComponent.transformObject = taggedObject;
            explodyComponent.originalPosition = taggedObject.position;
            explodyComponent.explodedPosition = (taggedObject.position - explosionCenter.transform.position) * explosionFactor;

            explodyItems.Add(explodyComponent);
        }
    }

    void Start()
    {
            
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ToggleExplodeView();
        }

        if (isMoving)
        {
            MoveExplodyComponents(isInExplodedView);
        }
    }


    // Search and add children with a given tag recursively
    private void AddChildrenByTag(Transform parent, string tag, List<Transform> list)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.CompareTag(tag))
            {
                list.Add(child);
            }
            AddChildrenByTag(child, tag, list);
        }
    }


    private void MoveExplodyComponents(bool isInExplodedView)
    {
        if (isInExplodedView)
        {

            //assemble back
            foreach (ExplodyComponent component in explodyItems)
            {
                component.transformObject.position = Vector3.Lerp(component.transformObject.position, component.explodedPosition, explosionSpeed / 100F);

                if (Vector3.Distance(component.transformObject.position, component.explodedPosition) == 0)
                {
                    component.transformObject.Translate(component.originalPosition);
                    isMoving = false;
                    isInExplodedView = false;
                }
            }
        }
        else
        {

            //disassemble
            foreach (ExplodyComponent component in explodyItems)
            {
                component.transformObject.position = Vector3.Lerp(component.transformObject.position, component.originalPosition, explosionSpeed / 100F);

                if (Vector3.Distance(component.transformObject.position, component.originalPosition) == 0)
                {
                    
                    isMoving = false;
                    isInExplodedView = true;
                }
            }
        }
    }

    private void ToggleExplodeView()
    {
        if (isInExplodedView)
        {
            isInExplodedView = false;
            isMoving = true;

        }
        else
        {
            isInExplodedView = true;
            isMoving = true;
        }
    }
}
