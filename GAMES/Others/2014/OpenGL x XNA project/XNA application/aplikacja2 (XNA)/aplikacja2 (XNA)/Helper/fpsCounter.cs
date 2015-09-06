using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aplikacja2__XNA_.BasicComponent
{
    class fpsCounter
    {
        private int FPS_ARRAY_LENGHT = 100;

        public int []fpsArray;
        public int []fpsArray1;
        public int []fpsArray2;
        public int []fpsArray3;
        public int []fpsArray4;
        public int []fpsArray5;

        public int[] fpsArray_X;
        public int[] fpsArray1_X;
        public int[] fpsArray2_X;
        public int[] fpsArray3_X;
        public int[] fpsArray4_X;
        public int[] fpsArray5_X;

        public int fps = 0;
        public int count = 0;
        public int Time0 = 0;

        public fpsCounter()
        {
            fpsArray = new int[FPS_ARRAY_LENGHT];
            fpsArray1 = new int[FPS_ARRAY_LENGHT];
            fpsArray2 = new int[FPS_ARRAY_LENGHT];
            fpsArray3 = new int[FPS_ARRAY_LENGHT];
            fpsArray4 = new int[FPS_ARRAY_LENGHT];
            fpsArray5 = new int[FPS_ARRAY_LENGHT];

            fpsArray_X = new int[FPS_ARRAY_LENGHT];
            fpsArray1_X = new int[FPS_ARRAY_LENGHT];
            fpsArray2_X = new int[FPS_ARRAY_LENGHT];
            fpsArray3_X = new int[FPS_ARRAY_LENGHT];
            fpsArray4_X = new int[FPS_ARRAY_LENGHT];
            fpsArray5_X = new int[FPS_ARRAY_LENGHT];
        }

        public bool countFPS(int time, int tryb)
        {
	        count ++;

        	if (Math.Abs(time -Time0) >= 1)
	        {
		        fps = count;
		        count = 0;

        		Time0 = time;

                /*
	        	for (int i = 0; i < FPS_ARRAY_LENGHT - 1; i++)
		        {
			        fpsArray[i] = fpsArray[i+1];

        			if (tryb == 1) fpsArray1[i] = fpsArray1[i+1];
		        	else if (tryb == 2) fpsArray2[i] = fpsArray2[i+1];
			        else if (tryb == 3) fpsArray3[i] = fpsArray3[i+1];
		        	else if (tryb == 4) fpsArray4[i] = fpsArray4[i+1];
	        		else if (tryb == 5) fpsArray5[i] = fpsArray5[i+1];
		        }

	        	fpsArray[0] = fps;

        		if (tryb == 1) fpsArray1[0] = fps;
		        else if (tryb == 2) fpsArray2[0] = fps;
		        else if (tryb == 3) fpsArray3[0] = fps;
		        else if (tryb == 4) fpsArray4[0] = fps;
		        else if (tryb == 5) fpsArray5[0] = fps;
                */

                for (int i = FPS_ARRAY_LENGHT - 2; i >= 0; i--)
                {
                    fpsArray[i + 1] = fpsArray[i];
                    fpsArray_X[i + 1] = fpsArray_X[i];

                    if (tryb == 1)
                    {
                        fpsArray1[i + 1] = fpsArray1[i];
                        fpsArray1_X[i + 1] = fpsArray1_X[i];
                    }
                    else if (tryb == 2)
                    {
                        fpsArray2[i + 1] = fpsArray2[i];
                        fpsArray2_X[i + 1] = fpsArray2_X[i];
                    }
                    else if (tryb == 3)
                    {
                        fpsArray3[i + 1] = fpsArray3[i];
                        fpsArray3_X[i + 1] = fpsArray3_X[i];
                    }
                    else if (tryb == 4)
                    {
                        fpsArray4[i + 1] = fpsArray4[i];
                        fpsArray4_X[i + 1] = fpsArray4_X[i];
                    }
                    else if (tryb == 5)
                    {
                        fpsArray5[i + 1] = fpsArray5[i];
                        fpsArray5_X[i + 1] = fpsArray5_X[i];
                    }
                }

                fpsArray[0] = fps;
                fpsArray_X[0] = time;

                if (tryb == 1)
                {
                    fpsArray1[0] = fps;
                    fpsArray1_X[0] = time;
                }
                else if (tryb == 2)
                {
                    fpsArray2[0] = fps;
                    fpsArray2_X[0] = time;
                }
                else if (tryb == 3)
                {
                    fpsArray3[0] = fps;
                    fpsArray3_X[0] = time;
                }
                else if (tryb == 4)
                {
                    fpsArray4[0] = fps;
                    fpsArray4_X[0] = time;
                }
                else if (tryb == 5)
                {
                    fpsArray5[0] = fps;
                    fpsArray5_X[0] = time;
                }

                return true;
	        }

            return false;
        }
    }
}
