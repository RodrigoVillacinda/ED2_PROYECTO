using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Estruct
{
    public class LlaveSDES
    {
        private int[] Key;


        public LlaveSDES(int[] key)
        {
            Key = key;

        }

        //Seleccionar clave: 1010000010
        //Entrada: 1,2,3,4,5,6,7,8,9,10
        private int[] TenPermutations()
        {

            int[] tenpermutations = new int[10];
            int[] key = Key;

            tenpermutations[0] = key[2]; //3
            tenpermutations[1] = key[4]; //5
            tenpermutations[2] = key[1]; //2
            tenpermutations[3] = key[6]; //7
            tenpermutations[4] = key[3]; //4
            tenpermutations[5] = key[9]; //10
            tenpermutations[6] = key[0]; //1
            tenpermutations[7] = key[8]; //9
            tenpermutations[8] = key[7]; //8
            tenpermutations[9] = key[5]; //6


            return tenpermutations;
            //Salida: 3,5,2,7,4,10,1,9,8,6
            //Key: 1000001100
        }

        //divide la llave en dos mitades+
        //entrada:{10000} | {01100}
        //salida: {00001} | {11000}
        private int[] LS_1()
        {

            int[] ls_1 = new int[10];
            int[] left = new int[5]; //{10000}
            int[] right = new int[5]; //{01100}

            ls_1[0] = TenPermutations()[1]; //3
            ls_1[1] = TenPermutations()[2]; //5
            ls_1[2] = TenPermutations()[3]; //2
            ls_1[3] = TenPermutations()[4]; //7
            ls_1[4] = TenPermutations()[0]; //4

            ls_1[5] = TenPermutations()[6]; //10
            ls_1[6] = TenPermutations()[7]; //1
            ls_1[7] = TenPermutations()[8]; //9
            ls_1[8] = TenPermutations()[9]; //8
            ls_1[9] = TenPermutations()[5]; //6

            return ls_1;
            //key: 0 0 0 0 1 1 1 0 0 0
        }

        //k1
        //Entrada: {00001} | {11000}
        //salida: 10100100
        public int[] EightPrermutationsLS1()
        {
            int[] eightpermutations = new int[8];

            eightpermutations[0] = LS_1()[5]; //6
            eightpermutations[1] = LS_1()[2]; //3
            eightpermutations[2] = LS_1()[6]; //7
            eightpermutations[3] = LS_1()[3]; //4
            eightpermutations[4] = LS_1()[7]; //8
            eightpermutations[5] = LS_1()[4]; //5
            eightpermutations[6] = LS_1()[9]; //10
            eightpermutations[7] = LS_1()[8]; //9

            return eightpermutations;
        }

        //Entrada: {00001} | {11000}
        //Salida: {00100} | {00011}
        private int[] LS_2()
        {

            int[] ls_2 = new int[10];
            int[] left = new int[5]; //{10000}
            int[] right = new int[5]; //{01100}


            ls_2[0] = LS_1()[2]; //3
            ls_2[1] = LS_1()[3]; //5
            ls_2[2] = LS_1()[4]; //2
            ls_2[3] = LS_1()[0]; //7
            ls_2[4] = LS_1()[1]; //4

            ls_2[5] = LS_1()[7]; //10
            ls_2[6] = LS_1()[8]; //1
            ls_2[7] = LS_1()[9]; //9
            ls_2[8] = LS_1()[5]; //8
            ls_2[9] = LS_1()[6]; //6

            return ls_2;
            //key: 0 0 1 0 0 0 0 0 1 1
        }

        //k2
        public int[] EightPrermutationsLS2()
        {
            int[] eightpermutations = new int[8];

            eightpermutations[0] = LS_2()[5]; //6
            eightpermutations[1] = LS_2()[2]; //3
            eightpermutations[2] = LS_2()[6]; //7
            eightpermutations[3] = LS_2()[3]; //4
            eightpermutations[4] = LS_2()[7]; //8
            eightpermutations[5] = LS_2()[4]; //5
            eightpermutations[6] = LS_2()[9]; //10
            eightpermutations[7] = LS_2()[8]; //9

            return eightpermutations;
        }

    }
}
