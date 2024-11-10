using UnityEngine;

public class getPoint : MonoBehaviour
{
    // Sphereの位置を更新するメソッド
    public void UpdatePosition(float x, float y, float z)
    {
        // 受信した座標に基づいてSphereの位置を更新
        Vector3 newPosition = new Vector3(x, -y, -1);  // 左右、上下の動きが反転している場合、符号を反転
        transform.position = newPosition;
    }
}
