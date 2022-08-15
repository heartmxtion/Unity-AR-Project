using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    private ProgramManager ProgramManagerScript;
    void Start()
    {
        ProgramManagerScript = FindObjectOfType<ProgramManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ProgramManagerScript.Recharging = false;
    }
}
