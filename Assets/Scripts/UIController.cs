using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    public CanvasGroup ControlPanelCanvasGroup;
    public FlyCamera flyCamera;

    public MapGenerator mapGenerator;
    public Toggle autoUpdateToggle;
    public Slider lodSlider;
    public TextMeshProUGUI lodTextValue;
    public TextMeshProUGUI noiseScaleTextValue;
    public TextMeshProUGUI octaveTextValue;
    public TextMeshProUGUI persistanceTextValue;
    public TextMeshProUGUI lacunarityTextValue;
    public TextMeshProUGUI seedTextValue;
    public TextMeshProUGUI offsetXTextValue, offsetYTextValue;
    public TextMeshProUGUI meshHeightMultTextValue;

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
        setNoiseScaleTextValue(mapGenerator.noiseScale);
        setOctavesTextValue(mapGenerator.octaves);
        setPersistanceTextValue(mapGenerator.persistance);
        setLacunarityTextValue(mapGenerator.lacunarity);
        setSeedTextValue(mapGenerator.seed);
        setOffsetTextValue();
        setMeshHeightMultTextValue(mapGenerator.meshHeightMultiplier);
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

    public void setAutoUpdate(bool newVal)
    {
        mapGenerator.autoUpdate = newVal;
        updateMapIfNeeded();
    }
    public void updateMapIfNeeded()
    {
        if (mapGenerator.autoUpdate == true)
        {
            mapGenerator.GenerateMap();
        }
    }

    public void setLoD(float newVal)
    {
        mapGenerator.levelOfDetail = (int)newVal;
        setLoDTextValue(newVal);

        updateMapIfNeeded();
    }
    public void setNoiseScale(float newVal)
    {
        mapGenerator.noiseScale = newVal;
        setNoiseScaleTextValue(newVal);

        updateMapIfNeeded();
    }
    public void setOctaves(float newVal)
    {
        mapGenerator.octaves = (int)newVal;
        setOctavesTextValue((int)newVal);

        updateMapIfNeeded();
    }
    public void setPersistance(float newVal)
    {
        mapGenerator.persistance = newVal;
        setPersistanceTextValue(newVal);

        updateMapIfNeeded();
    }
    public void setLacunarity(float newVal)
    {
        mapGenerator.lacunarity = newVal;
        setLacunarityTextValue(newVal);

        updateMapIfNeeded();
    }
    public void setSeed(float newVal)
    {
        mapGenerator.seed = (int)newVal;
        setSeedTextValue((int)newVal);

        updateMapIfNeeded();
    }
    public void setOffsetX(float newVal)
    {
        mapGenerator.offset.x = newVal;
        setOffsetTextValue();

        updateMapIfNeeded();
    }
    public void setOffsetY(float newVal)
    {
        mapGenerator.offset.y = newVal;
        setOffsetTextValue();

        updateMapIfNeeded();
    }
    public void setMeshHeightMult(float newVal)
    {
        mapGenerator.meshHeightMultiplier = newVal;
        setMeshHeightMultTextValue(newVal);

        updateMapIfNeeded();
    }

    private void setLoDTextValue(float newVal)
    {
        lodTextValue.text = newVal.ToString("F0");
    }
    private void setNoiseScaleTextValue(float newVal)
    {
        noiseScaleTextValue.text = newVal.ToString("F2");
    }
    private void setOctavesTextValue(int newVal)
    {
        octaveTextValue.text = newVal.ToString();
    }
    private void setPersistanceTextValue(float newVal)
    {
        persistanceTextValue.text = newVal.ToString("F2");
    }
    private void setLacunarityTextValue(float newVal)
    {
        lacunarityTextValue.text = newVal.ToString("F2");
    }
    private void setSeedTextValue(int newVal)
    {
        seedTextValue.text = newVal.ToString();
    }
    private void setOffsetTextValue()
    {
        offsetXTextValue.text = mapGenerator.offset.x.ToString("F2");
        offsetYTextValue.text = mapGenerator.offset.y.ToString("F2");
    }
    private void setMeshHeightMultTextValue(float newVal)
    {
        meshHeightMultTextValue.text = newVal.ToString();
    }

}