using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable] //��Ʃ����Ʈ  public ����� ��� �ʵ带
public class PlayerAnimation // �ν����� â�� ���� �ش�.
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runLeft;
    public AnimationClip runRight;
    public AnimationClip Sprint;
    
}

public class Player : MonoBehaviour
{
    public PlayerAnimation playerAnimation;
    [SerializeField] float moveSpeed = 5f;
    float rotSpeed = 300f;
    [SerializeField] Rigidbody rbody;
    [SerializeField] CapsuleCollider capCol;
    [SerializeField] Transform tr;
    [SerializeField] Animation _animation;
    float h, v, r;
    public bool isRunning;

    private void OnEnable()
    {
        GameManager.OnItemChange += UpdateSetup;
                        //�̺�Ʈ ���
    }
    void UpdateSetup()
    {
        moveSpeed = GameManager.G_instance.gameData.speed;
    }
    void Start()
    {

        moveSpeed = GameManager.G_instance.gameData.speed;
        //���۳�Ʈ ĳ�� ó��
        rbody = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        tr = GetComponent<Transform>();
        _animation = GetComponent<Animation>();
        _animation.Play(playerAnimation.idle.name);
        isRunning = false;
    }
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxisRaw("Mouse X");
        Vector3 moveDir = (h * Vector3.right) + (v * Vector3.forward);
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        {
            moveAni();
        }
        tr.Rotate(Vector3.up * r * Time.deltaTime * rotSpeed);

        Sprint();
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            moveSpeed = 10f;
            _animation.CrossFade(playerAnimation.Sprint.name, 0.3f);
            isRunning = true;

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            moveSpeed = 5f;

        }
    }

    private void moveAni()
    {
        if (h > 0.1f)
            _animation.CrossFade(playerAnimation.runRight.name, 0.3f);
        else if (h < -0.1f)
            _animation.CrossFade(playerAnimation.runLeft.name, 0.3f);
        else if (v > 0.1f)
            _animation.CrossFade(playerAnimation.runForward.name, 0.3f);
        else if (v < -0.1f)
            _animation.CrossFade(playerAnimation.runBackward.name, 0.3f);
        else
            _animation.CrossFade(playerAnimation.idle.name, 0.3f);
    }
}
