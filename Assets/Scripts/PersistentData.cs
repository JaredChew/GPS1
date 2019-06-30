﻿using UnityEngine;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable] public class PersistentData {

    Global.CheckpointLocation savedCheckpoint;

    private float playerPositionX;
    private float playerPositionY;

    private float time;

    private bool[] ability = new bool[Enum.GetNames(typeof(Global.BoxAbilities)).Length];

    public void saveData(Vector2 playerPosition, bool[] ability, float time, Global.CheckpointLocation savedCheckpoint) {

        playerPositionX = playerPosition.x;
        playerPositionY = playerPosition.y;

        this.time = time;
        this.ability = ability;

        this.savedCheckpoint = savedCheckpoint;

        FileStream file;

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        file = File.Create(Application.persistentDataPath + Global.saveFileName);//File.OpenRead(Application.persistentDataPath + Global.saveFileName);

        binaryFormatter.Serialize(file, this);

        file.Close();

    }

    public bool loadData() {

        FileStream file;

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (!File.Exists(Application.persistentDataPath + Global.saveFileName)) {
            return false;
        }

        file = File.OpenRead(Application.persistentDataPath + Global.saveFileName);

        PersistentData dataToLoad = (PersistentData)binaryFormatter.Deserialize(file);

        playerPositionX = dataToLoad.getPlayerPositionX();
        playerPositionY = dataToLoad.getPlayerPositionY();

        time = dataToLoad.getTime();
        ability = dataToLoad.getAbility();

        savedCheckpoint = dataToLoad.getSavedCheckpoint();

        file.Close();

        return true;

    }
    
    private float getPlayerPositionX() { return playerPositionX; }

    private float getPlayerPositionY() { return playerPositionY; }

    public Vector2 getPlayerPosition() {

        Vector2 playerPosition = new Vector2();

        playerPosition.x = playerPositionX;
        playerPosition.y = playerPositionY;

        return playerPosition;

    }

    public float getTime() { return time; }

    public bool[] getAbility() {
        return ability;
    }

    public Global.CheckpointLocation getSavedCheckpoint() {
        return savedCheckpoint;
    }

}