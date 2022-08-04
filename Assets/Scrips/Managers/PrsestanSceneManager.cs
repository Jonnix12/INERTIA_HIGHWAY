#region

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class PrsestanSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject _viewPort;
    [SerializeField] private Image _backGround;
    private bool _isFadeIn = true;

    public bool IsFadeIn
    {
        get { return _isFadeIn; }
    }

    public void FadeViewPort(bool toFade)
    {
        if (toFade)
        {
            StartCoroutine(FadeIn());
        }
        else
        {
            StartCoroutine(FadeOut());
        }
    }

    public IEnumerator FadeIn()
    {
        _viewPort.SetActive(true);
        float fade = 0;

        while (fade < 1)
        {
            Color color = new Color(0, 0, 0, fade += Time.deltaTime);
            yield return null;
            _backGround.color = color;
        }

        _isFadeIn = true;
    }

    public IEnumerator FadeOut()
    {
        float fade = 1;

        while (fade > 0)
        {
            Color color = new Color(0, 0, 0, fade -= Time.deltaTime);
            yield return null;
            _backGround.color = color;
        }

        _viewPort.SetActive(false);
        _isFadeIn = false;
    }
}