using System;
using System.IO;
using System.Linq;
using System.Collections;

namespace MALproject1
{
    class Program
    {
        // Error messages

        static ErrorLabels IllFormedLabel = new ErrorLabels("first token ends in a colon" +
                                          " but doesn't follow the rules for a label name", "IllFormedLabel");
        static ErrorLabels InvalidOpcode = new ErrorLabels("first token is not one of the valid opcodes", "InvalidOpcode");
        static ErrorLabels ExcessOperands = new ErrorLabels("for the specific opcode, there are more " +
                                          "operands than required", "ExcessOperands");
        static ErrorLabels ScarceOperands = new ErrorLabels("for the specific opcode, there are fewer operands" +
                                          "than required", "ScarceOperands");
        static ErrorLabels IllFormedIdentifier = new ErrorLabels("invalid identifier", "IllFormedIdentifier");
        static ErrorLabels IllFormedLiteral = new ErrorLabels("numeric literal is invalid", "IllFormedLiteral");


        static string inputFile = @"C:\Users\Owner\Desktop\ProgrammingPrinciples\MALproject1_TannerRosner\MALproject1\MALproject1\MALinputprogram.txt";
        static string outputFile = @"C:\Users\Owner\Desktop\ProgrammingPrinciples\MALproject1_TannerRosner\MALproject1\MALproject1\MALreportFile.txt";

        static int numErrors = 0;
        static int numLines = 1;
        static string error;
        static ArrayList errorList = new ArrayList();
        
        static void Main(string[] args)
        {
            
            string[] lines = File.ReadAllLines(inputFile);
            StreamWriter file = new StreamWriter(outputFile);
            IntroOutput(file, lines);

            string[] noBlankLines = File.ReadAllLines(inputFile).Where(s => s.Trim() != string.Empty).ToArray();
            SecondOutput(file, noBlankLines);
            ThirdOutput(file, noBlankLines);
            FinalOutput(file);

            file.Close();
        }
        


        // This prints the title page and also the original MAL program
        public static void IntroOutput(StreamWriter file, string[] lines)
        {
            file.WriteLine("{0,6}Mal Project 1 | MALinputprogram | MALreportFile | 02/26/2019 | Tanner Rosner" +
                "| CS 3210", " ");
            file.WriteLine(" ");

            int lineCount = 1;
            foreach (string line in lines)
            {
                file.WriteLine(lineCount + " " + line);
                lineCount++;
            }
           
        }

        //This prints the second MAL program with stripped of blank lines and comments
        public static void SecondOutput(StreamWriter file ,string[] lines)
        {

            int lineCount = 1;
            file.WriteLine("\n{0,6}", "-----------------------\n");
            file.WriteLine("Stripped MAL Program Listing:");
            file.WriteLine(" ");



            foreach (string line in lines)
            {
                if (!line.Contains('/'))
                {
                    file.Write(lineCount + " ");
                    file.WriteLine(line);
                    string[] words = line.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                }
                lineCount++;
               
            }
            
            file.WriteLine(" ");
            file.WriteLine("{0,6}", "-----------------------");
            file.WriteLine("Error Report Listing");
            file.WriteLine(" ");
        }

        // Creates the final MAL program with errors listed
        public static void ThirdOutput(StreamWriter file, string[] lines)
        {
            
            foreach(string line in lines)
            {
                if(!line.Contains('/'))
                {
                    file.Write(numLines + " ");
                    string[] words = line.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                    //Analyzes each line for errors by calling this method
                    AnalyzeLine(words, file, line);
                    numLines++;
                }
            }
        }

        

        public static void AnalyzeLine(string[] words, StreamWriter file, string line)
        {
            
            
                    switch (words[0])
                    {
                        case "Add":
                        CheckErrorsForAddSubOrMult(words);

                        break;
                        case "Sub":
                        CheckErrorsForAddSubOrMult(words);

                            break;
                        case "Mul":
                        CheckErrorsForAddSubOrMult(words);

                        break;
                        case "Str":
                        CheckErrorsForStrOrLoad(words);
                        break;
                        case "LoadI":
                         CheckErrorsForStrOrLoad(words);
                        break;
                        default:
                            error = InvalidOpcode.ReturnLabelDescription();
                            errorList.Add(InvalidOpcode.ReturnLabelTitle());
                            numErrors++;
                            break;
                    }

            Console.WriteLine(error);
            PrintThirdReport( file, error, line);
         }

        public static void PrintThirdReport(StreamWriter file, string error, string line)
        {
            file.WriteLine(line);
            if(error != "")
            {
                file.WriteLine("***" + error + "***");
               
            }
            
        }

        public static void CheckErrorsForAddSubOrMult(string[] words)
        {
            if (words.Length > 4)
            {
                error = ExcessOperands.ReturnLabelDescription();
                errorList.Add(ExcessOperands.ReturnLabelTitle());
                numErrors++;
            }
            else if (words.Length < 4)
            {
                error = ScarceOperands.ReturnLabelDescription();
                errorList.Add(ScarceOperands.ReturnLabelTitle());
                numErrors++;
            }
            else
            {
                error = "";
            }
        }

        public static void CheckErrorsForStrOrLoad(string[] words)
        {
            if (words.Length > 3)
            {
                error = ExcessOperands.ReturnLabelDescription();
                errorList.Add(ExcessOperands.ReturnLabelTitle());
                numErrors++;
            }
            else if (words.Length < 3)
            {
                error = ScarceOperands.ReturnLabelDescription();
                errorList.Add(ScarceOperands.ReturnLabelTitle());
                numErrors++;
            }
            else
            {
                error = "";
            }
        }

        public static void FinalOutput(StreamWriter file)
        {
            file.WriteLine(" ");
            file.WriteLine("{0, 6}", "-----------------------");
            file.WriteLine(" ");
            file.WriteLine("Total Errors: " + numErrors);

            if(errorList != null)
            {
                for (int i = 0; i < errorList.Count; i++)
                {
                    file.WriteLine(errorList[i]);
                }

                file.WriteLine(" ");
                file.WriteLine("Processing Complete: MAL Program Is Not Valid");
            }
            else
            {
                file.WriteLine(" ");
                file.WriteLine("Processing Complete: No Errors Found");
            }
            
        }
     }

}
