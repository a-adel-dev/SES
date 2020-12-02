using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teachersroom : MonoBehaviour
{
    bool classesInsession = false;
    SchoolManager schoolManager;

    // Start is called before the first frame update
    void Start()
    {
        schoolManager = FindObjectOfType<SchoolManager>();
    }

    // Update is called once per frame
    void Update()
    {
        classesInsession = schoolManager.classInSession;
    }
}
