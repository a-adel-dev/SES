using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolSpaceManager
{
    Transform GetParentClassroom(Location location)
    {
        return location.gameObject.transform.parent;
    }
}
