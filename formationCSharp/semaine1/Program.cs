using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Serie1;
using Serie2;
using Serie3;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;

namespace semaine1
{
    internal class Program
    {



        static void Main(string[] args)
        {
            /*           ElementaryOperations.BasicOperation(3, 4, '+');
                       ElementaryOperations.BasicOperation(6, 2, '/');
                       ElementaryOperations.BasicOperation(3, 0, '/');
                       ElementaryOperations.BasicOperation(6, 9, 'L');

                       // integer devision 
                       Console.WriteLine("integer devision ");

                       ElementaryOperations.IntegerDivision(12, -4);
                       ElementaryOperations.IntegerDivision(13, -4);
                       ElementaryOperations.IntegerDivision(12, 0);

                       // power 

                       ElementaryOperations.Pow(2, -4);
                       ElementaryOperations.Pow(3, 2);
                       ElementaryOperations.Pow(-2, 3);*/


            // journe 
            /*  SpeakingClock.Journee(-1);
              SpeakingClock.Journee(0);
              SpeakingClock.Journee(1);
              SpeakingClock.Journee(5);
              SpeakingClock.Journee(6);
              SpeakingClock.Journee(11);
              SpeakingClock.Journee(12);
              SpeakingClock.Journee(13);
              SpeakingClock.Journee(17);
              SpeakingClock.Journee(18);
              SpeakingClock.Journee(20);
              SpeakingClock.Journee(23);
              SpeakingClock.Journee(29);*/
            /*
                        Pyramid.PyramidConstruction(5, true);
                        Pyramid.PyramidConstruction(5, false);*/
            /*
                        Console.WriteLine(Factorial.Factorial_(0));
                        Console.WriteLine(Factorial.Factorial_(1));
                        Console.WriteLine(Factorial.Factorial_(5));
                        Console.WriteLine(Factorial.FactorialRecursive(0));
                        Console.WriteLine(Factorial.FactorialRecursive(1));
                        Console.WriteLine(Factorial.FactorialRecursive(5));
            */


            /*
             * serie 2
             */
            /*int[] tabs = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            TasksTables.SommePrinter(tabs);

            TasksTables.OperationPrinter(tabs, '+', 4);

            TasksTables.OperationPrinter(tabs, '-', 4);

            TasksTables.OperationPrinter(tabs, '*', 4);

            TasksTables.OperationPrinter(tabs, '/', 4);

            TasksTables.OperationPrinter(tabs, '/', 0);

            TasksTables.OperationPrinter(tabs, 'L', 6);



            int[] tab1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int[] tab2 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            TasksTables.PrintTab(tab1, header:"tab 1 : ");
            TasksTables.PrintTab(tab2, header:"tab 2 : ");
            int[] concatinated = TasksTables.ConcatTab(tab1, tab2);
            TasksTables.PrintTab(concatinated, header: "tab 1 + tab 2 : ");

            int[] tab3 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int[] tab4 = new int[] { };
            TasksTables.PrintTab(tab1, header: "tab 1 : ");
            TasksTables.PrintTab(tab2, header: "tab 2 : ");
            int[] concatinated1 = TasksTables.ConcatTab(tab3, tab4);
            TasksTables.PrintTab(concatinated1, header: "tab 1 + tab 2 : ");*/

            /*            int[,] morpion = { { 1, 1, -1 }, { -1, -1, 0 }, { -1, -1, 0 } };
                        Morpion.DisplayMorpion(morpion);
                        int resultat = Morpion.CheckMorpion(morpion);
                        Console.WriteLine(resultat);*/


            /*            int[] tab3 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                        int result = Search.LinearSearch(tab3, -5);
                        Console.WriteLine(result);
                        Console.WriteLine(Search.BinarySearch(tab3, 6));
                        Console.WriteLine(Search.BinarySearch(tab3, 3));
                        Console.WriteLine(Search.BinarySearch(tab3, 0));
                        Console.WriteLine(Search.BinarySearch(tab3, 9));*/

            /*String unsensored = "Hello afghanistan iraq bombe grande iraq ville londre berlin communists";
            String[] prohibited = { "iraq", "ville", "sparta", "Ville" };
            String clear = AdministrativeTasks.EliminateSeditiousThoughts(unsensored, prohibited);
            Console.WriteLine(clear);*/


            /*            // serie 3 exercice 1
                        StringBuilder personalInfoStringBuilder = new StringBuilder();
                        bool format;
                        String[] chaines = new string[3];

                        // cas valide
                        string infoPers1 = personalInfoStringBuilder
                            .Append("Mme ")
                            .Append("    cesar   ")
                            .Append("   dupont   ")
                            .Append("02")
                            .ToString();
                        chaines[0] =  infoPers1;
                        personalInfoStringBuilder.Clear();
            *//*
                        format = AdministrativeTasks.ControlFormat(infoPers1);
                        Console.WriteLine(format);
            *//*

                        // cas errone age
                        string infoPers2 = personalInfoStringBuilder
                            .Append("Mme ")
                            .Append("    cesar   ")
                            .Append("   dupont   ")
                            .Append("s2")
                            .ToString();

                        chaines[1] = infoPers2;
                        personalInfoStringBuilder.Clear();
            *//*
                        format = AdministrativeTasks.ControlFormat(infoPers2);
                        Console.WriteLine(format);
            *//*

                        // cas errone civilite
                        string infoPers3 = personalInfoStringBuilder
                            .Append("Mse ")
                            .Append("    c1sar   ")
                            .Append("   dupont   ")
                            .Append("02")
                            .ToString();

                        chaines[2] = infoPers3;
                        personalInfoStringBuilder.Clear();
            *//*
                        format = AdministrativeTasks.ControlFormat(infoPers3);
                        Console.WriteLine(format);
            *//*

                        AdministrativeTasks.AffichageComplet(chaines);
            */

            // socializer une date
            /*            Console.WriteLine(AdministrativeTasks.SocialiserUneDate("2001-03-04"));

                        String reportNonSocialiste = @"
                                    qsdqsdqsdqsdqsd  01248-123-123-233232323-12  2001-12-03                        
                                    qdsdqdqsd
                                    ";

                        Console.WriteLine(AdministrativeTasks.ChangeDate(reportNonSocialiste));*/

            Cesar CesarMachine = new Cesar();
            string chiffre = CesarMachine.CesarCode("BONJOURABCDEFGHIJKLMNOPQRSTUVWXYZ");
            string dechifre = CesarMachine.DecryptCesarCode(chiffre);
            Console.WriteLine(chiffre);
            Console.WriteLine(dechifre);

            string chiffre1 = CesarMachine.GeneralCesarCode("BONJOURABCDEFGHIJKLMNOPQRSTUVWXYZ", 5);
            string dechifre1 = CesarMachine.GeneralDecryptCesarCode(chiffre1, 5);
            Console.WriteLine(chiffre1);
            Console.WriteLine(dechifre1);
        }

        }


}
