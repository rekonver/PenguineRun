using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public Transform player;
    public float speed = 15f;
    public bool magnetActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Coins") && magnetActive)
        {
            other.transform.SetParent(player);

            StartCoroutine(MoveCoinToPlayer(other.transform));
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Bonus") && magnetActive)
        {
            other.transform.SetParent(player);

            StartCoroutine(MoveCoinToPlayer(other.transform));
        }
    }

    private IEnumerator MoveCoinToPlayer(Transform coin)
    {
        // ����������, �� ��'��� �� � null � �� �� ����
        while (coin != null && Vector3.Distance(coin.position, player.position) > 0.1f)
        {
            if (coin != null)
            {
                coin.position = Vector3.MoveTowards(coin.position, player.position, speed * Time.deltaTime);
            }
            yield return null;
        }

        // ���������� �� �������, ���� ������� ���� ������� �� ���������� ����
        if (coin != null)
        {
            coin.position = player.position;
        }
    }
}
