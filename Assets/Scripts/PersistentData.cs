using UnityEngine;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System.Collections.Generic;

[System.Serializable] public class PersistentData {

    Dictionary<int, bool> door;

    Global.CheckpointLocation currentCheckPoint;

    Global.Areas currentArea;

    private float playerPositionX;
    private float playerPositionY;

    private float facingDirection;

    private float time;

    private bool loadableData;

    private bool[] ability = new bool[Enum.GetNames(typeof(Global.BoxAbilities)).Length];

    public void saveData(Vector2 playerPosition, float facingDirection, bool[] ability, float time, Global.CheckpointLocation currentCheckPoint, Global.Areas currentArea) {

        playerPositionX = playerPosition.x;
        playerPositionY = playerPosition.y;

        this.facingDirection = facingDirection;
        this.time = time;
        this.ability = ability;

        this.currentCheckPoint = currentCheckPoint;
        this.currentArea = currentArea;

        FileStream file;

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        file = File.Create(Application.persistentDataPath + Global.saveFileName);//File.OpenRead(Application.persistentDataPath + Global.saveFileName);

        binaryFormatter.Serialize(file, this);

        file.Close();

    }

    public void loadData() {

        FileStream file;

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (!File.Exists(Application.persistentDataPath + Global.saveFileName)) {
            loadableData = false;
            return;
        }

        file = File.OpenRead(Application.persistentDataPath + Global.saveFileName);

        PersistentData dataToLoad = (PersistentData)binaryFormatter.Deserialize(file);

        playerPositionX = dataToLoad.getPlayerPositionX();
        playerPositionY = dataToLoad.getPlayerPositionY();

        facingDirection = dataToLoad.getPlayerFacingDirection();

        time = dataToLoad.getTime();
        ability = dataToLoad.getAbility();

        currentCheckPoint = dataToLoad.getCurrentCheckPoint();
        currentArea = dataToLoad.getCurrentArea();

        door = dataToLoad.getDoorStatus();

        if(door == null) {
            door = new Dictionary<int, bool>();
        }

        file.Close();

        loadableData = true;

    }
    
    private float getPlayerPositionX() { return playerPositionX; }

    private float getPlayerPositionY() { return playerPositionY; }

    public Vector2 getPlayerPosition() {

        Vector2 playerPosition = new Vector2();

        playerPosition.x = playerPositionX;
        playerPosition.y = playerPositionY;

        return playerPosition;

    }

    public float getPlayerFacingDirection() {
        return facingDirection;
    }

    public float getTime() {
        return time;
    }

    public bool[] getAbility() {
        return ability;
    }

    public Global.CheckpointLocation getCurrentCheckPoint() {
        return currentCheckPoint;
    }

    public Global.Areas getCurrentArea() {
        return currentArea;
    }

    public bool isDataLoadable() {
        return loadableData;
    }

    public void saveDoorStatus(int doorIndex, bool isOpen) {

        // !! Still testing !! //
        if (door == null) { door = new Dictionary<int, bool>(); }

        door.Add(doorIndex, isOpen);
    }

    public Dictionary<int, bool> getDoorStatus() {
        return door;
    }

}