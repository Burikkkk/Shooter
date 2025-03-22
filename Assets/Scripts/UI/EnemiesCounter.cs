using TMPro;
using UnityEngine;

public static class EnemiesCounter
{
    private static TMP_Text skeletonsCounter;
    private static TMP_Text piratesCounter;
    private static int skeletonsAmount;
    private static int piratesAmount;

    public static void Initialize(TMP_Text skeletons, TMP_Text pirates, int skeletonsStart, int piratesStart)
    {
        skeletonsCounter = skeletons;
        piratesCounter = pirates;
        skeletonsAmount = skeletonsStart;
        piratesAmount = piratesStart;

        UpdateUI();
    }

    private static void UpdateUI()
    {
        if (skeletonsCounter != null)
            skeletonsCounter.text = skeletonsAmount.ToString();

        if (piratesCounter != null)
            piratesCounter.text = piratesAmount.ToString();
    }

    public static void DecreaseEnemy(string enemyType)
    {
        if (enemyType == "Skeleton" && skeletonsAmount > 0)
            skeletonsAmount--;
        else if (enemyType == "Pirate" && piratesAmount > 0)
            piratesAmount--;

        UpdateUI();
    }

    public static bool CheckWin()
    {
        return skeletonsAmount == 0 && piratesAmount == 0;
    }
}
