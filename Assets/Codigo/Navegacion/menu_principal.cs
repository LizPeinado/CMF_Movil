using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_principal : MonoBehaviour
{
   private int nivel_actual = 1;

   private Scene escena_actual;
   public void ComenzarJuego(){
        PlayerPrefs.SetInt("personaje_seleccionado", 0);
        PlayerPrefs.SetInt("empezar_juego", 1);
        SceneManager.LoadSceneAsync(1);
    }

    public void EmpezarNivel1(){
        PlayerPrefs.SetInt("empezar_juego", 1);
        SceneManager.LoadSceneAsync(1);
    }

    public void EmpezarNivel2(){
        PlayerPrefs.SetInt("empezar_juego", 1);
        SceneManager.LoadSceneAsync(2);
    }

    public void EmpezarNivel3(){
        PlayerPrefs.SetInt("empezar_juego", 1);
        SceneManager.LoadSceneAsync(3);
    }
        
    public void MenuPrincipal(){
        PlayerPrefs.SetInt("empezar_juego", 1);
        SceneManager.LoadSceneAsync(0);
    }

    public void SiguienteNivel(){
        PlayerPrefs.SetInt("empezar_juego", 1);
        escena_actual = SceneManager.GetActiveScene(); //Conseguir escena actual
        nivel_actual = escena_actual.buildIndex + 1; //Aumentar valor para pasar al siguiente
        if(nivel_actual > 3){ nivel_actual = 0; PlayerPrefs.SetInt("empezar_juego", 0);} // Max 3 escenas, si es más de 4 regresa al menu
        SceneManager.LoadSceneAsync(nivel_actual);
    }



}
