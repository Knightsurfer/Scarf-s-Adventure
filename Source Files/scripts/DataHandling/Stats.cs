using System;

namespace DataHandling
{
    [Serializable] class PlayerData
    {
        public int[] items = new int[2];

        public int[] health = new int[4];
        public int[] level = new int[4];

        public float[] positionFloat = new float[12];
        public float[] rotationFloat = new float[16];

        public float[] currentYaw = new float[4];
    }
}
