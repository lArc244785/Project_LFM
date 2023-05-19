using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AdditionUpgrade : MonoBehaviour
{
	private Button m_buttonHp;
	private Button m_buttonSpeed;
	private Button m_buttonDamge;
	private Button m_buttonBullet;
	private Button m_buttonReload;

	private TextMeshProUGUI m_textHp;
	private TextMeshProUGUI m_textSpeed;
	private TextMeshProUGUI m_textDamge;
	private TextMeshProUGUI m_textReload;
	private TextMeshProUGUI m_textBullet;

	private AdditionalHealth m_playerHealth;
	private AdditionalSpeed m_playerSpeed;
	private AdditionalDamage m_playerDamage;
	private AdditionalGun m_playerGun;

	[SerializeField]
	private int[] m_healths;
	private int m_healthUpgrad;

	[SerializeField]
	private float[] m_speeds;
	private int m_speedUpgrad;

	[SerializeField]
	private int[] m_damages;
	private int m_damageUpgrad;

	[SerializeField]
	private int[] m_bullets;
	private int m_bulletUpgrad;

	[SerializeField]
	private float[] m_reloads;
	private int m_reloadUpgrad;

	private void Start()
	{
		Transform hp = transform.Find("HP");
		m_textHp = hp.Find("Text").GetComponent<TextMeshProUGUI>();
		m_buttonHp = hp.Find("Button").GetComponent<Button>();

		Transform damage = transform.Find("Damage");
		m_textDamge = damage.Find("Text").GetComponent<TextMeshProUGUI>();
		m_buttonDamge = damage.Find("Button").GetComponent<Button>();

		Transform speed = transform.Find("Speed");
		m_textSpeed = speed.Find("Text").GetComponent<TextMeshProUGUI>();
		m_buttonSpeed = speed.Find("Button").GetComponent<Button>();

		Transform bullet = transform.Find("Bullet");
		m_textBullet = bullet.Find("Text").GetComponent<TextMeshProUGUI>();
		m_buttonBullet = bullet.Find("Button").GetComponent<Button>();

		Transform reload = transform.Find("Reload");
		m_textReload = reload.Find("Text").GetComponent<TextMeshProUGUI>();
		m_buttonReload = reload.Find("Button").GetComponent<Button>();

		var findObject = "Player";
		var weaponManger = GameObject.Find(findObject).GetComponent<WeaponManager>();
		m_playerHealth = GameObject.Find(findObject).GetComponent<Health>().AdditionalHealth;
		m_playerSpeed = GameObject.Find(findObject).GetComponent<PlayerMovement>().AdditionalSpeed;
		m_playerDamage = weaponManger.AdditionalDamage;
		m_playerGun = weaponManger.AdditionalGun;

		m_healthUpgrad = 0;
		m_speedUpgrad = 0;
		m_damageUpgrad = 0;
		m_bulletUpgrad = 0;
		m_reloadUpgrad = 0;

		m_buttonHp.onClick.AddListener(OnHpUpgrad);
		m_buttonSpeed.onClick.AddListener(OnSpeedUpgrad);
		m_buttonDamge.onClick.AddListener(OnDamageUpgrad);
		m_buttonBullet.onClick.AddListener(OnBulletUpgrad);
		m_buttonReload.onClick.AddListener(OnReloadUpgrad);

		DrawHp();
		DrawSpeed();
		DrawDamage();
		DrawBullet();
		DrawReload();
	}

	private void OnHpUpgrad()
	{
		m_healthUpgrad++;
		m_healthUpgrad = Mathf.Clamp(m_healthUpgrad, 0, m_healths.Length - 1);
		m_playerHealth.SetHp(m_healths[m_healthUpgrad]);
		DrawHp();
	}
	private void OnSpeedUpgrad()
	{
		m_speedUpgrad++;
		m_speedUpgrad = Mathf.Clamp(m_speedUpgrad, 0, m_speeds.Length - 1);
		m_playerSpeed.SetSpeed(m_speeds[m_speedUpgrad]);
		DrawSpeed();
	}
	private void OnDamageUpgrad()
	{
		m_damageUpgrad++;
		m_damageUpgrad = Mathf.Clamp(m_damageUpgrad, 0, m_damages.Length - 1);
		m_playerDamage.SetDamge(m_damages[m_damageUpgrad]);
		DrawDamage();
	}
	private void OnBulletUpgrad()
	{
		m_bulletUpgrad++;
		m_bulletUpgrad = Mathf.Clamp(m_bulletUpgrad, 0, m_bullets.Length - 1);
		m_playerGun.SetBullet(m_bullets[m_bulletUpgrad]);
		DrawBullet();
	}
	private void OnReloadUpgrad()
	{
		m_reloadUpgrad++;
		m_reloadUpgrad = Mathf.Clamp(m_reloadUpgrad, 0, m_reloads.Length - 1);
		m_playerGun.SetReloadCoolTime(m_reloads[m_reloadUpgrad]);
		DrawReload();
	}

	private void DrawHp()
	{
		m_textHp.text = $"{m_healthUpgrad} T {m_healths[m_healthUpgrad]} HP UP";
	}

	private void DrawSpeed()
	{
		m_textSpeed.text = $"{m_speedUpgrad} T {m_speeds[m_speedUpgrad]} Speed UP";
	}
	private void DrawDamage()
	{
		m_textDamge.text = $"{m_damageUpgrad} T {m_damages[m_damageUpgrad]} Damage Up";
	}
	private void DrawBullet()
	{
		m_textBullet.text = $"{m_bulletUpgrad} T {m_bullets[m_bulletUpgrad]} Bullet Up";
	}
	private void DrawReload()
	{
		m_textReload.text = $"{m_reloadUpgrad} T {m_reloads[m_reloadUpgrad] * 10.0f}% Reload Cool";
	}
}
