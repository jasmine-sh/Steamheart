﻿using System.Collections.Generic;
using MapObjects;
using UnityEngine;

namespace Misc
{
    /// <summary> ************************************************************
    /// This class will be made for each player and will hold what they own
    /// and all of their resources.
    /// </summary> ***********************************************************
    public class Player
    {
        public bool isAlive;
        private int _playerID;
        private List<City> _ownedCities = new List<City>();

        public Player(int id)
        {
            _playerID = id;
            isAlive = true;
        }
        
        public int GetPlayerID() { return _playerID; }
        public List<City> GetOwnedCities() { return _ownedCities; }
        public void AssignCity(City city) { _ownedCities.Add(city); }
        public void RemoveCity(City city)
        {
            _ownedCities.Remove(city);
            if (_ownedCities.Count == 0) { isAlive = false; }
        }
    }
}