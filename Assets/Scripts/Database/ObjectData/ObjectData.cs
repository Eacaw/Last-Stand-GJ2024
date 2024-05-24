/*
 *  Copyright Chamber Designs 2024. All rights reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectData : MonoBehaviour
{
    public string Id { get; private set; }
    public string ObjectName { get; set; }

    // Object Information
    [field: SerializeField]
    public GameObject Prefab { get; set; }
    public LayoutData Shape { get; set; } // Grid layout, with pivot on 0,0
    public int currentRotation { get; set; } // deg

    // Not yet implemented/used in the game
    public float ProductionRate { get; set; }
    public float EnergyConsumption { get; set; }
    public float Cost { get; set; }

    public ObjectData(string objectName, LayoutData shape)
    {
        this.Id = Guid.NewGuid().ToString();
        this.ObjectName = objectName;
        this.Shape = shape;
    }

    public int rotate()
    {
        this.currentRotation = this.currentRotation + 90;
        if (this.currentRotation == 360)
        {
            this.currentRotation = 0;
        }
        this.Shape = Rotation.rotate(Shape, this.currentRotation);

        return this.currentRotation;
    }

    public virtual GameObject getGameObject(Vector3 worldPos)
    {
        return Instantiate(this.Prefab, worldPos, Quaternion.Euler(0, this.currentRotation, 0));
    }

    public List<Vector2Int> getOccupiedCells(Vector3Int gridPos)
    {
        return LayoutUtil.getOccupiedCells(gridPos, this.Shape);
    }
}
