using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Player/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public int Life;

    public float minAngle;
    public float maxAngle;


    public float stepAngle;

    //Variables mouvements en Y
    public float minSpeedY;
    public float maxSpeedY;
    public float maxSpeedDescenteY;

    public float coefAccelYMonter;
    public float coefDeccelYDescendre;
}
