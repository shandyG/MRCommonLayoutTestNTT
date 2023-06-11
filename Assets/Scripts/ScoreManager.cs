using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    //ゲームオブジェクトのリスト作成　inspectorにて追加
    public List<GameObject> objList = new List<GameObject>();
    public GameObject centerObj;
    //ゲームオブジェクトの初期座標のlist
    private List<Vector3> posList = new List<Vector3>();

    //タイムスタンプ用
    DateTime dt;


    void Start()
    {
        //ゲームオブジェクトの初期座標をlistに追加
        foreach (var obj in objList)
        {
            posList.Add(obj.transform.position);
        }

        MeasurePositionDiff();
    }

    //オブジェクトの位置を初期状態に戻す
    public void ResetObjPosition()
    {
        int num = 0;

        foreach(var pos in posList)
        {
            objList[num].transform.position = pos;
            num++;
        }
    }

    //Listにあるオブジェクトと中心にあるオブジェクトの座標の差 Vector3で出力
    public void MeasurePositionDiff()
    {
        foreach (var obj in objList)
        {
            var diff = centerObj.transform.position - obj.transform.position;
            dt = DateTime.Now;

            Debug.Log(obj.name + " : " + diff + dt);
        }
    }
}
