using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FpsDisplay : MonoBehaviour
{
	private float m_LastUpdateShowTime = 0f;    //��һ�θ���֡�ʵ�ʱ��;

	private float m_UpdateShowDeltaTime = 0.01f;//����֡�ʵ�ʱ����;

	private int m_FrameUpdate = 0;//֡��;

	private float m_FPS = 0;

	public Text text;

	void Awake()
	{
		Application.targetFrameRate = 144;
	}

	// Use this for initialization
	void Start()
	{
		m_LastUpdateShowTime = Time.realtimeSinceStartup;
	}

	// Update is called once per frame
	void Update()
	{
		m_FrameUpdate++;
		if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime)
		{
			m_FPS = m_FrameUpdate / (Time.realtimeSinceStartup - m_LastUpdateShowTime);
			m_FrameUpdate = 0;
			m_LastUpdateShowTime = Time.realtimeSinceStartup;
		}
		text.text = "FPS: " + m_FPS.ToString("0.0");
	}

}