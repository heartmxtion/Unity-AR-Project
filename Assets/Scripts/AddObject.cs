using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddObject : MonoBehaviour
{
    private Button button;
    private ProgramManager ProgramManagerScript;
    void Start()
    {
        ProgramManagerScript = FindObjectOfType<ProgramManager>();

        button = GetComponent<Button>();
        button.onClick.AddListener(AddObjectFunction);
    }

    void AddObjectFunction()
    {
        ProgramManagerScript.ScrolView.SetActive(true);
    }
}
