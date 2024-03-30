﻿using FrameworkDesign;

namespace ShootingEditor2D
{
    public interface IGunSystem : ISystem
    {
        GunInfo CurrentGun { get; }
    }

    public class GunSystem : AbstractSystem, IGunSystem
    {
        /// <summary>
        /// 枪械信息
        /// </summary>
        public GunInfo CurrentGun { get; } = new GunInfo()
        {
            BulletCountInGun = new BindableProperty<int>()
            {
                Value = 3
            },
            BulletCountOutGun = new BindableProperty<int>() // +
            {
                Value = 1
            },
            Name = new BindableProperty<string>() // +
            {
                //Value = "手枪"
            },

            GunState = new BindableProperty<GunState>() // +
            {
                Value = GunState.Idle
            }
        };

        public override void OnInit()
        {
        }
    }
}