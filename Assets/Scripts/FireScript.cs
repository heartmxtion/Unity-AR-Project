using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    private ProgramManager ProgramManagerScript;

    private Button button;
    private GameObject Beam;
    private Rigidbody BeamRigidBody;
    public float force;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(FireFunction);
        ProgramManagerScript = FindObjectOfType<ProgramManager>();
    }
    void FireFunction()
    {
        Beam = GameObject.Find("Beam");
        BeamRigidBody = Beam.GetComponent<Rigidbody>();
        if (!ProgramManagerScript.Recharging)
        {
            BeamRigidBody.AddForce(BeamRigidBody.transform.up * force, ForceMode.Impulse);
            ProgramManagerScript.Recharging = true;
        }
    }
}
