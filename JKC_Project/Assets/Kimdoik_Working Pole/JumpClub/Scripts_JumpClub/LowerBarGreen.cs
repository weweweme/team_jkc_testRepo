using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBarGreen : MonoBehaviour
{
    private Rigidbody _LowerBarGreenRigidbody;
    public float rotationSpeed = 10f;
    public float acceleration = 1f;

    void Awake()
    {
        _LowerBarGreenRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        StartCoroutine(IncreaseRotationSpeed());
    }

    void Update()
    {
        // ȸ�� �ӵ��� ����
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    IEnumerator IncreaseRotationSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1�ʸ��� ȸ�� �ӵ��� ������Ŵ
            rotationSpeed += acceleration;
        }
    }
}