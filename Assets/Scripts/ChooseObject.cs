using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseObject : MonoBehaviour
{
    private ProgramManager ProgramManagerScript;

    private Button button;

    public GameObject ChoosedObject;
    void Start()
    {
        ProgramManagerScript = FindObjectOfType<ProgramManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(ChooseObjectFunction);
    }

    void ChooseObjectFunction()
    {
        ProgramManagerScript.ObjectToSpawn = ChoosedObject;
        ProgramManagerScript.ChooseObject = true;
        ProgramManagerScript.ScrolView.SetActive(false);
    }
}
