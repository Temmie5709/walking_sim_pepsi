using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBackObject : MonoBehaviour, IInteractable
{

    [SerializeField] GameObject ObjectOutline;
    private Material Material;

    private Color InitialColor;
    private float InitialThickness;

    [SerializeField] Color ColorLooking;
    [SerializeField, Range(0f, 0.5f)] float ThiknessLooking;
    [SerializeField] GameObject FlashbackObject;

    // Start is called before the first frame update
    void Start()
    {
        Material = ObjectOutline.GetComponent<Renderer>().material;
        InitialColor = Material.GetColor("_OutlineColor");
        InitialThickness = Material.GetFloat("_OutlineThickness");
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
        Material.SetFloat("_OutlineThickness", ThiknessLooking);
        Material.SetColor("_OutlineColor", ColorLooking);
        return;
    }
    public void StopLooking()
    {
        Debug.Log("StopedLooking");
        Material.SetFloat("_OutlineThickness", InitialThickness);
        Material.SetColor("_OutlineColor", InitialColor);
        return;
    }
    public void Interact() {
        Debug.Log("Flashback");
        FlashbackObject.SetActive(true);
    }
}
