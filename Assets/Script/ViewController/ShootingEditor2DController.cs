using FrameworkDesign;
using UnityEngine;

namespace ShootingEditor2D
{
    /// <summary>
    /// Controller 父类
    /// </summary>
    public class ShootingEditor2DController : MonoBehaviour, IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return ShootingEditor2D.Interface;
        }
    }
}