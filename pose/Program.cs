using Cartheur.Animals.Robot;

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
        Console.WriteLine("\nStanding pose dictionary:");
        Console.Write("var currentPose = new Dictionary<string, int> { ");
        foreach (var kvp in abdomen)
        {
            Console.Write($"{{ {kvp.Key}, {kvp.Value} }}, ");
        }
        foreach (var kvp in leftLeg)
        {
            Console.Write($"{{ {kvp.Key}, {kvp.Value} }}, ");
        }
        foreach (var kvp in rightLeg)
        {
            Console.Write($"{{ {kvp.Key}, {kvp.Value} }}, ");
        }
        Console.WriteLine("};");

        // Dispose resources.
        MotorFunctions.DisposeDynamixelMotors();

        await Task.CompletedTask;
    }
}
