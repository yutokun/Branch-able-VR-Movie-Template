using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
	[SerializeField] GameObject start, end, credits;

	void Awake()
	{
		start.SetActive(true);
		end.SetActive(true);
		credits.SetActive(true);
	}
}