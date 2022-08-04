using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    [SerializeField] private CarInputManager _carInputManager;
    [SerializeField] private CarMoveAgent _carAI;

    public void OnEndRace()
    {
        _carInputManager.enabled = false;
        _carAI.enabled = true;
    }
}
