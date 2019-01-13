using UnityEngine;

public class ControllerVisiblity : MonoBehaviour
{
	[SerializeField] Material[] materials;

	public void ChangeAlpha(float alpha)
	{
		var color = materials[0].color;
		color.a = alpha;
		foreach (var item in materials) Tween.FadeMaterial(item, color, 1f);
	}

#if UNITY_EDITOR
	Color initialColor;

	void Awake()
	{
		initialColor = materials[0].color;
	}

	void OnApplicationQuit()
	{
		foreach (var material in materials) material.color = initialColor;
	}
#endif
}