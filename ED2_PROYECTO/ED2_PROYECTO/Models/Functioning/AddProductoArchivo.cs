using ED2_PROYECTO.Models.Estruct;
using ED2_PROYECTO.Models.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Functioning
{
    public static class AddProductoArchivo
    {

        public static void AgregarProducto(string rutaCSV, string rootEscrbir)
        {
            CargarCsv(rutaCSV, rootEscrbir);
        }
        public static  List<string[]> parseCSV(string path)
        {
            List<string[]> parsedData = new List<string[]>();

            using (StreamReader readFile = new StreamReader(path))
            {
                string line;
                string[] row;

                while ((line = readFile.ReadLine()) != null)
                {
                    row = line.Split(',');
                    parsedData.Add(row);
                }
            }
            return parsedData;
        }

        static StreamReader lecturaArchivo;
        private static  void CargarCsv(string path, string ruta)
        {
            string linea = "";
            lecturaArchivo = new StreamReader(path);//Ruta del archivo a cargar
            testingTree2.ruta = ruta + "IngresoCSV.txt";
            while ((linea = lecturaArchivo.ReadLine()) != null)
            {
                string[] datos = linea.Split(';');
                //Arbol de Producto
                //Insertar en arbol producto por cada registro leido
                testingTree2.insertElement(new Producto { ID_Producto = int.Parse(datos[0]), Nombre = datos[1], Precio = (datos[2]) });
                testingTree2.InsertarEnDisco(new Producto { ID_Producto = int.Parse(datos[0]), Nombre = datos[1], Precio = (datos[2]) });
            }

        }

        static BStarTree<Producto> testingTree2 = new BStarTree<Producto>(7);
        public static void insertar()
        {

            string path = @"C:\Users\" + Environment.UserName + @"\OneDrive\Escritorio\productos.csv"; //Ruta de creacion del arbol en disco
            CargarCsv(path, path);
        }

    }
}
