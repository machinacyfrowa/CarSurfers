using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerScript : MonoBehaviour
{
    //prefabrykat samochodu jad�cego z przodu
    public GameObject carPrefab;
    //co ile sekund ma si� pojawi� nowy samoch�d
    public float spawnInterval = 2;
    // Start is called before the first frame update
    void Start()
    {
        //uruchamiamy funkcj� SpawnCar jako coroutine czyli funkcj�,
        //kt�ra dzia�a w tle
        StartCoroutine(SpawnCar());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnCar()
    {
        //while true mo�na u�y� z yield bez obawy o zawieszenie gry
        while (true)
        {
            //wylosuj liczb� z przedzia�u [-1, 1] i pomn� przez 3
            //aby uzyska� warto�� -3, 0 lub 3
            Lane lane = (Lane)(Random.Range(-1, 2) * 3);
            //stw�rz nowe wsp�rz�dne Vector3 z warto�ci� x r�wn�
            //wylosowanej warto�ci oraz y i z takie jak spawner
            Vector3 spawnPosition = new Vector3((float)lane, transform.position.y, transform.position.z);
            //stw�rz nowy obiekt z prefabrykatu carPrefab na pozycji spawnPosition
            GameObject car = Instantiate(carPrefab, spawnPosition, Quaternion.identity);
            //dodaj si�� do obiektu car w kierunku przeciwnym do z
            //�eby wys�a� go w kierunku gracza tworz�c z�udzenie wyprzedzania
            car.GetComponent<Rigidbody>().AddForce(Vector3.back * 1000);
            //usuni�cie obiektu po 10 sekundach
            GameObject.Destroy(car, 10);
            //poczekaj spawnInterval sekund
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
