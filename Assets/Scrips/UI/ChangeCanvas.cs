using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChangeCanvas : MonoBehaviour
{

    [Header("FirstButtons")]
    [SerializeField] private GameObject _mainMenuFirstButton;
    [SerializeField] private GameObject _optionsFirstButton;
    [SerializeField] private GameObject _volumeFirstButton;
    [SerializeField] private GameObject _graphicsFirstButton;

    [SerializeField] private Canvas _mainMenuCanvas;
    [SerializeField] private Canvas _optionsCanvas;
    [SerializeField] private Canvas _volumeCanvas;
    [SerializeField] private Canvas _graphicsCanvas;

    void Start()
    {
        MoveToMainMenu();
    }

    public void MoveToMainMenu()
    {
        _optionsCanvas.gameObject.SetActive(false);
        _mainMenuCanvas.gameObject.SetActive(true);
        UpdateFirstButton(_mainMenuFirstButton);
    }

    public void MoveToOption()
    {
        _mainMenuCanvas.gameObject.SetActive(false);
        _volumeCanvas.gameObject.SetActive(false);
        _graphicsCanvas.gameObject.SetActive(false);
        _optionsCanvas.gameObject.SetActive(true);
        UpdateFirstButton(_optionsFirstButton);
    }

    public void MoveToVolume()
    {
        _optionsCanvas.gameObject.SetActive(false);
        _volumeCanvas.gameObject.SetActive(true);
        UpdateFirstButton(_volumeFirstButton);
    }

    public void MoveToGraphics()
    {
        _optionsCanvas.gameObject.SetActive(false);
        _graphicsCanvas.gameObject.SetActive(true);
        UpdateFirstButton(_graphicsFirstButton);
    }

    private void UpdateFirstButton(GameObject firstButton)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
