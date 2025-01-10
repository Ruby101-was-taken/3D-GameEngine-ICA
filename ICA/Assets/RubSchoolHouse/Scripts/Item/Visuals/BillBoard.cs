using UnityEngine;

namespace RUB {
    public class BillBoard : MonoBehaviour {
        // Update is called once per frame
        void Update() {
            transform.rotation = Camera.main.transform.rotation; // https://discussions.unity.com/t/how-i-can-create-an-sprite-that-always-look-at-the-camera/16891/4 - 7/1/2025
        }
    }
}
