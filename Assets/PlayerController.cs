using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//deklarujemy sobie enum Lane, który bêdzie przechowywa³ wartoœci -3, 0, 3
//odpowiadaj¹ce pozycji x dla lewego, œrodkowego i prawego pasa
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
    //prêdkoœæ zmiany pasa
    [SerializeField]
    public float laneChangeSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        //zaczynamy ze œrodkowego pasa
        targetLane = Lane.MIDDLE;
        //ustawiamy docelow¹ pozycjê na aktualn¹ pozycjê
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //sprawdŸ czy gracz nacisn¹³ klawisz "a" lub "d"
        //jeœli tak to ustaw nowy docelowy pas i flagê isMoving
        GetInput();
        //przesuñ gracza kawa³ek w kierunku docelowego pasa
        Move();
    }

    void GetInput()
    {
        //jeœli naciœniêty jest klawisz "a" lub "d"
        //i jednoczeœnie nie jesteœmy w trakcie zmiany pasa
        //to ustaw nowy docelowy pas
        if (Input.GetAxisRaw("Horizontal") != 0 && !isMoving)
        {
            //dodaj do wartoœci liczbowej pasa +3 albo -3
            targetLane += (int)Input.GetAxisRaw("Horizontal")*3;
            //je¿eli targetLane jest mniejszy od -3 to ustaw na -3
            //je¿eli jest wiêkszy od 3 to ustaw na 3
            targetLane = (Lane)Math.Clamp((int)targetLane, -3, 3);
        }
    }

    public void Move()
    {
        //jeœli odleg³oœæ miêdzy aktualn¹ pozycj¹ gracza
        //a docelowym pasem jest wiêksza ni¿ epsilon
        //epsilon to bardzo ma³a wartoœæ bliska zeru
        if (Mathf.Abs(transform.position.x - (int)targetLane) > Mathf.Epsilon)
        {
            //nie jesteœmy na docelowym pasie - zacznij siê przesuwaæ
            isMoving = true;
            //ustawiamy docelow¹ pozycjê
            targetPosition = new Vector3((int)targetLane, transform.position.y, transform.position.z);
            //przesuwamy gracza w kierunku docelowej pozycji
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
        }
        else
        {
            //jestesmy w granicach prawid³owego pasa - nie przesuwamy siê
            isMoving = false;
        }
    }
}
