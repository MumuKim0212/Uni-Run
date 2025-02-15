﻿using UnityEngine;
using System.Collections;
using UnityEngine.VFX;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour
{
    public AudioClip jumpClip;
    public AudioClip deathClip;
    public AudioClip getItem;
    public AudioClip hitSound;
    public float jumpForce = 700f;      // 점프 힘
    public ParticleSystem particle;
    public GameManager gameManager;
    public GameObject[] heart;
    public BackgroundMove bgMove;

    private bool isGrounded = false;     // 바닥에 닿았는지 나타냄
    private bool isDead = false;         // 사망 상태

    private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    private Animator animator;           // 사용할 애니메이터 컴포넌트
    private AudioSource playerAudio;     // 사용할 오디오 소스 컴포넌트

    private int life = 2;


    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Jump();
    }

    // 사용자 입력을 감지하고 점프하는 처리
    private void Jump()
    {
        // 사망시 처리를 더이상 진행하지 않고 종료
        if (isDead)
            return;

        // 마우스 왼쪽 버튼을 누름 && 최대 점프 횟수에 도달하지 않으면
        // 0: 왼쪽버튼, 1: 오른쪽버튼, 2: 휠 스크롤 버튼
        if (Input.GetMouseButtonDown(0))
        {
            if (Music.instance.isSoundOn == true)
                playerAudio.PlayOneShot(jumpClip);
            if (!particle.isPlaying)
                particle.Play();

            // 점프 직전에 속도를 순간적으로 제로(0,0)로 변경
            // 움직이던 힘이 합쳐져 점프 높이가 비일관적이 되지 않도록
            playerRigidbody.velocity = Vector2.zero;
            // 리지드바디에 위쪽으로 힘주기
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        // 마우스를 길게 누르면 오래점, 짧게 누르면 짧게점프
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // 마우스 왼쪽 버튼에서 손을 뗌 && 속도의 값이 양수(위로 상승)
            // 현재 속도를 절반으로 변경
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        // 애니메이터의 Grounded 파라미터를 isGrounded값으로 갱신
        animator.SetBool("Grounded", isGrounded);

    }

    // 사망 처리
    private void Die()
    {
        // 애니메이터의 Die 트리거 파라미터를 셋
        animator.SetTrigger("Die");

        if (Music.instance.isSoundOn == true)
            playerAudio.PlayOneShot(deathClip);

        // 속도를 제로(0,0)로 변경
        playerRigidbody.velocity = Vector2.zero;
        // 사망 상태를 true로 변경
        isDead = true;

        playerRigidbody.AddForce(Vector2.up * 700);

        // 게임 매니저의 게임오버 처리 실행
        gameManager.OnPlayerDead();
    }

    // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item" && Music.instance.isSoundOn == true)
            playerAudio.PlayOneShot(getItem);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        // 충돌한 상대방의 태그가 Dead이며 아직 사망하지 않았다면 Die 실행
        if (collision.collider.tag == "Dead" && !isDead)
        {
            collision.gameObject.SetActive(false);
            if (life > 0)
            {
                heart[life--].SetActive(false);
                playerAudio.PlayOneShot(hitSound);
                bgMove.isDamaged = true;
            }
            else
            {
                heart[life--].SetActive(false);
                Die();
            }
        }
        else if (collision.collider.tag == "Finish" && !isDead)
            Die();
    }
}