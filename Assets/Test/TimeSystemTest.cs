using UnityEngine;

namespace ShootingEditor2D.Test
{
    public class TimeSystemTest : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            Debug.Log(Time.deltaTime);

            ShootingEditor2D.Interface.GetSystem<ITimeSystem>().AddDelayTask(3, () => { Debug.Log(Time.time); });
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}