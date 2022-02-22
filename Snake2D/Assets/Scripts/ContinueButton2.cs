using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton2 : MonoBehaviour
{
    public GameObject InstructionsUI;

    public GameObject InstructionsUI2;

    private Button button;

    //Awake
    void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        InstructionsUI.SetActive(false);
        InstructionsUI2.SetActive(true);
    }
}
