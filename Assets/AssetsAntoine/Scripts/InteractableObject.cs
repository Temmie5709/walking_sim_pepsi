using GLTFast.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable
{

    [SerializeField] GameObject ObjectOutline;
    private Renderer render;

    private Color InitialColor;
    private float InitialThickness;

    [SerializeField] Color ColorLooking;
    [SerializeField, Range(0f, 0.5f)] float ThiknessLooking;

    public UnityEvent Events;

    // Start is called before the first frame update
    void Start()
    {
        render = ObjectOutline.GetComponent<Renderer>();
        InitialColor = render.material.GetColor("_OutlineColor");
        InitialThickness = render.material.GetFloat("_OutlineThickness");
        if(InitialThickness >= ThiknessLooking)
        {
            Debug.LogWarning("La taille du countour initiale est plus grand que la taille quand il est regard�");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Looking() {
        Debug.Log("Looking");
        foreach(UnityEngine.Material material in render.materials)
        {
            material.SetFloat("_OutlineThickness", ThiknessLooking);
            material.SetColor("_OutlineColor", ColorLooking);

        }
        return;
        }
    public void StopLooking()
    {
        Debug.Log("StopedLooking");
        foreach (UnityEngine.Material material in render.materials)
        {
            material.SetFloat("_OutlineThickness", InitialThickness);
            material.SetColor("_OutlineColor", InitialColor);
        }
        return;
    }
    public void Interact() {
        Debug.Log("Do Something");
        Events.Invoke();
    }
    public bool IsActive()
    {
        return enabled;
    }
}
