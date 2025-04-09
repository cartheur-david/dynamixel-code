using Cartheur.Animals.Robot;
using ConsoleTables;

class PoseReader
{
    public static MotorFunctions MotorControl { get; set; }
    public static MotorSequence MotorSequences { get; set; }
    public static bool MotorsInitialized { get; set; }

    static async Task Main()
    {
        MotorControl = new MotorFunctions();
        MotorSequences = new MotorSequence();
        Console.WriteLine(MotorFunctions.InitializeDynamixelMotors());
        MotorsInitialized = MotorFunctions.DynamixelMotorsInitialized;
        MotorFunctions.CollateMotorArray();

        if (MotorsInitialized) { MotorControl.CreateConnectMotorObjects(); }
        else Logging.WriteLog("Cannot create connection objects.", Logging.LogType.Error, Logging.LogCaller.MotorControl);

        Console.WriteLine("Hold the robot in the standing pose, then press Enter...");
        Console.ReadLine(); // Wait for you to position it.

        // Read and display current positions.
        var abdomen = MotorSequences.ReturnDictionaryOfPositions(Limbic.Abdomen);
        var leftLeg = MotorSequences.ReturnDictionaryOfPositions(Limbic.LeftLeg);
        var rightLeg = MotorSequences.ReturnDictionaryOfPositions(Limbic.RightLeg);

        // Output as code-friendly format.
        Console.WriteLine("\nStanding pose dictionary:" + Environment.NewLine);

        var tableAbdomen = new ConsoleTable("abdomen-motor", "position");
        var tableLeftLeg = new ConsoleTable("left-leg-motor", "position");
        var tablerightLeg = new ConsoleTable("right-leg-motor", "position");

        foreach (var kvp in abdomen)
        {
            tableAbdomen.AddRow(kvp.Key, kvp.Value);
        }
        tableAbdomen.Write();
        Console.WriteLine();
        foreach (var kvp in leftLeg)
        {
            tableLeftLeg.AddRow(kvp.Key, kvp.Value);
        }
        tableLeftLeg.Write();
        Console.WriteLine();
        foreach (var kvp in rightLeg)
        {
            tablerightLeg.AddRow(kvp.Key, kvp.Value);
        }
        tablerightLeg.Write();
        Console.WriteLine();
        Console.ReadLine(); // Wait for you to view the position values.

        // Dispose resources.
        MotorFunctions.DisposeDynamixelMotors();

        await Task.CompletedTask;
    }
}
