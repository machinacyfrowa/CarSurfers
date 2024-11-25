using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerScript : MonoBehaviour
{
    //prefabrykat samochodu jad¹cego z przodu
    public GameObject carPrefab;
    //co ile sekund ma siê pojawiæ nowy samochód
    public float spawnInterval = 2;
    // Start is called before the first frame update
    void Start()
    {
        //uruchamiamy funkcjê SpawnCar jako coroutine czyli funkcjê,
        //która dzia³a w tle
        StartCoroutine(SpawnCar());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnCar()
    {
        //while true mo¿na u¿yæ z yield bez obawy o zawieszenie gry
        while (true)
        {
            //wylosuj liczbê z przedzia³u [-1, 1] i pomnó¿ przez 3
            //aby uzyskaæ wartoœæ -3, 0 lub 3
            Lane lane = (Lane)(Random.Range(-1, 2) * 3);
            //stwórz nowe wspó³rzêdne Vector3 z wartoœci¹ x równ¹
            //wylosowanej wartoœci oraz y i z takie jak spawner
            Vector3 spawnPosition = new Vector3((float)lane, transform.position.y, transform.position.z);
            //stwórz nowy obiekt z prefabrykatu carPrefab na pozycji spawnPosition
            GameObject car = Instantiate(carPrefab, spawnPosition, Quaternion.identity);
            //dodaj si³ê do obiektu car w kierunku przeciwnym do z
            //¿eby wys³aæ go w kierunku gracza tworz¹c z³udzenie wyprzedzania
            car.GetComponent<Rigidbody>().AddForce(Vector3.back * 1000);
            //usuniêcie obiektu po 10 sekundach
            GameObject.Destroy(car, 10);
            //poczekaj spawnInterval sekund
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
