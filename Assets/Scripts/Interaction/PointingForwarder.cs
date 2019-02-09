using UnityEngine;

public class PointingForwarder : MonoBehaviour, IPointable
{
	[SerializeField] MonoBehaviour target;
	IPointable pointable;

	void Awake()
	{
		pointable = target as IPointable;
		if (pointable == null)
		{
			Debug.LogError("target が IPointable ではありません。対象を確認して下さい。");
			Destroy(this);
		}
	}

	public void Pointed()
	{
		pointable.Pointed();
	}

	public void UnPointed()
	{
		pointable.UnPointed();
	}

	public void Click()
	{
		pointable.Click();
	}
}