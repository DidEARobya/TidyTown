using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Updated name to capitalised UI, previous was UiManager. (BH)
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TMP_Text lifeAmountText;
    public TMP_Text LitterAmountText;
    public TMP_Text binnedAmountText;
    public TMP_Text LitterCounterText;

    public GameObject recyclingCanvas;
    public GameObject joystickCanvas;
    public GameObject mainGameCanvas;

    public GameObject triggerUICanvas;
    public Image recyclingFadeInPanel;

    public Slider LitterAmountSlider;

    private LitterManager _litterManager;
    private ReferenceManager _referenceManager;

    public GameObject totalScore;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        _litterManager = GameManager.GetLitterManager();
        _referenceManager = GameManager.GetReferenceManager();

        _litterManager.AddUpdateUICallback(UpdateUI);
        UpdateUI();
    }
    private void UpdateUI()
    {
        LitterCounterText.text = _litterManager._worldLitter.Count.ToString();
        LitterAmountSlider.value = Mathf.InverseLerp(0, _referenceManager.MaxLitterAmount, _litterManager._worldLitter.Count);
    }
}
