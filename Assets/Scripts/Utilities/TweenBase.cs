using UnityEngine;

public class TweenBase : MonoBehaviour
{
	public static TweenBase Instance { get; private set; }

	void Awake()
	{
		Instance = this;
	}
}