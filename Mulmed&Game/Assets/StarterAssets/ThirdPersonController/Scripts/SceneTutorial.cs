using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTutorial : MonoBehaviour
{
    // Variabel untuk melacak apakah objek telah muncul
    public bool objectHasAppeared = false;
    public GameObject satu;
    public GameObject dua;
    public GameObject tiga;
    public GameObject empat;
    public GameObject lima;
    public GameObject enam;
    public GameObject tujuh;
    bool isQReleased = false;
    bool isWASDorArrowKeysPressed = false;
    bool isEKeyPressed = false;
    bool isMouseButtonDown = false;
    bool isQKeyUp = false;
    bool isShiftAndWPressed = false;

    bool isWPressed = false;
    bool isAPressed = false;
    bool isSPressed = false;
    bool isDPressed = false;
    
    bool isUpArrowPressed = false;
    bool isDownArrowPressed = false;
    bool isRightArrowPressed = false;
    bool isLeftArrowPressed = false;

    void Update()
    {
        // NOMOR 1 : WASD
        if (Input.GetKeyUp(KeyCode.W)) {
            isWPressed = true;
        }
        if (isWPressed && Input.GetKeyUp(KeyCode.A)) {
            isAPressed = true;
        }
        if (isAPressed && Input.GetKeyUp(KeyCode.S)) {
            isSPressed = true;
        }
        if (isSPressed && Input.GetKeyUp(KeyCode.D)) {
            isDPressed = true;
        }

        if (!isWASDorArrowKeysPressed && isDPressed)
        {   
            Debug.Log("Berhasil 1");
            // Menghancurkan gameObject satu dalam waktu 5 detik
            Destroy(satu, 2f);
            // Memunculkan gameObject dua dalam waktu 2 detik
            Invoke("ShowDua", 2f);
            isWASDorArrowKeysPressed = true;
        }

        // NOMOR 1 : Arrow Keys
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            isUpArrowPressed = true;
        }
        if (isUpArrowPressed && Input.GetKeyUp(KeyCode.DownArrow)) {
            isDownArrowPressed = true;
        }
        if (isDownArrowPressed && Input.GetKeyUp(KeyCode.RightArrow)) {
            isRightArrowPressed = true;
        }
        if (isRightArrowPressed && Input.GetKeyUp(KeyCode.LeftArrow)) {
            isLeftArrowPressed = true;
        }

        if (!isWASDorArrowKeysPressed && isLeftArrowPressed)
        {   
            Debug.Log("Berhasil 1");
            // Menghancurkan gameObject satu dalam waktu 5 detik
            Destroy(satu, 2f);
            // Memunculkan gameObject dua dalam waktu 2 detik
            Invoke("ShowDua", 2f);
            isWASDorArrowKeysPressed = true;
        }

        // NOMOR 2 : KAKI
        if (isWASDorArrowKeysPressed && !isEKeyPressed && Input.GetKeyDown(KeyCode.E))
        {   
            Debug.Log("Berhasil 2");
            // Menghancurkan gameObject satu dalam waktu 5 detik
            Destroy(dua, 2f);
            // Memunculkan gameObject dua dalam waktu 2 detik
            Invoke("ShowTiga", 2f);
            isEKeyPressed = true;
        }
        // NOMOR 3 : PUKUL
        if (isEKeyPressed && !isMouseButtonDown && Input.GetMouseButtonDown(0)){
            Debug.Log("Berhasil 3");
            // Menghancurkan gameObject satu dalam waktu 5 detik
            Destroy(tiga, 2f);
            // Memunculkan gameObject dua dalam waktu 2 detik
            Invoke("ShowEmpat", 2f);
            isMouseButtonDown = true;
        }
        // NOMOR 4 : AMBIL PEDANG DAN HUNUS
        if (isMouseButtonDown && !isQReleased && Input.GetKeyUp(KeyCode.Q)) {
            isQReleased = true;
        }

        if (isQReleased && !isQKeyUp && Input.GetMouseButtonDown(0)) {
            Debug.Log("Berhasil 4");
            // Menghancurkan gameObject satu dalam waktu 5 detik
            Destroy(empat, 2f);
            // Memunculkan gameObject dua dalam waktu 2 detik
            Invoke("ShowLima", 2f);
            isQKeyUp = true;
        }
        // NOMOR 5 : MENSARUNGKAN PEDANG
        if (isQKeyUp && !isShiftAndWPressed && Input.GetKeyUp(KeyCode.Q)) {
            Debug.Log("Berhasil 5");
            // Menghancurkan gameObject satu dalam waktu 5 detik
            Destroy(lima, 2f);
            // Memunculkan gameObject dua dalam waktu 2 detik
            Invoke("ShowEnam", 2f);
            isShiftAndWPressed = true;
        }
        // NOMOR 6 : BERLARI
        if (isShiftAndWPressed && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift)) {
            Debug.Log("Berhasil 6");
            // Menghancurkan gameObject satu dalam waktu 5 detik
            Destroy(enam, 2f);
            // Memunculkan gameObject dua dalam waktu 2 detik
            Invoke("ShowTujuh", 2f);
        }
        // NOMOR 7 : ENTER
        if (isShiftAndWPressed && Input.GetKeyDown(KeyCode.Return))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            // Muat scene berikutnya
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
    }

    void ShowDua()
    {
        dua.SetActive(true);
    }
    void ShowTiga()
    {
        tiga.SetActive(true);
    }
    void ShowEmpat()
    {
        empat.SetActive(true);
    }
    void ShowLima()
    {
        lima.SetActive(true);
    }
    void ShowEnam()
    {
        enam.SetActive(true);
    }
    void ShowTujuh()
    {
        tujuh.SetActive(true);
    }
}
