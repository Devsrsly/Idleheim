using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public BattleManager battle;
    public TMP_Text playerText;
    public TMP_Text enemyText;
    public TMP_Text goldText;

    public Slider playerHpBar;
    public Slider enemyHpBar;

    private void Start()
    {
        if (playerHpBar)
        {
            playerHpBar.minValue = 0;
            playerHpBar.maxValue = battle.PlayerMaxHp;
        }

        if (enemyHpBar)
        {
            enemyHpBar.minValue = 0;
            enemyHpBar.maxValue = battle.EnemyMaxHp;
        }
    }

    private void Update()
    {
        if (!battle) return;

        playerText.text = $"Player HP: {battle.PlayerHp}/{battle.PlayerMaxHp}";
        enemyText.text = $"Enemy HP: {battle.EnemyHp}/{battle.EnemyMaxHp}";
        goldText.text = $"Gold: {battle.gold}";

        if (playerHpBar)
        {
            playerHpBar.maxValue = battle.PlayerMaxHp;
            playerHpBar.value = battle.PlayerHp;
        }

        if (enemyHpBar)
        {
            enemyHpBar.maxValue = battle.EnemyMaxHp;
            enemyHpBar.value = battle.EnemyHp;
        }
    }
}