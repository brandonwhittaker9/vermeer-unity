using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Dropdown _modeDropdown;

    [SerializeField]
    private Button _recordBtn;

    [SerializeField]
    private Button _stopBtn;

    [SerializeField]
    private Button _setAoiBtn;

    [SerializeField]
    private Button _previsBtn;

    private enum Mode { Continous, Aoi}

    void Start()
    {
        // Dropdown set to Continuous Mode by default
        _modeDropdown.SetValueWithoutNotify(0);
        _previsBtn.gameObject.SetActive(false);
        SetMode(Mode.Continous);

        _modeDropdown.onValueChanged.AddListener(OnModeChanged);
    }

    private void SetMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.Continous:
                _stopBtn.gameObject.SetActive(false);
                _setAoiBtn.gameObject.SetActive(false);
                _recordBtn.gameObject.SetActive(true);
                break;
            case Mode.Aoi:
                _stopBtn.gameObject.SetActive(false);
                _setAoiBtn.gameObject.SetActive(true);
                _recordBtn.gameObject.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnModeChanged(int val)
    {
        if (val == 0)
        {
            SetMode(Mode.Continous);
        }
        else if (val == 1)
        {
            SetMode(Mode.Aoi);
        }
    }
}
