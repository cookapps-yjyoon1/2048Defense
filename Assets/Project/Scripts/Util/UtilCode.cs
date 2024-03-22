using UnityEngine;

public static class UtilCode
{
    public static bool GetChance(float chance)
    {
        if (chance <= 0) return false;
        if (chance >= 100) return true;

        return chance >= Random.Range(0, 100);
    }
    
    public static int GetWeightChance(float[] chance)
    {
        float sum = 0;
        
        for(int i = 0 ; i < chance.Length ; i++)
        {
            sum += chance[i];
        }

        float randomFloat = Random.Range(0, sum);
        
        sum = 0;

        for (var i = 0; i < chance.Length; i++)
        {
            sum += chance[i];
    
            if (randomFloat <= sum)
            {
                return i;
            }
        }

        return 0;
    }
    
    public static string GetTimeFormat(long time)
    {
        long hour = time / 3600;
        long minute = time / 60;
        long second = time % 60;

        return $"{hour:D2}:{minute:D2}:{second:D2}";
    }
}