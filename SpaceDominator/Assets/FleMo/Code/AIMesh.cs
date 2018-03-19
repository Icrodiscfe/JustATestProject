using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMesh : MonoBehaviour
{
    private GameObject player;
    private List<MeshField> meshField;
    private Stack<MeshField> freeFields;
    private List<GameObject> visualizeObjects;

    public int X { get; private set; }
    public int Y { get; private set; }
    public int MinX { get; private set; }
    public int MinY { get; private set; }
    public int MaxX { get; private set; }
    public int MaxY { get; private set; }

    [SerializeField]
    private float _SizeX;
    [SerializeField]
    private float _SizeY;
    [SerializeField]
    private int _X;
    [SerializeField]
    private int _Y;
    [SerializeField]
    private bool _Visualize;
    [SerializeField]
    private GameObject _VisualizeObject;
    [SerializeField]
    private float _SizeXMultiplier = 1;
    [SerializeField]
    private float _SizeYMultiplier = 1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        X = (int)player.transform.position.x;
        Y = (int)player.transform.position.z;
        MinX = (int)player.transform.position.x - _X / 2;
        MinY = (int)player.transform.position.y - _Y / 2;
        MaxX = (int)player.transform.position.x + _X / 2;
        MaxY = (int)player.transform.position.y + _Y / 2;
        meshField = new List<MeshField>();

        if (_Visualize)
        {
            visualizeObjects = new List<GameObject>();
        }

        for (var i = 0; i < _X * _Y; i++)
        {
            meshField.Add(new MeshField(int.MinValue, int.MinValue, _SizeX, _SizeY));
            GameObject go = Instantiate(_VisualizeObject);
            go.transform.localScale = new Vector3(_SizeX * _SizeXMultiplier, 1, _SizeY * _SizeYMultiplier);   
            visualizeObjects.Add(go);
        }

        freeFields = new Stack<MeshField>();
        PlayerMoved();
    }

    void Update()
    {
        int x = (int)player.transform.position.x;
        int y = (int)player.transform.position.z;

        if(x != X || y != Y)
        {
            X = x;
            Y = y;
            MinX = (int)player.transform.position.x - _X / 2;
            MinY = (int)player.transform.position.y - _Y / 2;
            MaxX = (int)player.transform.position.x + _X / 2;
            MaxY = (int)player.transform.position.y + _Y / 2;
            PlayerMoved();
        }
    }

    private void PlayerMoved()
    {
        //return;
        meshField.Where(f =>
            f.X < MinX ||
            f.Y < MinY ||
            f.X > MaxX ||
            f.Y > MinY).ToList().ForEach(f => freeFields.Push(f));

        int counter = 0;

        for(int x = MinX; x <= MaxX; x++)
        {
            for(int y = MinY; y <= MaxY; y++)
            {
                MeshField field = meshField.Cast<MeshField>().Where(f => f.X == x && f.Y == y).FirstOrDefault();

                if(field == null)
                {
                    MeshField mf = freeFields.Pop();
                    mf.X = x;
                    mf.Y = y;
                }

                if (_Visualize)
                {
                    visualizeObjects[counter].transform.position = new Vector3(x * _SizeX, 0.1f, y * _SizeY);
                }

                counter++;
            }
        }
    }
}
