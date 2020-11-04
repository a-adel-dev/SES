using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolSubSpace : MonoBehaviour
{
    private SchoolSpace parent;

    private void Awake()
    {
        parent = transform.parent.GetComponent<SchoolSpace>() ;
    }

    public SchoolSpace getParentSpace()
    {
        return parent;
    }

}
