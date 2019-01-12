using UnityEngine;

public class ControllerVisiblity : MonoBehaviour
{
	[SerializeField] Material[] materials;

	public void ChangeAlpha(float alpha)
	{
		var color = materials[0].color;
		color.a = alpha;
		foreach (var item in materials) Tween.FadeMaterial(this, item, color, 1f);
	}
}