using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Config")]
    public UnitStats playerStats;
    public UnitStats enemyStats;

    [Header("Runtime")]
    public int gold = 0;

    private UnitRuntime player;
    private UnitRuntime enemy;

    private const string GoldKey = "gold";
    private const string LastTimeKey = "lastTimeUtc";

    private void Start()
    {
        Load();
        ApplyOfflineProgress();

        player = new UnitRuntime(playerStats);
        enemy  = new UnitRuntime(enemyStats);
    }

    private void Update()
    {
        float t = Time.time;

        if (!player.IsDead && !enemy.IsDead)
        {
            if (player.CanAttack(t))
                enemy.TakeDamage(player.DealDamage(t));

            if (enemy.CanAttack(t))
                player.TakeDamage(enemy.DealDamage(t));

            if (enemy.IsDead)
            {
                gold += 10;
                RespawnEnemy();
            }

            if (player.IsDead)
            {
                RespawnPlayer();
            }
        }
    }

    private void RespawnEnemy()
    {
        enemy = new UnitRuntime(enemyStats);
    }

    private void RespawnPlayer()
    {
        player = new UnitRuntime(playerStats);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt(GoldKey, gold);
        PlayerPrefs.SetString(LastTimeKey, System.DateTime.UtcNow.ToString("o"));
        PlayerPrefs.Save();
    }

    private void Load()
    {
        gold = PlayerPrefs.GetInt(GoldKey, 0);
    }

    private void ApplyOfflineProgress()
    {
        var last = PlayerPrefs.GetString(LastTimeKey, "");
        if (string.IsNullOrEmpty(last)) return;

        if (System.DateTime.TryParse(last, null,
            System.Globalization.DateTimeStyles.RoundtripKind,
            out var lastUtc))
        {
            var seconds = (System.DateTime.UtcNow - lastUtc).TotalSeconds;
            if (seconds <= 5) return;

            int earned = Mathf.Clamp((int)seconds, 0, 60 * 60 * 8);
            gold += earned;
        }
    }

    // UI getters
    public int PlayerHp => player?.hp ?? 0;
    public int PlayerMaxHp => player?.stats?.maxHp ?? 0;
    public int EnemyHp => enemy?.hp ?? 0;
    public int EnemyMaxHp => enemy?.stats?.maxHp ?? 0;
}