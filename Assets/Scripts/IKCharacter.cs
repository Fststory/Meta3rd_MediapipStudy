using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKCharacter : MonoBehaviour
{
    // Point �� ���� (PointCharacter)
    public MediaPipeData data;

    // Rig �� Target (������)
    public Transform rigRightHandTarget;

    // Animator
    Animator anim;

    // ������ ��� to ������ �Ȳ�ġ �� �Ÿ�
    float rightShoulderToElbow;
    // ������ �Ȳ�ġ to ������ �ո� �� �Ÿ�
    float rightElbowToWrist;

    void Start()
    {
        // Animator ��������
        anim = GetComponent<Animator>();

        // ������ ��� to ������ �Ȳ�ġ �� �Ÿ� ������
        Vector3 rightShoulder = anim.GetBoneTransform(HumanBodyBones.RightUpperArm).position;
        Vector3 rightElbow = anim.GetBoneTransform(HumanBodyBones.RightLowerArm).position;
        rightShoulderToElbow = Vector3.Distance(rightShoulder, rightElbow);
        // ������ �Ȳ�ġ to ������ �ո��� �Ÿ��� ������
        Vector3 rightWrist = anim.GetBoneTransform(HumanBodyBones.RightHand).position;
        rightElbowToWrist = Vector3.Distance(rightElbow, rightWrist);
    }

    void Update()
    {
        SetRightHand();
    }

    void SetRightHand()
    {
        // �̵�� ������ ������ (������ ��� ----> ������ �Ȳ�ġ ����)
        Vector3 dirShoulderToElbow =
            data.allPoints[(int)PoseName.right_elbow].position -
            data.allPoints[(int)PoseName.right_shoulder].position;

        // ������ �Ȳ�ġ�� ���ϴ� ����ȭ�� ���� * ĳ������ �Ȳ�ġ������ �Ÿ�
        dirShoulderToElbow = dirShoulderToElbow.normalized * rightShoulderToElbow;

        // �̵�� ������ ������ (������ �Ȳ�ġ ----> ������ �ո� ����)
        Vector3 dirElbowToWrist =
            data.allPoints[(int)PoseName.right_wrist].position -
            data.allPoints[(int)PoseName.right_elbow].position;

        // ������ �ո��� ���ϴ� ����ȭ�� ���� * ĳ������ �ո������ �Ÿ�
        dirElbowToWrist = dirElbowToWrist.normalized * rightElbowToWrist;

        // dirShoulderToElbow + dirElbowToWrist ������ ĳ������ ������ ������� ������.
        Vector3 targetPos = anim.GetBoneTransform(HumanBodyBones.RightUpperArm).position +
            dirShoulderToElbow +
            dirElbowToWrist;

        // rig�� ������ �� target �� ��ġ�� targetPos �� ����
        rigRightHandTarget.position = targetPos;
    }
}
