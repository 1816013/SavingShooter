using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TagetStatas
{
    public Vector3 _pos;
    public bool _player;
}

public class Pathfinding : MonoBehaviour
{
    private GameObject finalTarget; // 最終目標;       
    private List<Vector3> _targetList = new List<Vector3>();
    private TagetStatas targetStatas;  // 近くのターゲット
    private Vector3[] _edges;
    private Vector3 _boxColliderSize;

    private void Awake()
    { 
        // finaltargetはこのゲームではプレイヤーのみなのでここに書く
        finalTarget = GameObject.FindGameObjectWithTag("Player");     
    }

    // Update is called once per frame
    public TagetStatas PathFind(Vector3 pos, float radius)
    {
        Init(pos);
        RaycastHit hit = new RaycastHit();
        if (FinalTargetRayCast(pos, ref hit, radius))
        {
            targetStatas._player = true;
            targetStatas._pos = hit.transform.position;
            // 二次元にするためにyを調整
            targetStatas._pos.y = pos.y;
           
            return targetStatas;
        }
        else
        {
            if(hit.collider == null)
            {
                //レイが当たってなかったら再計算
                targetStatas._pos = pos;
                return targetStatas;
            }
            targetStatas._player = false;
            SetTargetList(radius * 2f, pos,ref hit);  // 2.5はキャラの半径にかけて直径+aにする

            //一番近い点を削除
            float min = float.MaxValue;
            int minID = 0;
            for (int i = 0; i < _targetList.Count; i++)
            {
                if (min > (_targetList[i] - pos).magnitude)
                {
                    min = (_targetList[i] - pos).magnitude;
                    minID = i;
                }
            }
           
            _targetList.RemoveAt(minID);
            if (_targetList.Count >= 4)
            {
                Debug.Log("削除失敗");
            }
            // 見えないところの要素削除
            for (int i = _targetList.Count - 1; i >= 0; i--)
            {
                var targetRay = new Ray(pos, (_targetList[i] - pos).normalized);
                RaycastHit targetHit;
                if (Physics.SphereCast(targetRay,radius, out targetHit, (_targetList[i] - pos).magnitude))
                {
                    
                    if (targetHit.collider.CompareTag("Collision"))
                    {             
                        _targetList.RemoveAt(i);
                    }
                }
                else
                {
               //     Debug.DrawRay(targetRay.origin, targetRay.direction * (_targetList[i] - pos).magnitude, Color.blue, 5, false);
                }
            }
            if (_targetList.Count == 0)
            {
                Debug.Log("削除しすぎ");
            }

            // 見えている所から目的地を割り出す
            if (_targetList.Count > 1)
            {
                float minPathDistance = float.MaxValue;
                int minPathID = 0;
                for (int i = 0; i < _targetList.Count; i++)
                {                        // 目的地までの距離                    // 目的地からプレイヤーまでの距離 
                    float pathDistance = /*(_targetList[i] - pos).magnitude + */(finalTarget.transform.position - _targetList[i]).magnitude;

                    if (pathDistance <= minPathDistance)
                    {
                        minPathDistance = pathDistance;
                        minPathID = i;
                    }
                }

                targetStatas._pos = _targetList[minPathID];
                return targetStatas;
            }
            else
            {
                // 再計算
                targetStatas._pos = pos;
            }
            
        }
               
        return targetStatas;
    }
    private void Init(Vector3 rayOrigin)
    {
        _targetList = new List<Vector3>();
        _edges =  new Vector3[0];
        targetStatas = new TagetStatas();
        _boxColliderSize = Vector3.zero;
    }
    // 最終目標(プレイヤー)に当たっているか
    public bool FinalTargetRayCast(Vector3 rayOrigin, ref RaycastHit hit, float radius)
    {
        var ray = new Ray(rayOrigin, (finalTarget.transform.position - rayOrigin).normalized);
       
        if (Physics.SphereCast(ray, radius , out hit, (finalTarget.transform.position - rayOrigin).magnitude))
        {
           // Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.blue, 5, false);
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
            if (hit.collider.CompareTag("Collision"))
            {
                _boxColliderSize = hit.transform.gameObject.GetComponent<BoxCollider>().size;               
            }
        }
        return false;
    }
    // 四つの頂点をターゲットリストに登録
    private void SetTargetList(float offset, Vector3 pos, ref RaycastHit hit)
    {
        // 固有ベクトルは単位ベクトルを使用
        // eageは1はx軸2はy軸3はy軸を表す(角度が0の時)
        Vector3 edge1 = hit.transform.localToWorldMatrix.MultiplyVector(new Vector3(1, 0, 0));
        Vector3 edge2 = hit.transform.localToWorldMatrix.MultiplyVector(new Vector3(0, 1, 0));
        Vector3 edge3 = hit.transform.localToWorldMatrix.MultiplyVector(new Vector3(0, 0, 1));

        edge1 *= _boxColliderSize.x;
        edge2 *= _boxColliderSize.y;
        edge3 *= _boxColliderSize.z;

        // 四つの頂点にターゲットを設定
        Vector3 target1 = hit.transform.position + (edge1 * 0.5f) + (edge2 * 0.5f) + (edge3 * 0.5f);
        Vector3 target2 = target1 - edge3;
        Vector3 target3 = target1 - edge1;
        Vector3 target4 = target3 - edge3;

        _targetList.Add(target1);
        _targetList.Add(target2);
        _targetList.Add(target3);
        _targetList.Add(target4);


        // 四つの点を外側にずらす
        for (int i = 0; i < _targetList.Count; i++)
        {
            Vector3 tmp = _targetList[i];
            tmp += (_targetList[i] - hit.transform.position) * offset;
            tmp.y = pos.y;   // y初期化
            _targetList[i] = tmp;
        }
    }
}
