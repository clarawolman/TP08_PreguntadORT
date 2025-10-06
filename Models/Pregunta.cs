
using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
namespace TP08_PreguntadORT.Models {
  
    public class Pregunta{
    public int PreguntaID {get; private set;}
    public string Texto{get; private set;}
    public int CategoriaID{get; private set;}
    public int DificultadID{get; private set;}


    public Pregunta(int id, string texto, int categoriaid, int dificultadid) {
        PreguntaID = id;
        Texto = texto;
        CategoriaID = categoriaid;
        DificultadID = dificultadid;
    }

    public Pregunta(){}
    }  
}