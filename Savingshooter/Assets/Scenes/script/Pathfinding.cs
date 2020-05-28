using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private GameObject finalTarget; // 最終目標
    private Vector3 _target;         // 近くのターゲット
    private List<Vector3> _targetList = new List<Vector3>();
    private GameObject gameCtr;
    private targetGenerator _targetGenerator;
    private Vector3[] _edges = new Vector3[0];

    private void Awake()
    {
        finalTarget = GameObject.FindGameObjectWithTag("Player");     
        gameCtr = GameObject.FindGameObjectWithTag("GameController");
        _targetGenerator = gameCtr.GetComponent<targetGenerator>();
    }

    // Update is called once per frame
    public Vector3 PathFind(Vector3 rayOrigin)
    {
       
        rayOrigin.y = 0;
        _target = Vector3.zero;
        var ray = new Ray(rayOrigin, (finalTarget.transform.position - rayOrigin).normalized);
        RaycastHit hit;
       
        if (Physics.Raycast(ray, out hit))
        {
           
            if (hit.collider.CompareTag("Player"))
            {
                _target = hit.transform.position;
                // 二次元にするためにyを調整
                _target.y = rayOrigin.y;
                return _target;

            }
            if (hit.collider.CompareTag("Collision"))
            {
                // 固有ベクトルは単位ベクトルを使用
                // eageは1はx軸2はy軸3はy軸を表す(角度が0の時)
                Vector3 edge1 = hit.transform.localToWorldMatrix.MultiplyVector(new Vector3(1, 0, 0));
                Vector3 edge2 = hit.transform.localToWorldMatrix.MultiplyVector(new Vector3(0, 1, 0));
                Vector3 edge3 = hit.transform.localToWorldMatrix.MultiplyVector(new Vector3(0, 0, 1));

                // 四つの頂点にターゲットを設定
                Vector3 target1 = hit.transform.position + (edge1 * 0.5f) + (edge2 * 0.5f) + (edge3 * 0.5f);
                target1.y = rayOrigin.y;
                Vector3 target2 = target1 - edge3;
                target2.y = rayOrigin.y;
                Vector3 target3 = target1 - edge1;
                target3.y = rayOrigin.y;
                Vector3 target4 = target3 - edge3;
                target4.y = rayOrigin.y;

                // 計算しなおした時用初期化
                _targetList = new List<Vector3>();
                _targetList.Add(target1);
                _targetList.Add(target2);
                _targetList.Add(target3);
                _targetList.Add(target4);


                // 四つの点を外側にずらす(一回だけ読む)
                float offset = 0.5f;
                for (int i = 0; i < _targetList.Count; i++)
                {
                    Vector3 tmp = _targetList[i];
                    tmp += (_targetList[i] - hit.transform.position) * offset;
                    tmp.y = rayOrigin.y;   // y初期化
                    _targetList[i] = tmp;
                    _targetGenerator.GenerateTarget(_targetList[i]);
                }

                //一番近い点を削除
                float min = float.MaxValue;
                int minID = 0;
                for (int i = 0; i < _targetList.Count; i++)
                {
                    if (min > (_targetList[i] - rayOrigin).magnitude)
                    {
                        min = (_targetList[i] - rayOrigin).magnitude;
                        minID = i;
                    }
                }
                _targetList.RemoveAt(minID);
                if (_targetList.Count >= 4)
                {
                    Debug.Log("4以上エラー");
                }

                // 見えないところの要素削除
                for (int i = _targetList.Count - 1; i >= 0; i--)
                {
                    var targetRay = new Ray(rayOrigin, (_targetList[i] - rayOrigin).normalized);
                    RaycastHit targetHit;
                    if (Physics.Raycast(targetRay, out targetHit, (_targetList[i] - rayOrigin).magnitude))
                    {                        
                        if (targetHit.collider.CompareTag("Collision"))
                        {
                            _targetList.RemoveAt(i);
                        }
                    }
                    else
                    {
                        Debug.DrawRay(targetRay.origin, targetRay.direction * (_targetList[i] - rayOrigin).magnitude, Color.blue, 5, false);
                    }
                }

                if (_targetList.Count >= 3)
                {
                    Debug.Log("3以上エラー");
                }
                // 見えている所から目的地を割り出す(ここで2つ以下まで絞れている)
                if (_targetList.Count > 1)
                {
                    float minPathDistance = float.MaxValue;
                    int minPathID = 0;
                    for (int i = 0; i < _targetList.Count; i++)
                    {                        // 目的地までの距離                    // 目的地からプレイヤーまでの距離 
                        float pathDistance = (_targetList[i] - rayOrigin).magnitude + (finalTarget.transform.position - _targetList[i]).magnitude;
                       
                        if (pathDistance <= minPathDistance)
                        {
                            minPathDistance = pathDistance;
                            minPathID = i;
                        }
                    }
                    return _targetList[minPathID];
                }
                else
                {
                    _target = _targetList[0];
                }
                //_targetGenerator.GenerateTarget(_target);
            }
        }
        
        return _target;
    }

}
