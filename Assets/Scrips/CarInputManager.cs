using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputManager : MonoBehaviour
{
    #region ScrifsReference

    public CarInput Input;

    #endregion

    private void Awake()
    {
        Input = new CarInput();
    }

    private void OnEnable()
    {
        Input.Enable();
    }

    private void OnDisable()
    {
        Input.Disable();
    }

}
