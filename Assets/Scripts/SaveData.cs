using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<WeaponsStruct> Weapons;
    public float SunPosition;
    public float playerXPosition;
    public float playerYPosition;
    public float playerZPosition;
}
