using UnityEditor.UI;
using UnityEngine;
//ÉvÉåÉCÉÑÅ[ÇÃëÄçÏ
public class PlayerController : PlayerManager
{
    protected void Start()
    {
        _playerState = PlayerState.Idle;
        _joystickScript = _joystick.GetComponent<Joystick>();
        _playerAnimator = GetComponent<Animator>();
        _horizontalInputValue = 0.0f;
        _verticalInputValue = 0.0f;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _shotTime -= Time.deltaTime;
        if (_shotTime <= 0.0f)
        {
            PlayerShot(_playerShotMode, _playerShotMode.BulletData, 0, this.transform.position);
            _shotTime = _playerShotMode.ShotIntervalTime;    
        }

        //_rigidbody2D.velocity = transform.up * 10.0f;
        _horizontalInputValue = _joystickScript.Horizontal;
        _verticalInputValue = _joystickScript.Vertical;

        if (_horizontalInputValue < -_inputMinLimitValue)
        {
            _playerState = PlayerState.Left;
        }
        else if (_horizontalInputValue > _inputMinLimitValue)
        {
            _playerState = PlayerState.Right;
        }
        else
        {
            _playerState = PlayerState.Idle;

        }


    }

    private void FixedUpdate()
    {
        switch (_playerState)
        {
            case PlayerState.Idle:
                _playerAnimator.SetInteger("playeranim", 0);
                PlayerVerticalMove();
                break;
            case PlayerState.Right:
                _playerAnimator.SetInteger("playeranim", 1);
                PlayerVerticalMove();
                break;
            case PlayerState.Left:
                _playerAnimator.SetInteger("playeranim", 2);
                PlayerVerticalMove();
                break;
            case PlayerState.Damage:
                break;
        }
    }

    private void PlayerVerticalMove()
    {
        _playerPotision = this.transform.position;

        if (_verticalInputValue < -_inputMinLimitValue || _verticalInputValue > _inputMinLimitValue)
        {
            if (_verticalInputValue < -_inputMinLimitValue)
            {
                _playerPotision.y -= _moveValue;
            }
            else if (_verticalInputValue > _inputMinLimitValue)
            {
                _playerPotision.y += _moveValue;
            }
        }
        if (_horizontalInputValue < -_inputMinLimitValue || _horizontalInputValue > _inputMinLimitValue)
        {
            if (_horizontalInputValue < -_inputMinLimitValue)
            {
                _playerPotision.x -= _moveValue;
            }
            else if (_horizontalInputValue > _inputMinLimitValue)
            {
                _playerPotision.x += _moveValue;
            }
        }
        this.transform.position = new Vector2(Mathf.Clamp(_playerPotision.x, -2.5f, 2.5f), Mathf.Clamp(_playerPotision.y, -4.8f, 4.8f));
    }
}
