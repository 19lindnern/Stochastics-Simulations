using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;

namespace CTMC
{
    public class MarkovProcess
    {
        private Matrix Generator;
        private Exponential E;
        private Random U;
        private int SimCounter;
        private int State;
        private int InitialState;
        private int FinalState;
        private double Time;
        private string Name;
        private List<(double, double, int, int)> SimData; //Arrival time, Inter-arrival time, origin, destination 

        public MarkovProcess(Matrix gen, int initialState, string name)
        {
            if (!gen.IsGenerator())
            {
                throw new Exception("Input is not a valid Generator Matrix");
            }
            Generator = new Matrix(gen);

            if (0 > initialState || initialState > Generator.GetRows() )
            {
                throw new Exception("Invalid initial state");
            }
            
            InitialState = initialState;
            State = InitialState;
            E = new Exponential(-Generator[State, State]);
            U = new Random();
            SimData = new List<(double, double, int, int)>();
            SimCounter = 0;
            Name = name;
        }
        
        public void PrintMatrix()
        {
            Console.Out.WriteLine(Generator.ToString());
        }

        private void Jump()
        {
            int origin = State;
            double interArrival = E.Sample();
            Time += interArrival;
            double dest = U.NextDouble();
            double sum = 0;

            if (Generator[State, State] == 0)
            {
                SimData.Add((Time, interArrival, origin, State));
                return;
            }
            
            for (int i = 0; i < Generator.GetRows(); i++)
            {
                if (i != State)
                {
                    sum += -Generator[State, i] / Generator[State, State];
                    if (dest < sum)
                    {
                        State = i;
                        SimData.Add((Time, interArrival, origin, State));
                        return;
                    }
                }
            }
            throw new Exception("Jump failed. Invalid generator matrix.");
        }

        public void Simulate(double maxTime)
        {
            SimData.Clear();
            while (Time < maxTime)
            {
                Jump();
            }
            SimCounter++;
        }

        public void WriteToFile(string path) //Write current SimData list out to file.
        {
            //TODO: Add header with statistical information. Potentially reformat the file output into a more table-like view.
            string[] filePathArray = {path, "Simulation-" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") +".txt"};
            string filePath = Path.Combine(filePathArray);
            using (System.IO.StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < SimData.Count; i++)
                {
                    string output =
                        $"Jump Number: {i}, Arrival Time: {SimData[i].Item1.ToString()}, Inter-Arrival Time: {SimData[i].Item2.ToString()}, Origin: {SimData[i].Item3.ToString()}, Destination: {SimData[i].Item4.ToString()}";
                    writer.WriteLine(output);
                }
            }
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetSimCount()
        {
            return SimCounter;
        }
    }
}