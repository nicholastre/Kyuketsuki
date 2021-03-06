using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D theRB;
    public float moveSpeed;
    public float horizontalSpeed;
    public float verticalSpeed;

    public Animator animator;

    public static PlayerController instance;

    public string areaTransitionName;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public bool canMove = true;

    // Use this for initialization
    void Start () {

        // Vereifica se só existe uma instância do jogador na cena
        if (instance == null)
        {
            instance = this;
        } else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);

        // Garante que os atributos carregados nao sejam sobrescritos pela inicializacao
        GameManager.instance.LoadGame();
    }

    // Update is called once per frame
    void Update () {
        //Movimentação
        if (canMove)
        {
            horizontalSpeed = Input.GetAxisRaw("Horizontal");
            verticalSpeed = Input.GetAxisRaw("Vertical");

            animator.SetInteger("HorizontalSpeed", (int)horizontalSpeed);
            animator.SetInteger("VerticalSpeed", (int)verticalSpeed);
            theRB.velocity = new Vector2(horizontalSpeed, verticalSpeed).normalized * moveSpeed;

        } else
        {
            theRB.velocity = Vector2.zero;
        }

        //Para onde o personagem está olhando
        /*myAnim.SetFloat("moveX", theRB.velocity.x);
        myAnim.SetFloat("moveY", theRB.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }*/

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    // Chamada pelo Start da CameraController para definir limites de tela do jogador
    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(.5f, 1f, 0f);
        topRightLimit = topRight + new Vector3(-.5f, -1f, 0f);
    }
}
