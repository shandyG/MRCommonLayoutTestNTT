using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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
        List<string> curPosList = new List<string>();

        foreach (var obj in objList)
        {
            var diff = obj.transform.position - centerObj.transform.position;
            dt = DateTime.Now;

            curPosList.Add(diff.ToString("F4"));

            //ToString F4 で小数点桁数変更
            //Debug.Log(obj.name + " : " + diff.ToString("F4") + dt);
        }

        //StartCoroutine(PostData(curPosList));
        
    }

    private IEnumerator PostData(List<string> curPosList)
    {
        Debug.Log("データ送信開始・・・");
        var form = new WWWForm();
        form.AddField("obj1", curPosList[0]);
        form.AddField("obj2", curPosList[1]);
        form.AddField("obj3", curPosList[2]);

        var request = UnityWebRequest.Post("https://script.google.com/macros/s/" + "AKfycbyTzJx8-ibxD_ycwjIp48DTKxvn_S8ndVzV7BTY6qJm1yoZnJrFiw2foPED0wV9_OpiTg" + "/exec", form);

        yield return request.SendWebRequest();

        if (request.isHttpError)
        {
            Debug.Log($"[Error]Response Code : {request.responseCode}");
        }
        else if (request.isNetworkError)
        {
            if (request.error == "Request timeout")
            {
                // タイムアウト時の処理
                Debug.Log($"[Error]TimeOut");
            }
            else
            {
                Debug.Log($"[Error]Message : {request.error}");
            }
        }
        else
        {
            // 成功したときの処理
            Debug.Log($"[Success]");
        }
    }
}
