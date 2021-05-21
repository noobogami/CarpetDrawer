using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Garaj.GLog;
using Garaj.Zoom;
using UnityEngine;
using UnityEngine.EventSystems;
using Color = UnityEngine.Color;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Vector2Int dimension;
    [SerializeField] private GameObject cellPrefab;

    private void Awake()
    {
        _cells = new List<Cell>();
        CurrentColor = Color.black;
    }

    internal static Color CurrentColor;
    private List<Cell> _cells; 
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            CurrentColor = Color.red;
        if (Input.GetKey(KeyCode.Alpha2))
            CurrentColor = Color.blue;
        if (Input.GetKey(KeyCode.Alpha3))
            CurrentColor = Color.green;
        if (Input.GetKey(KeyCode.Alpha4))
            CurrentColor = Color.grey;
        if (Input.GetKey(KeyCode.Alpha5))
            CurrentColor = Color.yellow;
        if (Input.GetKey(KeyCode.Alpha6))
            CurrentColor = Color.black;
        
        if (Input.GetKey(KeyCode.E))
            Export();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.dragging || GZoom.Ins.Dragging) return;

        var pos = Camera.main.ScreenToWorldPoint(eventData.position);
        var convertedPos = new Vector2Int((int) Mathf.Floor(pos.x + 0.5f),(int) Mathf.Floor(pos.y + 0.5f));
        var cell = Instantiate(cellPrefab, transform)
            .GetComponent<Cell>();
        cell.Init(convertedPos, CurrentColor);
        _cells.Add(cell.GetComponent<Cell>());
    }

    private void Export()
    {
        const int scale = 1;
        var tex = new Texture2D(600, 600);
        foreach (var cell in _cells)
        {
            for (var i = 0; i < scale; i++)
            for (var j = 0; j < scale; j++)
                tex.SetPixel(cell.Pos.x * scale + i, cell.Pos.y * scale + j, cell.Color);
        }
        
        var bytes = tex.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/Export.png", bytes);
    }
}
