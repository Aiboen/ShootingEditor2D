﻿using FrameworkDesign;

namespace ShootingEditor2D
{
    public class PickCommand : AbstractCommand
    {
        private readonly string mName;
        private readonly int mBulletInGun;
        private readonly int mBulletOutGun;

        public PickCommand(string name, int bulletInGun, int bulletOutGun)
        {
            mName = name;
            mBulletInGun = bulletInGun;
            mBulletOutGun = bulletOutGun;
        }

        protected override void OnExecute()
        {
            this.GetSystem<IGunSystem>().PickGun(mName, mBulletInGun, mBulletOutGun);
        }
    }
}