using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Push
{
    private Vector2 difference; // разница координат
    private float length; // расстояние
    private float boost; // ускорение
    private System.Random rand = new System.Random();

    public Push(Dictionary<string, float> info, Transform transform, Transform obj, ref float speed) {
        difference = obj.position - transform.position;
        length = info["length"];
        speed = info["speed"];
        boost = -(float)Math.Pow(speed, 2)/(2*length);
        // Debug.Log($"длина: {length}    скорость: {speed}    время: {-speed/boost}    ускорение: {boost}");
    }

    public void OnPushStay(ref Vector2 move, ref float speed) {
        move.x = -difference.x / (Math.Abs(difference.x) + Math.Abs(difference.y));
        move.y = -difference.y / (Math.Abs(difference.x) + Math.Abs(difference.y));

        speed += boost*Time.deltaTime;
    }

    /*public void OnPushEnter(Dictionary<float, List<float>> data, Transform transform, Transform obj, ref float speed) {
        difference = obj.position - transform.position;  

        List<float> keyList = new List<float>(data.Keys);
        length = keyList[rand.Next(0, keyList.Count)];
        
        startSpeed = data[length][rand.Next(0, data[length].Count)];
        speed = startSpeed;
        boost = -(float)Math.Pow(startSpeed, 2)/(2*length);
        // Debug.Log($"длина - {Length}\nскорость - {Speed}\nускорение - {Boost}\nвремя - {Length/2}");
        Debug.Log($"длина: {length}    скорость: {startSpeed}    время: {-startSpeed/boost}    ускорение: {boost}");

    } */

    // public void OnPushExit() {
    // }
}
