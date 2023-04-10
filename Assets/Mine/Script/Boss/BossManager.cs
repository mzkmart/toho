using UnityEngine;
/// <summary>
/// �{�X�̊Ǘ�������N���X
/// </summary>
public class BossManager : MonoBehaviour
{
    // �����E�v�[�����O����I�u�W�F�N�g
    [SerializeField] private GameObject bossObjectPrefab = default;

    // �������ꂽ�I�u�W�F�N�g
    private GameObject bossObject = default;

    private void Awake()
    {
        // �v���C��ʊO�ɐ��� �i�[
        bossObject = Instantiate(bossObjectPrefab, new Vector2(30, 30), Quaternion.identity);

        // �v���C���[�̒e�����̃I�u�W�F�N�g�̎q�ɐݒ�
        bossObject.transform.parent = this.transform;

        // �I�u�W�F�N�g���\����
        bossObject.SetActive(false);

        // �I�u�W�F�N�g�̖��O��ݒ�
        bossObject.name = "BossObject";
    }

    /// <summary>
    /// �G�̐������\�b�h
    /// </summary>
    public void ActiveBoss(BossData newBossData)
    {
        // �{�X���A�N�e�B�u�ɂ��ď���n��
        bossObject.SetActive(true);
        bossObject.GetComponent<BossEnemy>().ActiveInitLoad(newBossData);
    }
}