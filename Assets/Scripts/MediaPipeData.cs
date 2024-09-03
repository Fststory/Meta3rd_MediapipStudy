using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoseName
{
    nose,
    left_eye_inner,
    left_eye,
    left_eye_outer,
    right_eye_inner,
    right_eye,
    right_eye_outer,
    left_ear,
    right_ear,
    mouth_left,
    mouth_right,
    left_shoulder,
    right_shoulder,
    left_elbow,
    right_elbow,
    left_wrist,
    right_wrist,
    left_pinky,
    right_pinky,
    left_index,
    right_index,
    left_thumb,
    right_thumb,
    left_hip,
    right_hip,
    left_knee,
    right_knee,
    left_ankle,
    right_ankle,
    left_heel,
    right_heel,
    left_foot_index,
    right_foot_index,

    pose_max    // 이 녀석이 enum 안에 몇 개가 있는지 표시하는 역할임(책갈피 같은 느낌)
}

public class MediaPipeData : MonoBehaviour
{
    // point prefab
    public GameObject pointFactory;

    // UDPServer
    public UDPServer udpServer;

    // 모든 point 담을 변수
    public Transform[] allPoints;

    void Start()
    {
        // allPoints 를 몇개 담을지
        allPoints = new Transform[(int)PoseName.pose_max];

        // point 를 33개(landMark의 개수) 만들자
        for (int i = 0; i < (int)PoseName.pose_max; i++)
        {
            GameObject point = Instantiate(pointFactory);
            // 만들어진 point 를 나의 자식으로 하자
            point.transform.parent = transform;
            // 만들어진 point 의 이름을 해당되는 PoseName 으로 변경
            point.name = ((PoseName)i).ToString();  // i를 posename으로 형변환 -> 그걸 또 스트링으로 형변환
            // 만들어진 point 의 transform 을 allPoints 에 담자
            allPoints[i] = point.transform;
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    for (int i = 0; i < (int)PoseName.pose_max; i++)
        //    {
        //        allPoints[i].parent = null;   // 부모가 널이면 빠져 나온다
        //    }
        //}

        // 모든 LandMarkData 를 담을 변수
        LandMarkData landMark = udpServer.landmark; // udpServer가 갖고 오는 것을 계속해서 넣어줘야 된다.

        // 데이터가 들어있다면
        if (landMark.data.Count == (int)PoseName.pose_max)
        {
            // landMark 의 위치값을 point 의 위치값으로 설정
            for(int i = 0; i < allPoints.Length; i++)
            {
                Vector3 pos = new Vector3(landMark.data[i].x, -landMark.data[i].y, landMark.data[i].z);
                allPoints[i].position = pos;
            }
        }
    }
}
