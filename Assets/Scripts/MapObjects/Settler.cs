using Hex;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapObjects;

public class Settler : MonoBehaviour
{
    // spawner creates settler, settler calls create city
    private HexGrid _hexGrid;
    private Hex.Hex _currentHex;
    private Hex.Hex _selectedPosition;
    private Spawner _spawner;
    private int _currentPlayer;
    private Camera _camera;
    private int _currentHexIndex;

    private void Start()
    {
        _hexGrid = FindObjectOfType<HexGrid>();
        _spawner = FindObjectOfType<Spawner>();
        _currentHex = _hexGrid.GetHexAt(transform.position);
        _camera = Camera.main; 
        player();
        
    }
    private void Update() 
    {
        detectClick();

    }
    public void detectClick()
    {
        
        _selectedPosition = null;
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("left click detected in Settler.cs");
            Ray ray = _camera!.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit)) return;

            if(!hit.transform.CompareTag("Unit")) return; 
            Debug.Log("Unit selected"); // doesn't get here
            _currentHexIndex = GetHexIndexAtWorldPos(hit.transform.position);
            _selectedPosition = _hexGrid.GetHexList()[_currentHexIndex]; 

            // is the unit a settler
            if(_hexGrid.GetUnitDictionary()[_selectedPosition].GetUnitType() != Unit.UnitType.Settler)
            {
                Debug.Log("IN last if in detectClick in Settler.cs");
                _selectedPosition = null;
                return;
            }
            return;
            
        }
        // /*TODO: if owner of hex is current player 
        // If there is multiple cities check for whcih one is clicked
        //  copy variables from UnitMovement.cs to get rid of errors
        // */

        Debug.Log("after the first big IF");
        if (_selectedPosition == null){ return;}
        
        
        if(Input.GetKeyDown(KeyCode.N)) // this will be replaced soon
        {
            Debug.Log("detected n click");
            if(compare(_selectedPosition))
            {
                _hexGrid.CreateCityAt(_selectedPosition, _currentPlayer, false);
                Debug.Log("city created");
            }
        }
        return;
    }

    private bool compare(Hex.Hex selectedPosition)
    {
        int mapRadius = 3;
        Vector3 center = selectedPosition.GetVectorCoordinates();   
        for (int i = 1; i < mapRadius; i++)
        {
             Vector3 hexCoordinates = HexGrid.AddCoordinates(center,
                HexGrid.CoordinateScale(_hexGrid.GetDirectionVector()[4], i));
            for (int k = 0; k < 6; k++)
            {
                for(int j = 0; j < i; j++)
                {
                    if(selectedPosition.GetOwnerID() != 0 ){return false;}

                    hexCoordinates = HexGrid.HexNeighbor(hexCoordinates, k);
                }
            }
            
        }
        return true;

    }
    private int GetHexIndexAtWorldPos(Vector3 coordinates)
    {
        int hexIndex = -1;
        for (int i = 0; i < _hexGrid.GetHexList().Count; i++)
        {
            if (_hexGrid.GetHexList()[i].WorldPosition.x == coordinates.x &&
                _hexGrid.GetHexList()[i].WorldPosition.z == coordinates.z)
            {
                hexIndex = i;
                break;
            }
        }
        return hexIndex;
    }
    private int player()
    {
        _currentPlayer = _spawner.currentPlayer();
        return _currentPlayer;
    }


}
