/* Processor -version 0.2 - authored by Alex Reid & Max Austin
 * For use in CSCI305 Fall 2016 Lab #3 - ATM 
 * 
 * Processes attempts to process the file gotten from atmDriver
 * and determine the amount of money needed to be dispensed and the 
 * amount of each denomination. Will return exception if file not found.
 *
 * Processes should also handle cases where amounts for a
 * certain denomination(s) may not be given due to incorrect 
 * input formatting. i.e. = [m, n5, n10, , , ]
 *                          [m, n5, n10]
 *                          [m, n5]
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atm
{
    public class Processor
    {
        private static string[] input;
        private static int[] nums = new int[6];
        private string outText = "";

        //instantiation of Change and ATM objects
        private ATM atm = new ATM();
        private Change change = new Change();
        
        //Default constructor
        Processor() { }

        /* The workhorse of the whole program, makeChange contains the algorithm used to
         * produce the amounts of each denomination to produce the correct change.
         */
        public void makeChange()
        {

            int changeAmt = change.getAmt();
            int five = 5;
            int ten = 10;
            int twenty = 20;
            int fifty = 50;
            int hund = 100;

            /* Our algorithm makes use of integer division to get the number of bills for a denomination
             * It then takes the modulus of the previous division in order to get the remainder that must still
             * be made into change. The division order goes from largest denom to smallest, like a series
             * of finer sediment screens.
             */
            if (atm.isChangePossible(changeAmt))
            {
                int hunAmt = (int)(changeAmt / hund);
                int fiftyAmt = (int)((changeAmt % hund) / fifty);
                int twentyAmt = (int)(((changeAmt % hund) % fifty) / twenty);
                int tenAmt = (int)((((changeAmt % hund) % fifty) % twenty) / ten);
                int fiveAmt = (int)(((((changeAmt % hund) % fifty) % twenty) % ten) / five);

                /* Updates the amounts left in the ATM, based on what amounts of each denomination
                 * the algorithm decides to use to make the change 
                 */
                int fiveLeft = atm.getFive() - fiveAmt;
                int tenLeft = atm.getTen() - tenAmt;
                int twentyLeft = atm.getTwenty() - twentyAmt;
                int fiftyLeft = atm.getFifty() - fiftyAmt;
                int hunLeft = atm.getHun() - hunAmt;

                outText = "[" + fiveLeft + " " + tenLeft + " " + twentyLeft + " " + fiftyLeft + " " + hunLeft + "]";         
            }
            else
            {
                //If isChangePossible() returns false, then let user know there is insufficient funds 
                outText = "Insufficient funds";
            }
        }
        public void go(string filename)
        {
            string line = "";
            string outFile = "out.txt";

            try
            {
                if (File.Exists(outFile)) //Deletes outFile.txt so a fresh outFile.txt can be created
                {
                    File.Delete(outFile);
                }

                //Create an instance of StreamWriter to write to an output file.
                //statement "using" also closes StreamWriter once done.
                using (StreamWriter sw = new StreamWriter(outFile))
                {
                    // Create an instance of StreamReader to read from a file.
                    // The "using" statement also closes the StreamReader.
                    using (StreamReader sr = new StreamReader(filename))
                    {

                        // Read and display lines from the file until the end of 
                        // the file is reached.
                        while (sr.Peek() >= 0)
                        {
                            line = sr.ReadLine();
                            line = line.Remove(0, 1);
                            line = line.Remove(line.Length - 1, 1);
                            input = line.Split(',');
                            for (int i = 0; i < input.Length; i++)
                            {
                                if (input[i] == " " || input[i] == "")
                                {
                                    input[i] = "0";
                                }
                                nums[i] = Int32.Parse(input[i]);
                            }

                            change.setAmt(nums[0]);
                            atm.setFive(nums[1]);
                            atm.setTen(nums[2]);
                            atm.setTwenty(nums[3]);
                            atm.setFifty(nums[4]);
                            atm.setHun(nums[5]);

                            makeChange();
                            sw.WriteLine(outText);    //Reference: Console.WriteLine(outText) happens right before in makeChange()

                        }
                    }      
                }
            }
            catch (Exception e)
            {
                //Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }

        //Execution begins here.
        public static void Main(string[] args)
        {
            Processor processer = new Processor();
            string filename;

            //File can be included as an argument or passed into the console.
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a file to open: ");
                filename = Console.ReadLine();
            }
            else
                filename = args[0];

            Console.WriteLine(filename);
            processer.go(filename);

            //Console.WriteLine("Please enter any key to exit");   <-- We included a prompt to leave the console running if a real user wanted to use
            //Console.ReadKey();
        }

    }


}
