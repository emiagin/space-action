using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthSpriteLevel
{
	public Sprite sprite;
	public int level;
}
public class HpView : MonoBehaviour
{
	[SerializeField]
	private HealthSpriteLevel[] healthSpriteLevels;

	private SpriteRenderer spriteRenderer;

	public void Init(int health)
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		SetHealthSprite(health);
	}

	public void SetHealthSprite(int health)
	{
		if (health == healthSpriteLevels[0].level)
			spriteRenderer.sprite = healthSpriteLevels[0].sprite;
		else
			for (int i = 1; i < healthSpriteLevels.Length; i++)
			{
				if (health < healthSpriteLevels[i - 1].level && health >= healthSpriteLevels[i].level)
				{
					spriteRenderer.sprite = healthSpriteLevels[i].sprite;
					break;
				}
			}
	}
}
