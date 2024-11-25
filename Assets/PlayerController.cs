using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//deklarujemy sobie enum Lane, kt�ry b�dzie przechowywa� warto�ci -3, 0, 3
//odpowiadaj�ce pozycji x dla lewego, �rodkowego i prawego pasa
enum Lane
{
    LEFT = -3,
    MIDDLE = 0,
    RIGHT = 3
 }
public class PlayerController : MonoBehaviour
{
    //czy aktualnie zmieniamy pas
    bool isMoving = false;
    //docelowy pas
    Lane targetLane;
    //docelowa pozycja
    Vector3 targetPosition;
    //pr�dko�� zmiany pasa
    [SerializeField]
    public float laneChangeSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        //zaczynamy ze �rodkowego pasa
        targetLane = Lane.MIDDLE;
        //ustawiamy docelow� pozycj� na aktualn� pozycj�
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //sprawd� czy gracz nacisn�� klawisz "a" lub "d"
        //je�li tak to ustaw nowy docelowy pas i flag� isMoving
        GetInput();
        //przesu� gracza kawa�ek w kierunku docelowego pasa
        Move();
    }

    void GetInput()
    {
        //je�li naci�ni�ty jest klawisz "a" lub "d"
        //i jednocze�nie nie jeste�my w trakcie zmiany pasa
        //to ustaw nowy docelowy pas
        if (Input.GetAxisRaw("Horizontal") != 0 && !isMoving)
        {
            //dodaj do warto�ci liczbowej pasa +3 albo -3
            targetLane += (int)Input.GetAxisRaw("Horizontal")*3;
            //je�eli targetLane jest mniejszy od -3 to ustaw na -3
            //je�eli jest wi�kszy od 3 to ustaw na 3
            targetLane = (Lane)Math.Clamp((int)targetLane, -3, 3);
        }
    }

    public void Move()
    {
        //je�li odleg�o�� mi�dzy aktualn� pozycj� gracza
        //a docelowym pasem jest wi�ksza ni� epsilon
        //epsilon to bardzo ma�a warto�� bliska zeru
        if (Mathf.Abs(transform.position.x - (int)targetLane) > Mathf.Epsilon)
        {
            //nie jeste�my na docelowym pasie - zacznij si� przesuwa�
            isMoving = true;
            //ustawiamy docelow� pozycj�
            targetPosition = new Vector3((int)targetLane, transform.position.y, transform.position.z);
            //przesuwamy gracza w kierunku docelowej pozycji
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
        }
        else
        {
            //jestesmy w granicach prawid�owego pasa - nie przesuwamy si�
            isMoving = false;
        }
    }
}
