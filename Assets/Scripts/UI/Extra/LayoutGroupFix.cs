using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LayoutGroupFix : MonoBehaviour
{
	[SerializeField] private bool fixOnStart;
	
    public LayoutGroup _layoutGroup;

	private void Awake()
	{
		_layoutGroup = GetComponent<LayoutGroup>();
		
		if (fixOnStart)
			_ = FixLayout();
	}

	public async UniTask FixLayout()
    {
		await FixingLayout();
	}

	private async UniTask FixingLayout()
	{
		_layoutGroup.enabled = false;
		await UniTask.Delay(20);
		_layoutGroup.enabled = true;
	}
}
