using UnityEngine;

namespace LMS.Data
{
    [System.Serializable]
    public class PlayerData
    {
        // Player Money
        public int money;
        // Clear Count
        public int count;
        // Player Ability
        public int[] level = new int[3];
        // Sound Volume
        public float[] volumes = new float[2];
        public float[] saveVolumes = new float[2];
        public bool[] isMute = new bool[2];

        public PlayerData(int money, int count, int[] level, float[] volumes, float[] saveVolumes, bool[] isMute)
        {
            this.money = money;
            this.count = count;
            this.level = level;
            this.volumes = volumes;
            this.saveVolumes = saveVolumes;
            this.isMute = isMute;
        }

        public PlayerData()
        {
            money = 0;
            count = 0;
            for (int i = 0; i < 3; i++)
            {
                level[i] = -1;
                if (i > 1) continue;
                volumes[i] = 1f;
                saveVolumes[i] = 1f;
                isMute[i] = false;
            }
        }
    }

}
