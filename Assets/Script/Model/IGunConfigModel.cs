using FrameworkDesign;
using System.Collections.Generic;

namespace ShootingEditor2D
{
    public interface IGunConfigModel : IModel
    {
        GunConfigItem GetItemByName(string name);
    }

    public class GunConfigModel : AbstractModel, IGunConfigModel
    {
        private Dictionary<string, GunConfigItem> mItems = new Dictionary<string, GunConfigItem>()
        {
            { "手枪", new GunConfigItem("手枪", 7, 1, 1, 0.5f, false, 3, "默认强") },
            { "冲锋枪", new GunConfigItem("冲锋枪", 30, 1, 6, 0.34f, true, 3, "无") },
            { "步枪", new GunConfigItem("步枪", 50, 3, 3, 1f, true, 1, "有一定后坐力") },
            { "狙击枪", new GunConfigItem("狙击枪", 12, 6, 1, 1f, true, 5, "红外瞄准+后坐力大") },
            { "火箭筒", new GunConfigItem("火箭筒", 1, 5, 1, 1f, true, 4, "跟踪+爆炸") },
            { "霰弹枪", new GunConfigItem("霰弹枪", 1, 1, 1, 0.5f, true, 1, "一次发射 6 ~ 12 个子弹") },
        };

        public GunConfigItem GetItemByName(string name)
        {
            return mItems.ContainsKey(name) ? mItems[name] : null;
        }

        public override void OnInit()
        {
        }
    }
}