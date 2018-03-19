using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshField
{
    private int x, y;
    private float sizeX, sizeY;

    public bool IsSet { get; private set; }
    public GameObject GameObjectOnIt { get; private set; }
    public int X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }
    public int Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }

    public MeshField(int x, int y, float sizeX, float sizeY)
    {
        this.x = x;
        this.y = y;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
    }

    public void SetField(GameObject go)
    {
        if(go != null)
        {
            IsSet = true;
            GameObjectOnIt = go;
        }
        else
        {
            IsSet = false;
            GameObjectOnIt = null;
        }
    }
	
}
