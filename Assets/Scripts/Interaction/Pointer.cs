using System.Collections;
using UnityEngine;

public class Pointer : MonoBehaviour
{
	LineRenderer line;
	RaycastHit hit;
	IPointable pointable, prevPointable;
	bool isRunning = true;

	static bool Trigger => OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.One);

	void Start()
	{
		line = GetComponent<LineRenderer>();
	}

	void Update()
	{
		if (isRunning == false) return; //非表示設定の場合、処理しません

		if (Physics.Raycast(transform.position, transform.forward, out hit)) //指し示す先にものがある場合、そこにラインを伸ばします
		{
			line.SetPosition(1, Vector3.Lerp(line.GetPosition(1), transform.InverseTransformPoint(hit.point), 0.5001f));
			pointable = hit.transform.GetComponent<IPointable>();
			if (Trigger) pointable?.Click(); //トリガーを押したらクリック処理を行います
		}
		else //指し示す先に何もなければ、ラインの長さを20cmに固定します
		{
			line.SetPosition(1, Vector3.Lerp(line.GetPosition(1), Vector3.forward * 0.2f, 0.5001f));
			pointable = null;
		}

		if (pointable != prevPointable) //指し示す物が変わった場合に、ポイントした場合、外した場合の処理をそれぞれ呼びます
		{
			prevPointable?.Unpointed();
			pointable?.Pointed();
			prevPointable = pointable;
		}
	}

	/// <summary>
	/// ラインの表示・非表示をなめらかに行います
	/// </summary>
	/// <param name="state">オンオフ</param>
	public void SetRunningState(bool state)
	{
		isRunning = state;
		if (gameObject.activeInHierarchy)
			StartCoroutine(FadeLine(state ? Vector3.forward * 0.2f : Vector3.zero));
	}

	/// <summary>
	/// ラインの長さをなめらかに変えます
	/// </summary>
	/// <param name="target">目標地点</param>
	IEnumerator FadeLine(Vector3 target)
	{
		while (line.GetPosition(1) != target)
		{
			line.SetPosition(1, Vector3.Lerp(line.GetPosition(1), target, 0.5001f));
			yield return null;
		}
	}
}