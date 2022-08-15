using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowShell : MonoBehaviour
{
    private ProgramManager ProgramManagerScript;
    private TrajectoryRenderer TrajectoryRendererScript;

    [SerializeField] private GameObject ShellPrefab;
    private GameObject ShellObject;
    private Rigidbody ShellRigidBody;
    private Vector3 speed;

    private GameObject fieldobject;
    private InputField field;
    private string ForceString;
    private int force;
    private Rigidbody CollisionRigidBody;
    void Start()
    {
        ProgramManagerScript = FindObjectOfType<ProgramManager>();
        TrajectoryRendererScript = FindObjectOfType<TrajectoryRenderer>();
        fieldobject = GameObject.Find("InputField");
        field = fieldobject.GetComponent<InputField>();
    }


    void Update()
    {
        ForceString = field.text;

        int parser_test;
        bool is_parsed = System.Int32.TryParse(ForceString, out parser_test);
        if (is_parsed == true)
        {
            force = parser_test;
        }

        //force = System.Int32.Parse(ForceString);
        speed = transform.forward * 2 + transform.up * force;
        TrajectoryRendererScript.ShowTrajectory(transform.position + new Vector3(0, 0.25f, 0), speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ShellObject = Instantiate(ShellPrefab, transform.position + new Vector3(0, 0.25f, -0.05f), ShellPrefab.transform.rotation);
        ShellRigidBody = ShellObject.GetComponent<Rigidbody>();
        ShellRigidBody.AddForce(speed, ForceMode.Impulse);

        CollisionRigidBody = collision.rigidbody;
        CollisionRigidBody.AddForce(-CollisionRigidBody.transform.up, ForceMode.Impulse);

        ProgramManagerScript.Recharging = true;
        Destroy(ShellObject, 10);
    }
}
