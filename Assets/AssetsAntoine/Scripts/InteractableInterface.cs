using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    // Méthode que toutes les classes liés a l'inteface devront implémenter
    void Interact(); 
    void Looking();
}