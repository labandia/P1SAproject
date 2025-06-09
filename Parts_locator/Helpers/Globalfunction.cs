using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parts_locator.Data
{
    internal class Globalfunction
    {
        public static int Pallet_number(string pname)
        {
            int result = 0;

            switch (pname)
            {
                case "A":
                    result = 1;
                    break;
                case "B":    
                    result = 2; 
                    break;
                case "C":
                    result = 3; 
                    break;
                case "D":                    
                    result = 4; 
                    break;
                case "E":    
                    result = 5; 
                    break;                   
                case "F":                   
                    result = 6; 
                    break;                      
                case "H":          
                    result = 7; 
                    break;     
                case "I":
                    result = 8; 
                    break;
                case "J": 
                    result = 9;
                    break;
                case "L":
                    result = 10;
                    break;
                case "M":
                    result = 11;
                    break;
                case "N":
                    result = 12;
                    break;
                case "O":
                    result = 13;
                    break;
                case "P":
                    result = 14;
                    break;
                case "R":
                    result = 15;
                    break;
                case "S":
                    result = 16;
                    break;
                case "T":
                    result = 17;
                    break;
                case "U":
                    result = 18;
                    break;
            }


            return result;
        }
    }
}
