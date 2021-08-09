using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class shakeCamera : MonoBehaviour
{

	public float shakeLevel = 3f;// �𶯷���
	public float setShakeTime = 0.5f;   // ��ʱ��
	public float shakeFps = 45f;    // �𶯵�FPS

	private bool isshakeCamera = false;// �𶯱�־
	private float fps;
	private float shakeTime = 0.0f;
	private float frameTime = 0.0f;
	private float shakeDelta = 2000f;
	private Camera selfCamera;

	void OnEnable()
	{
		isshakeCamera = true;
		selfCamera = gameObject.GetComponent<Camera>();
		shakeTime = setShakeTime;
		fps = shakeFps;
		frameTime = 0.03f;
		shakeDelta = 0.005f;
	}

	void OnDisable()
	{
		isshakeCamera = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (isshakeCamera)
		{
			if (shakeTime > 0)
			{
				shakeTime -= Time.deltaTime;
				if (shakeTime <= 0)
				{
					enabled = false;
				}
				else
				{
					frameTime += Time.deltaTime;

					if (frameTime > 1.0 / fps)
					{
						frameTime = 0;
						
						selfCamera.transform.position = constant.CAM_START_POS + new Vector3( shakeDelta * (shakeLevel * Random.Range(-1, 1)), shakeDelta * (shakeLevel * Random.Range(-1, 1)));

						//selfCamera.rect = new Rect(0.25f+ shakeDelta * (-1.0f + shakeLevel * Random.value),0.25f + shakeDelta * (-1.0f + shakeLevel * Random.value), 0.5f, 0.5f);
					}
				}
			}
		}
	}
}