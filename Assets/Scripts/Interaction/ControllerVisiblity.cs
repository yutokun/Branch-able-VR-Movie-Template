using UnityEngine;

/// <summary>
/// コントローラーモデルの表示を切り替えます
/// </summary>
public class ControllerVisiblity : MonoBehaviour
{
	[SerializeField] Material[] materials; //Inspector でコントローラーのマテリアルを指定

	/// <summary>
	/// コントローラーを指定の透明度（alpha）にします
	/// </summary>
	/// <param name="alpha">透明度</param>
	public void ChangeAlpha(float alpha)
	{
		var color = materials[0].color;
		color.a = alpha;
		foreach (var item in materials) Tween.FadeMaterial(this, item, color, 1f);
	}
}