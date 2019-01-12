using UnityEngine;

public class TweenBase : MonoBehaviour
{
	public static TweenBase instance;

	void Awake()
	{
		instance = this;
	}
}