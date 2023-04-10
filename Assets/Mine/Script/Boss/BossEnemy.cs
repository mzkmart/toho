using System.Collections;
using UnityEngine;

/// <summary>
/// �G�̃N���X
/// </summary>
public class BossEnemy : EnemyBase
{
    // �{�X�̃f�[�^
    BossData bossData;

    // �X�y���J�[�h�����o����N���X
    //SpellCardEffect spellCardEffect;

    // �{�X�̐퓬�O�̒�ʒu
    private const float STANDBY_POSITION_X = 0f;
    private const float STANDBY_POSITION_Y = 4f;

    private const float APPEARD_POSITION_X = 0f;
    private const float APPEARD_POSITION_Y = 8f;

    // �{�X�̏�Ԃ̗�
    enum BossState
    {
        Idle,
        Appear,
        Battle,
        Defeat
    }

    // ���ˊԊu���v�邽�߂̕ϐ�
    private float shotCountTime = default;

    // �o������Lerp�����̕⊮�ʒu�i0�`1�j
    //private float leapNowLocation = default;

    // �E�F�[�u�ԍ�
    private int nowWeveNumber = default;

    // �{�X���o��������
    private bool isBossAppeard = default;

    // �ˌ��������邩
    private bool canBossShot = default;

    // ���݂̃{�X�̏��
    [SerializeField] private BossState nowBossState = BossState.Idle;

    Vector2 currentVelocity = default;

    private void FixedUpdate()
    {
        switch (nowBossState)
        {
            case BossState.Idle:

                // �ҋ@���͉������Ȃ�
                return;

            case BossState.Appear:

                // �o�����̏���
                AppearUpdate();
                break;

            case BossState.Battle:

                // �퓬���̏���
                BattleUpdate();
                break;

            case BossState.Defeat:

                // ���j��̏���
                break;
        }
    }

    /// <summary>
    /// �o���E�퓬�ҋ@���̏���
    /// </summary>
    private void AppearUpdate()
    {
        // �A�j���K���ׂ̈ɉ��œ�����
        base.EnemyMove((int)EnemyMovePattern.Pattern.Stay, speed, appeardTime);

        // �X�^���o�C�ʒu�ֈړ�
        if (!isBossAppeard)
        {
            Vector2 currentPos = this.transform.position;


            this.transform.position = currentPos;

            // �ړ�������Ƀt���O��ݒ�
            if (Mathf.Abs(currentVelocity.x) < 0.01f)
            {
                isBossAppeard = true;
            }
        }

        if (isBossAppeard)
        {
            //spellCardEffect = this.GetComponent<SpellCardEffect>();

            StartCoroutine(ChangeWaveEffect());

            // �ˌ�������
            canBossShot = true;

            // �퓬�ֈڍs
            nowBossState = BossState.Battle;
        }
    }

    /// <summary>
    /// �퓬���̏���
    /// </summary>
    private void BattleUpdate()
    {
        if (hp <= 0)
        {
            return;
        }

        // �o������̎��Ԃ̉��Z
        appeardTime += Time.deltaTime;

        // �G�̈ړ�
        base.EnemyMove(movePattern, speed, appeardTime);


        // �G�̓����蔻��
        int hitCount = 0;

        if (hitCount > 0)
        {
            LifeCalcurate(hitCount);
        }
        // �ˌ��f�[�^���Ȃ��ꍇ�I��
        if (shotData == null)
        {
            return;
        }

        // �G�̎ˌ�
        shotCountTime += Time.deltaTime;

        if (bulletShotInterval < shotCountTime &&
            canBossShot)
        {
            base.EnemyShot(shotData, enemyBulletData, shotUpdateCount);

            shotUpdateCount++;


            shotCountTime = 0;
        }
    }

    /// <summary>
    /// HP�̌v�Z�Ƃ���ɉ��������������郁�\�b�h
    /// </summary>
    private void LifeCalcurate(int hitCount)
    {
        // ����HP��0�ȉ��̏ꍇ�͏I���
        if (hp <= 0)
        {
            return;
        }

        // HP�����Z
        while (hitCount > 0)
        {
            hp--;
            hitCount--;
        }

        // HP��0�ȉ��ɂȂ����ꍇ�Ɉȍ~�̏������s��
        if (hp > 0)
        {
            return;
        }


        // ���̃E�F�[�u�ֈڍs����
        nowWeveNumber++;

        // �����Ȃ��ꍇ���j�����
        if (bossData.bossWaveData.Count <= nowWeveNumber)
        {
            // �ˌ��𒆎~
            canBossShot = false;

            // ���j��Ԃ֑J�ځA���o�R���[�`�����N��
            nowBossState = BossState.Defeat;
            StartCoroutine(BossDefeatEffect());
            return;
        }

        // ���E�F�[�u��ǂݍ��݁E���o
        StartCoroutine(ChangeWaveEffect());
    }

    /// <summary>
    /// �}�l�[�W���[����L���������ۂ̃f�[�^��K�����郁�\�b�h
    /// </summary>
    public void ActiveInitLoad(BossData newBossData)
    {
        // �G�̃E�F�[�u�Ǘ��f�[�^���擾
        bossData = newBossData;

        // �E�F�[�u�ԍ���������
        nowWeveNumber = 0;

        // ���݂̃A�j���[�V�����p�Ɏg�p����摜�f�[�^�z���������
        loadSpriteArray = SpriteArray.idle;

        offset = bossData.Offset;

        // �ʒu��ݒ�
        this.transform.position = new Vector2(STANDBY_POSITION_X, STANDBY_POSITION_Y);

        // �A�j���[�V�����p�̉摜
        idleImagePattern = bossData.BossImageIdle;
        rightImagePattern = bossData.BossImageRightMove;

        // �ҋ@��Ԃ���ڍs
        nowBossState = BossState.Appear;
    }

    /// <summary>
    /// �E�F�[�u�J�n�E�ύX���̃f�[�^��K�����郁�\�b�h
    /// </summary>
    /// <param name="bossWaveNumber">�{�X�f�[�^���̃E�F�[�u�ԍ�</param>
    public void ChangeWaveLoad(int bossWaveNumber)
    {
        // HP�E�ړ��p�^�[��
        hp = bossData.bossWaveData[bossWaveNumber].BossHp;
        movePattern = bossData.bossWaveData[bossWaveNumber].BossMovePattern;

        // �ˌ��p�̃p�����[�^�E�摜
        shotData = bossData.bossWaveData[bossWaveNumber].BossShotData;

        if (shotData != null)
        {
            bulletSpeed = shotData.BulletSpeed;
            bulletColor = shotData.BulletColorType;
            bulletShotInterval = shotData.ShotIntervalTime;

            // �G�̔��˂���e�����擾
            enemyBulletData = shotData.BulletData;
        }
    }

    /// <summary>
    /// �E�F�[�u���؂�ւ��ۂ̉��o
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeWaveEffect()
    {
        // �ˌ��𒆎~
        canBossShot = false;

        // ���̃E�F�[�u���X�y���J�[�h�Ȃ�ǉ����o
        if (bossData.bossWaveData[nowWeveNumber].IsSpellCard)
        {
            yield return new WaitForSeconds(3f);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

        // ���̃E�F�[�u�����[�h
        ChangeWaveLoad(nowWeveNumber);

        // �ˌ����ĊJ
        canBossShot = true;
    }

    /// <summary>
    /// ��_���[�W���̉��o�Ə���
    /// </summary>
    /// <returns></returns>
    IEnumerator BossDefeatEffect()
    {

        Vector2 currentVelocity = default;

        Vector2 goalPos = new Vector2(this.transform.position.x + 3, this.transform.position.y + 3);

        // �E��Ɏ����Ă���
        for (Vector2 pos = this.transform.position; pos.y < goalPos.y - 0.01f; pos = Vector2.SmoothDamp(pos, goalPos, ref currentVelocity, 0.05f))
        {
            this.transform.position = pos;

            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.1f);


        // ���j���o
        float scale = this.transform.localScale.x;

        // ���X�ɓ������傫������
        for (float alpha = 1; alpha > 0; alpha -= 0.03f)
        {
            scale += 0.1f;
            this.gameObject.transform.localScale = new Vector3(scale, scale, 1);


            yield return new WaitForSeconds(0.01f);
        }

        gameObject.SetActive(false);
    }
}