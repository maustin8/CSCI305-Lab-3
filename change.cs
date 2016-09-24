using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atm
{
    /*This class handles the amount of change wanted from the ATM*/
    public class Change
    {
        //The current change being processed
        private int changeAmt;

        /*Setter to set the current change amount*/
        public void setAmt(int newChange)
        {
            changeAmt = newChange;
        }//setAmt()

        /*Getter to get the current change amount*/
        public int getAmt()
        {
            return changeAmt;
        }//getAmt()

        
    }
}
