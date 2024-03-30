namespace ShootingEditor2D
{
    public class GunConfigItem
    {
        public GunConfigItem(string name, int bulletMaxCount, float attack, float frequency, float shootDistance,
            bool needBullet, float reloadSeconds, string description)
        {
            Name = name;
            BulletMaxCount = bulletMaxCount;
            Attack = attack;
            Frequency = frequency;
            ShootDistance = shootDistance;
            NeedBullet = needBullet;
            ReloadSeconds = reloadSeconds;
            Description = description;
        }

        public string Name { get; set; }
        public int BulletMaxCount { get; set; }
        public float Attack { get; set; }
        public float Frequency { get; set; }
        public float ShootDistance { get; set; }
        public bool NeedBullet { get; set; }
        public float ReloadSeconds { get; set; }
        public string Description { get; set; }
    }
}