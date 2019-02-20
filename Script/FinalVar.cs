using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalVar : MonoBehaviour
{

    public const int GRASS = 1;
    public const int TREE = 2;
    public const int BEACH = 3;
    public const int SHOAL = 4;
    public const int SEA = 5;
    public const int ICE = 6;
    public const int TOWN = 7;
    public const int TOWER = 8;
    public const int VILLAGE = 9;
    public const bool RED = true;
    public const bool BLUE = false;

    public static bool IsBuiling(int i)
    {
        if (i > 6) { return true; }
        return false;
    }
}
