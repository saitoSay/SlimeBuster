using UnityEngine;

/// <summary>攻撃判定のオブジェクトにアタッチするクラス</summary>
public class WeaponCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().Damage(PlayerManager.Instance.GetPlayer().AttackPower);
        }
    }
}
