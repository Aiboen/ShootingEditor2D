using FrameworkDesign;
using UnityEngine.SceneManagement;

namespace ShootingEditor2D
{
    public class HurtPlayerCommand : AbstractCommand
    {

        private readonly int mHurt;

        public HurtPlayerCommand(int hurt = 1)
        {
            mHurt = hurt;
        }

        protected override void OnExecute()
        {
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.HP.Value -= mHurt;

            if (playerModel.HP.Value == 0)
            {
                //��ת��ʧ�ܳ���
                SceneManager.LoadScene("GameOver");
            }
        }

    }
}
