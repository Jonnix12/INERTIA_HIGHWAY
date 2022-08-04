using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelectionManager : MonoBehaviour
{
    [Header("Level Selection")]
    [SerializeField] private GameObject _levelSelectionPanel;
    [SerializeField] private GameObject _firstLevelButton;
    private int _levelChoice;

   [Header("Car Selection")]
    [SerializeField] private GameObject _carSelectionPanel;
    [SerializeField] private GameObject _carSelectionEnvironment;
    [SerializeField] private GameObject _firstCarSelectionButton;
    private int _carChoice;

    public void Start()
    {
        _levelChoice = 0;
        _carChoice = 0;
        MoveToLevelSelection();
    }

    public void MoveBackToMainMenu()
    {
        GameManager.Instance.LoadScene(1);
    }

    public void MoveToLevelSelection()
    {
        _levelSelectionPanel.gameObject.SetActive(true);
        _carSelectionPanel.gameObject.SetActive(false);
        _carSelectionEnvironment.gameObject.SetActive(false);
        UpdateFirstButton(_firstLevelButton);
    }

    public void MoveToCarChoice()
    {
        if(_levelChoice==0)
        {
            Debug.LogError("I didnt chose any level");
        }
        _levelSelectionPanel.gameObject.SetActive(false);
        _carSelectionPanel.gameObject.SetActive(true);
        _carSelectionEnvironment.gameObject.SetActive(true);
        UpdateFirstButton(_firstCarSelectionButton);
    }

    public void ChooseLevel(int levelIndex)
    {
        _levelChoice = levelIndex;
        MoveToCarChoice();
    }

    public void ChooseCar(int carIndex)
    {
        GameManager.Instance.playerManager.ScelectCar = carIndex;
        LoadLevel();
    }

    public void LoadLevel()
    {
        if (_levelChoice == 3 || _levelChoice == 4 || _levelChoice == 5)
        {
            GameManager.Instance.LoadScene(_levelChoice);
        }
    }

    private void UpdateFirstButton(GameObject firstButton)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
