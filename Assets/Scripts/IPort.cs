using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPort
{
    void AttachBattery(GameObject battery);
    void DetachBattery();
}
