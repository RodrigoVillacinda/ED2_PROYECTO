using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Estruct
{
    public class CipherSDES
    {

        private int[] Key;
        private int[] K1;
        private int[] K2;

        public CipherSDES(int[] key, int[] k1, int[] k2)
        {
            Key = key;
            K1 = k1;
            this.K2 = k2;
        }


        //paso2
        private int[] EightPermutations()
        {
            int[] eightpermutations = new int[8];

            eightpermutations[0] = Key[1]; //2
            eightpermutations[1] = Key[5]; //6
            eightpermutations[2] = Key[2]; //3
            eightpermutations[3] = Key[0]; //1
            eightpermutations[4] = Key[3]; //4
            eightpermutations[5] = Key[7]; //8
            eightpermutations[6] = Key[5]; //5
            eightpermutations[7] = Key[6]; //7

            return eightpermutations;
        }

        //paso3, dividir izquierda
        private int[] LeftBits()
        {

            int[] leftbits = new int[4];
            int[] temp = EightPermutations();
            leftbits[0] = temp[0];
            leftbits[1] = temp[1];
            leftbits[2] = temp[2];
            leftbits[3] = temp[3];


            return leftbits;

        }

        //paso4, dividir derecha Tamño: 4 bits
        private int[] RightBits()
        {

            int[] righbits = new int[4];
            int[] temp = EightPermutations();
            righbits[0] = temp[4];
            righbits[1] = temp[5];
            righbits[2] = temp[6];
            righbits[3] = temp[7];


            return righbits;

        }

        //paso5, expandir y pemutar, OP
        private int[] ExpandRigh()
        {
            int[] expandright = new int[8];
            int[] temp = RightBits();
            expandright[0] = temp[3]; //4
            expandright[1] = temp[0]; //1
            expandright[2] = temp[1]; //2
            expandright[3] = temp[2]; //3

            expandright[4] = temp[1]; //2
            expandright[5] = temp[2]; //3
            expandright[6] = temp[3]; //4
            expandright[7] = temp[0]; //1


            return expandright;
        }

        //paso6 opXORk1
        private int[] OPXORK1()
        {
            int[] opxork1 = new int[8];
            int[] k1 = K1;
            int[] temp = ExpandRigh();

            opxork1[0] = temp[0] ^ k1[0];
            opxork1[1] = temp[1] ^ k1[1];
            opxork1[2] = temp[2] ^ k1[2];
            opxork1[3] = temp[3] ^ k1[3];
            opxork1[4] = temp[4] ^ k1[4];
            opxork1[5] = temp[5] ^ k1[5];
            opxork1[6] = temp[6] ^ k1[6];
            opxork1[7] = temp[7] ^ k1[7];

            return opxork1;
        }

        private int[] S0()
        {
            int[] s0 = new int[2];
            int[] opxork1 = new int[4];
            int row = 0;
            int column = 0;
            int[] temp = OPXORK1();

            int roww = 0;
            int columnn = 0;

            opxork1[0] = temp[0];
            opxork1[1] = temp[1];
            opxork1[2] = temp[2];
            opxork1[3] = temp[3];

            int[,] box0 = new int[4,4];
            box0[0,0] = 01;
            box0[0,1] = 00;
            box0[0,2] = 11;
            box0[0,3] = 10;

            box0[1,0] = 11;
            box0[1,1] = 00;
            box0[1,2] = 01;
            box0[1,3] = 00;

            box0[2,0] = 00;
            box0[2,1] = 10;
            box0[2,2] = 01;
            box0[2,3] = 11;

            box0[3,0] = 11;
            box0[3,1] = 01;
            box0[3,2] = 11;
            box0[3,3] = 00;

            int o1 = opxork1[0];
            int o2 = opxork1[3];

            row = (o1 * 2) + (o2 * 1); //2

            int i1 = opxork1[1];
            int i2 = opxork1[2];

            column = (i1 * 2) + (i2 * 1); //3

            roww = box0[row,column]; //00
            char[] rowColumn = new char[2];

            if (roww == 0)
            {
                String rowwx = "00";
                rowColumn = rowwx.ToCharArray();
            }
            if (roww == 1)
            {
                String rowwx = "01";
                rowColumn = rowwx.ToCharArray();
            }
            if (roww != 0 && roww != 1)
            {
                rowColumn = roww.ToString().ToCharArray();
            }

            for (int i = 0; i < 2; i++)
            {

                string item = Convert.ToString(rowColumn[i]);
                int columnnn = Int32.Parse(item);
                columnn = columnnn;
                if (columnn == 49)
                {
                    s0[i] = 1;
                }
                if (columnn == 48)
                {
                    s0[i] = 0;
                }


            }

            return s0;
        }

        private int[] S1()
        {
            int[] s0 = new int[2];
            int[] opxork1 = new int[8];
            int row = 0;
            int column = 0;
            int[] temp = OPXORK1();

            int roww = 0;
            int columnn = 0;

            opxork1[0] = temp[4];
            opxork1[1] = temp[5];
            opxork1[2] = temp[6];
            opxork1[3] = temp[7];

            int[,] box0 = new int[4,4];
            box0[0,0] = 00;
            box0[0,1] = 01;
            box0[0,2] = 10;
            box0[0,3] = 11;

            box0[1,0] = 10;
            box0[1,1] = 00;
            box0[1,2] = 01;
            box0[1,3] = 11;

            box0[2,0] = 11;
            box0[2,1] = 00;
            box0[2,2] = 01;
            box0[2,3] = 00;

            box0[3,0] = 10;
            box0[3,1] = 01;
            box0[3,2] = 00;
            box0[3,3] = 11;


            int o1 = opxork1[0];
            int o2 = opxork1[3];

            row = (o1 * 2) + (o2 * 1); //2


            int i1 = opxork1[1];
            int i2 = opxork1[2];

            column = (i1 * 2) + (i2 * 1); //3

            roww = box0[row,column]; //00

            char[] rowColumn = new char[2];

            if (roww == 0)
            {
                String rowwx = "00";
                rowColumn = rowwx.ToCharArray();
            }
            if (roww == 1)
            {
                String rowwx = "01";
                rowColumn = rowwx.ToCharArray();
            }
            if (roww != 0 && roww != 1)
            {
                rowColumn = roww.ToString().ToCharArray();
            }

            for (int i = 0; i < 2; i++)
            {

                string item = rowColumn[i].ToString();
                int columnnn = Int32.Parse(item);
                columnn = columnnn;
                if (columnn == 49)
                {
                    s0[i] = 1;
                }
                if (columnn == 48)
                {
                    s0[i] = 0;
                }


            }

            return s0;
        }


        private int[] S0andS1()
        {
            int[] s0ands1 = new int[4];

            int[] temp = S0();
            int[] temp2 = S1();
            s0ands1[0] = temp[0];
            s0ands1[1] = temp[1];

            s0ands1[2] = temp2[0];
            s0ands1[3] = temp2[1];

            return s0ands1;
        }

        //PASO7: permutación de 4 bits, sobre la union de la box0 y box1
        private int[] FourPermutations()
        {
            int[] fourpermutations = new int[4];
            int[] temp = S0andS1();

            fourpermutations[0] = temp[1]; //2
            fourpermutations[1] = temp[3]; //4
            fourpermutations[2] = temp[2]; //3
            fourpermutations[3] = temp[0]; //1

            return fourpermutations;
        }

        //PASO8: Paso3 XOR paso 7, tamño: 4 bits
        private int[] LeftbitsXORFourpermutations()
        {
            int[] leftbitsXORfourpemutations = new int[4];
            int[] temp1 = FourPermutations();
            int[] temp2 = LeftBits();

            leftbitsXORfourpemutations[0] = temp1[0] ^ temp2[0];
            leftbitsXORfourpemutations[1] = temp1[1] ^ temp2[1];
            leftbitsXORfourpemutations[2] = temp1[2] ^ temp2[2];
            leftbitsXORfourpemutations[3] = temp1[3] ^ temp2[3];

            return leftbitsXORfourpemutations;
        }

        //PASO9: paso8 + permutación de 8 a la derecha || Tamaño: 8bits
        private int[] XORandRightBits()
        {
            int[] xorANDrightbits = new int[8];
            int[] temp1 = LeftbitsXORFourpermutations();
            int[] temp2 = RightBits();

            xorANDrightbits[0] = temp1[0];
            xorANDrightbits[1] = temp1[1];
            xorANDrightbits[2] = temp1[2];
            xorANDrightbits[3] = temp1[3];

            xorANDrightbits[4] = temp2[0];
            xorANDrightbits[5] = temp2[1];
            xorANDrightbits[6] = temp2[2];
            xorANDrightbits[7] = temp2[3];

            return xorANDrightbits;
        }

        //PASO10: mitad izquierdad del paso9 || Tamaaño: 4 bits
        private int[] LeftBitsXOR()
        {

            int[] righbits = new int[4];
            int[] temp = XORandRightBits();
            righbits[0] = temp[0];
            righbits[1] = temp[1];
            righbits[2] = temp[2];
            righbits[3] = temp[3];


            return righbits;

        }

        //PASO11: mitad derecha del paso9 || Tamaaño: 4 bits
        private int[] RightBitsXOR()
        {

            int[] righbits = new int[4];
            int[] temp = XORandRightBits();

            righbits[0] = temp[4];
            righbits[1] = temp[5];
            righbits[2] = temp[6];
            righbits[3] = temp[7];


            return righbits;

        }

        //PASO12: invertir y unir || Tamaño: 8 bits
        private int[] EightBits()
        {
            int[] eightbits = new int[8];
            int[] temp1 = RightBitsXOR();
            int[] temp2 = LeftBitsXOR();

            eightbits[0] = temp1[0];
            eightbits[1] = temp1[1];
            eightbits[2] = temp1[2];
            eightbits[3] = temp1[3];

            eightbits[4] = temp2[0];
            eightbits[5] = temp2[1];
            eightbits[6] = temp2[2];
            eightbits[7] = temp2[3];


            return eightbits;
        }
        //---------------------------------------funciona-----------------------------------------------


        //----------------------------------------------------KEY 2--------------------------------------------------------------------


        //paso13, dividir izquierda
        private int[] LeftBitsK2()
        {

            int[] leftbits = new int[4];
            int[] temp = EightBits();

            leftbits[0] = temp[0];
            leftbits[1] = temp[1];
            leftbits[2] = temp[2];
            leftbits[3] = temp[3];


            return leftbits;

        }

        //paso14, dividir derecha Tamño: 4 bits
        private int[] RightBitsK2()
        {

            int[] righbits = new int[4];
            int[] temp = EightBits();

            righbits[0] = temp[4];
            righbits[1] = temp[5];
            righbits[2] = temp[6];
            righbits[3] = temp[7];


            return righbits;

        }

        //paso15, expandir y pemutar, OP
        private int[] ExpandRighK2()
        {
            int[] expandright = new int[8];
            int[] temp = RightBitsK2();
            expandright[0] = temp[3]; //4
            expandright[1] = temp[0]; //1
            expandright[2] = temp[1]; //2
            expandright[3] = temp[2]; //3
            expandright[4] = temp[1]; //2
            expandright[5] = temp[2]; //3
            expandright[6] = temp[3]; //4
            expandright[7] = temp[0]; //1


            return expandright;
        }

        //paso16 opXORk1
        private int[] OPXORK2()
        {
            int[] opxork1 = new int[8];
            int[] k1 = K1;
            int[] temp = ExpandRighK2();

            opxork1[0] = K2[0] ^ temp[0];
            opxork1[1] = K2[1] ^ temp[1];
            opxork1[2] = K2[2] ^ temp[2];
            opxork1[3] = K2[3] ^ temp[3];
            opxork1[4] = K2[4] ^ temp[4];
            opxork1[5] = K2[5] ^ temp[5];
            opxork1[6] = K2[6] ^ temp[6];
            opxork1[7] = K2[7] ^ temp[7];

            return opxork1;
        }

        private int[] S0K2()
        {
            int[] s0 = new int[2];
            int[] opxork1 = new int[8];
            int row = 0;
            int column = 0;
            int[] temp = OPXORK2();

            int roww = 0;
            int columnn = 0;

            opxork1[0] = temp[0];
            opxork1[1] = temp[1];
            opxork1[2] = temp[2];
            opxork1[3] = temp[3];

            int[,] box0 = new int[4,4];
            box0[0,0] = 01;
            box0[0,1] = 00;
            box0[0,2] = 11;
            box0[0,3] = 10;

            box0[1,0] = 11;
            box0[1,1] = 00;
            box0[1,2] = 01;
            box0[1,3] = 00;

            box0[2,0] = 00;
            box0[2,1] = 10;
            box0[2,2] = 01;
            box0[2,3] = 11;

            box0[3,0] = 11;
            box0[3,1] = 01;
            box0[3,2] = 11;
            box0[3,3] = 00;


            int o1 = opxork1[0];
            int o2 = opxork1[3];

            row = (o1 * 2) + (o2 * 1); //2


            int i1 = opxork1[1];
            int i2 = opxork1[2];

            column = (i1 * 2) + (i2 * 1); //3

            roww = box0[row,column]; //00
            roww = box0[row,column]; //00
            char[] rowColumn = new char[2];

            if (roww == 0)
            {
                string rowwx = "00";
                rowColumn = rowwx.ToCharArray();
            }
            if (roww == 1)
            {
                string rowwx = "01";
                rowColumn = rowwx.ToCharArray();
            }
            if (roww != 0 && roww != 1)
            {
                rowColumn = roww.ToString().ToCharArray();
            }

            for (int i = 0; i < 2; i++)
            {

                string item = rowColumn[i].ToString();
                int columnnn = Int32.Parse(item);
                columnn = columnnn;
                if (columnn == 49)
                {
                    s0[i] = 1;
                }
                if (columnn == 48)
                {
                    s0[i] = 0;
                }


            }

            return s0;
        }

        private int[] S1K2()
        {
            int[] s0 = new int[2];
            int[] opxork1 = new int[8];
            int row = 0;
            int column = 0;
            int[] temp = OPXORK2();

            int roww = 0;
            int columnn = 0;

            opxork1[0] = temp[4];
            opxork1[1] = temp[5];
            opxork1[2] = temp[6];
            opxork1[3] = temp[7];

            int[,] box0 = new int[4, 4];
            box0[0, 0] = 00;
            box0[0, 1] = 01;
            box0[0, 2] = 10;
            box0[0, 3] = 11;

            box0[1, 0] = 10;
            box0[1, 1] = 00;
            box0[1, 2] = 01;
            box0[1, 3] = 11;

            box0[2,0] = 11;
            box0[2,1] = 00;
            box0[2,2] = 01;
            box0[2,3] = 00;

            box0[3,0] = 10;
            box0[3,1] = 01;
            box0[3,2] = 00;
            box0[3,3] = 11;


            int o1 = opxork1[0];
            int o2 = opxork1[3];

            row = (o1 * 2) + (o2 * 1); //2


            int i1 = opxork1[1];
            int i2 = opxork1[2];

            column = (i1 * 2) + (i2 * 1); //3

            roww = box0[row,column]; //00

            char[] rowColumn = new char[2];

            if (roww == 0)
            {
                string rowwx = "00";
                rowColumn = rowwx.ToCharArray();
            }
            if (roww == 1)
            {
                string rowwx = "01";
                rowColumn = rowwx.ToCharArray();
            }
            if (roww != 0 && roww != 1)
            {
                rowColumn = roww.ToString().ToCharArray();
            }

            for (int i = 0; i < 2; i++)
            {

                string item = rowColumn[i].ToString();
                int columnnn = Int32.Parse(item);
                columnn = columnnn;
                if (columnn == 49)
                {
                    s0[i] = 1;
                }
                if (columnn == 48)
                {
                    s0[i] = 0;
                }


            }
            return s0;
        }

        private int[] S0andS1K2()
        {
            int[] s0ands1 = new int[4];
            int[] temp1 = S0K2();
            int[] temp2 = S1K2();

            s0ands1[0] = temp1[0];
            s0ands1[1] = temp1[1];

            s0ands1[2] = temp2[0];
            s0ands1[3] = temp2[1];

            return s0ands1;
        }

        //PASO17: permutación de 4 bits, sobre la union de la box0 y box1
        private int[] FourPermutationsK2()
        {
            int[] fourpermutations = new int[4];
            int[] temp = S0andS1K2();

            fourpermutations[0] = temp[1]; //2
            fourpermutations[1] = temp[3]; //4
            fourpermutations[2] = temp[2]; //3
            fourpermutations[3] = temp[0]; //1

            return fourpermutations;
        }

        //PASO18: Paso3 XOR paso 7, tamño: 4 bits
        private int[] LeftbitsXORFourpermutationsK2()
        {
            int[] leftbitsXORfourpemutations = new int[4];
            int[] temp1 = FourPermutationsK2();
            int[] temp2 = LeftBitsK2();

            leftbitsXORfourpemutations[0] = temp1[0] ^ temp2[0];
            leftbitsXORfourpemutations[1] = temp1[1] ^ temp2[1];
            leftbitsXORfourpemutations[2] = temp1[2] ^ temp2[2];
            leftbitsXORfourpemutations[3] = temp1[3] ^ temp2[3];

            return leftbitsXORfourpemutations;
        }

        //PASO19: paso18 + permutación de 8 a la derecha || Tamaño: 8bits
        private int[] XORandRightBitsK2()
        {
            int[] xorANDrightbits = new int[8];
            int[] temp1 = LeftbitsXORFourpermutationsK2();
            int[] temp2 = RightBitsK2();

            xorANDrightbits[0] = temp1[0];
            xorANDrightbits[1] = temp1[1];
            xorANDrightbits[2] = temp1[2];
            xorANDrightbits[3] = temp1[3];

            xorANDrightbits[4] = temp2[0];
            xorANDrightbits[5] = temp2[1];
            xorANDrightbits[6] = temp2[2];
            xorANDrightbits[7] = temp2[3];

            return xorANDrightbits;
        }


        public int[] EightPermutationsIK2()
        {
            int[] eightpermutations = new int[8];
            int[] temp = XORandRightBitsK2();

            eightpermutations[0] = temp[3]; //4
            eightpermutations[1] = temp[0]; //1
            eightpermutations[2] = temp[2]; //3
            eightpermutations[3] = temp[4]; //5
            eightpermutations[4] = temp[6]; //7
            eightpermutations[5] = temp[1]; //2
            eightpermutations[6] = temp[7]; //8
            eightpermutations[7] = temp[5]; //6

            return eightpermutations;
        }


    }
}
