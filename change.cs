/* Change.cs -version 0.2 - authored by Alex Reid & Max Austin
 * For use in CSCI305 Fall 2016 Lab #3 - ATM 
 * 
 * Change simply holds the amount of change requested to be dispensed
 * with appropriate getters and setters.
 * Will be *hopefully* more useful in later assignments.
 */

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
