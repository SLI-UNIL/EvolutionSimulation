using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public CanvasGroup ControlPanelCanvasGroup;
    public FlyCamera flyCamera;

    public MapGenerator mapGenerator;
    public Toggle autoUpdateToggle;
    public Slider lodSlider;
    public TextMeshProUGUI lodTextValue;

    private void Start()
    {
        ToggleControlPanel();

        if (ControlPanelCanvasGroup.alpha > 0.5f)
            flyCamera.enabled = false;
        else
            flyCamera.enabled = true;

        autoUpdateToggle.isOn = mapGenerator.autoUpdate;
        lodSlider.value = mapGenerator.levelOfDetail;
        setLoDTextValue(mapGenerator.levelOfDetail);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            ToggleControlPanel();
        }
    }

    public void ToggleControlPanel()
    {
        if (ControlPanelCanvasGroup.alpha > 0.5f)
        {
            LeanTween.alphaCanvas(ControlPanelCanvasGroup, 0f, .3f);
            LeanTween.scale(ControlPanelCanvasGroup.gameObject, Vector3.zero, 0.4f).setEase(LeanTweenType.easeInBack);
            ControlPanelCanvasGroup.interactable = false;
            ControlPanelCanvasGroup.blocksRaycasts = false;
            flyCamera.enabled = true;
            Cursor.visible = false;
        }
        else
        {
            LeanTween.alphaCanvas(ControlPanelCanvasGroup, 1f, .3f);
            LeanTween.scale(ControlPanelCanvasGroup.gameObject, Vector3.one, 0.4f).setEase(LeanTweenType.easeOutBounce);
            ControlPanelCanvasGroup.interactable = true;
            ControlPanelCanvasGroup.blocksRaycasts = true;
            flyCamera.enabled = false;
            Cursor.visible = true;
        }
    }

    public void setLoD(float newVal)
    {
        mapGenerator.levelOfDetail = (int)newVal;
        setLoDTextValue(newVal);

        if (mapGenerator.autoUpdate == true)
        {
            mapGenerator.GenerateMap();
        }
    }

    public void setAutoUpdate(bool newVal)
    {
        mapGenerator.autoUpdate = newVal;

        if (mapGenerator.autoUpdate == true)
        {
            mapGenerator.GenerateMap();
        }
    }

    private void setLoDTextValue(float newVal)
    {
        lodTextValue.text = newVal.ToString("F0");
    }
}
